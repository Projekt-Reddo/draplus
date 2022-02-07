using DraplusApi.Data;
using DraplusApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Npgsql;
using static Constant;

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

// MongoDB Services
builder.Services.AddScoped<IChatRoomRepo, ChatRoomRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

#endregion

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
