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
using DraplusApi.Helpers;
using DraplusApi.Hubs;
using DraplusApi.Dtos;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// #region PostgrestDb config

// string userId = Environment.GetEnvironmentVariable(DbConfig.UserId) ?? builder.Configuration[DbConfig.UserId];
// string password = Environment.GetEnvironmentVariable(DbConfig.Password) ?? builder.Configuration[DbConfig.Password];

// var dbBuilder = new NpgsqlConnectionStringBuilder();
// dbBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSql");
// dbBuilder.Username = userId;
// dbBuilder.Password = password;

// builder.Services.AddDbContext<DataContext>(opt => opt.UseNpgsql(dbBuilder.ConnectionString));

// #endregion

#region MongoDB config

builder.Services.Configure<MongoDbSetting>(builder.Configuration.GetSection("MongoDbSetting"));
builder.Services.AddSingleton<MongoDbSetting>(sp => sp.GetRequiredService<IOptions<MongoDbSetting>>().Value);
builder.Services.AddSingleton<IMongoContext, MongoContext>();

#endregion

// Project Services
builder.Services.AddScoped<IBoardRepo, BoardRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ISignInRepo, SignInRepo>();

// alows CORS
builder.Services.AddCors();

// Config for SignalR
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opt => new Dictionary<string, UserConnection>());
builder.Services.AddSingleton<IDictionary<string, UserConnectionChat>>(opt => new Dictionary<string, UserConnectionChat>());

// Shape stack for each board
builder.Services.AddSingleton<IDictionary<string, List<ShapeReadDto>>>(opt => new Dictionary<string, List<ShapeReadDto>>());

// Note list for each board
builder.Services.AddSingleton<IDictionary<string, List<NoteDto>>>(opt => new Dictionary<string, List<NoteDto>>());

// Authentication
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecret"]))
        };
    });

builder.Services.AddAuthorization();

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

app.UseExceptionHandler(e => e.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()!.Error;
    await context.Response.WriteAsJsonAsync(new ResponseDto(500, exception.Message));
}));

// cors has to be on top of all
app.UseCors(opt => opt.WithOrigins(builder.Configuration.GetSection("FrontendUrl").Get<string[]>())
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials());

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDto(401), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDto(403), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
    }

});

app.UseAuthentication();

app.UseAuthorization();

// SignalR board maping
app.MapHub<ChatHub>("/chat");
app.MapHub<BoardHub>("/board");

app.MapControllers();

app.Run();
