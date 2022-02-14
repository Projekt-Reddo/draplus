using DraplusApi.Data;
using DraplusApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Npgsql;
using System.Text;
using static Constant;
using DraplusApi.Helper;
using DraplusApi.Hubs;
using DraplusApi.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region PostgrestDb config

string userId = Environment.GetEnvironmentVariable(DbConfig.UserId) ?? builder.Configuration[DbConfig.UserId];
string password = Environment.GetEnvironmentVariable(DbConfig.Password) ?? builder.Configuration[DbConfig.Password];

var dbBuilder = new NpgsqlConnectionStringBuilder();
dbBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSql");
dbBuilder.Username = userId;
dbBuilder.Password = password;

builder.Services.AddDbContext<DataContext>(opt => opt.UseNpgsql(dbBuilder.ConnectionString));

#endregion

#region MongoDB config

builder.Services.Configure<MongoDbSetting>(builder.Configuration.GetSection("MongoDbSetting"));
builder.Services.AddSingleton<MongoDbSetting>(sp => sp.GetRequiredService<IOptions<MongoDbSetting>>().Value);
builder.Services.AddSingleton<IMongoContext, MongoContext>();

// alows CORS
builder.Services.AddCors();
builder.Services.AddSignalR();

// MongoDB Services
builder.Services.AddScoped<IBoardRepo, BoardRepo>();
builder.Services.AddScoped<IChatRoomRepo, ChatRoomRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

#endregion

builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opt => new Dictionary<string, UserConnection>());

// Authentication
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // options.Authority = "https://securetoken.google.com/draplus-api";
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecret"])),
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://securetoken.google.com/draplus-api",
            ValidateAudience = true,
            ValidAudience = "draplus-api",
            ValidateLifetime = true
        };
    });
builder.Services.AddSingleton<IJwtGenerator>(new JwtGenerator(configuration["JwtSecret"]));

// Auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    // Config for camelCase properties name return in json
    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token (Access Token) on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    opt.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// cors has to be on top of all
app.UseCors(opt=>opt.SetIsOriginAllowed(origin=>true)
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapControllers();

app.Run();
