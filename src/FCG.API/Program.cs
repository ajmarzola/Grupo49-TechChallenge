using FCG.Infrastructure.Data;
using FCG.API.GraphQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.AspNetCore.Http;
using FCG.Application.Services;
using FCG.Domain.Services;
using FCG.Domain.Repository;
using FCG.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// 1. Conexão com LocalDB
builder.Services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Configuração do GraphQL
builder.Services.AddGraphQLServer().AddQueryType<Queries>().AddFiltering().AddSorting().AddProjections();

builder.Services.AddAuthorization();

builder.Services.AddTransient<IJogoService>();

// 3. JWT
var jwtKey = builder.Configuration["Jwt:SecretKey"] ?? "sua-chave-super-secreta";
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// 4. Swagger
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FCG API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Informe o token JWT: Bearer {seu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });
});

// Configurar Serilog
Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();

// Adicionar logging
builder.Services.AddLogging();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<FCG.API.Middlewares.ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 

app.MapGraphQL("/graphql");

#region Start

app.MapGet("/", () => Results.Content(@"<!DOCTYPE html>
<html lang='pt-br'>
<head>
    <meta charset='UTF-8'>
    <title>Grupo 49 - FCG.TechChallenge</title>
    <style>
        body {
            background-color: #0f172a;
            color: #e2e8f0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
            margin: 0;
        }
        h1 {
            color: #38bdf8;
            font-size: 2.5rem;
            margin-bottom: 0.5rem;
        }
        h2 {
            font-size: 1.5rem;
            margin-bottom: 1.5rem;
            color: #f8fafc;
        }
        .swagger-link {
            background-color: #38bdf8;
            color: #0f172a;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 6px;
            font-weight: bold;
            transition: background-color 0.3s ease;
            margin-bottom: 2rem;
        }
        .swagger-link:hover {
            background-color: #0ea5e9;
        }
        ul {
            list-style-type: none;
            padding: 0;
            background-color: #1e293b;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.3);
        }
        li {
            padding: 12px 24px;
            border-bottom: 1px solid #334155;
        }
        li:last-child {
            border-bottom: none;
        }
        .badge {
            background-color: #38bdf8;
            color: #0f172a;
            padding: 2px 8px;
            border-radius: 6px;
            margin-left: 12px;
            font-size: 0.85rem;
        }
    </style>
</head>
<body>
    <h1>Grupo 49 - FCG.TechChallenge</h1>
    <h2>🚀 API rodando com sucesso!</h2>
    <p><a class='swagger-link' href='/swagger'>📘 Documentação da API</a></p>
    <ul>
        <li>Anderson <span class='badge'>RM005100</span></li>
        <li>Rafael <span class='badge'>RM334455</span></li>
        <li>Valber <span class='badge'>RM131450</span></li>
    </ul>
</body>
</html>", "text/html"));

#endregion

app.Run();