using System.Security.Cryptography;
using System.Text;
using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;
using EnergiasRenovables.Model.DTO.Biomasa;
using EnergiasRenovables.Model.Strategy.ConcreteStrategy;
using EnergiasRenovables.Model.Strategy.Context;
using EnergiasRenovables.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar servicios de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Energías Renovables",
        Version = "v1",
        Description = "API para gestionar usuarios con autenticación JWT."
    });

    // Configurar el esquema de autenticación Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en este formato: Bearer {token}"
    });

    // Requerir el esquema en todos los endpoints protegidos
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configurar JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException());

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
    var signingkey = new SymmetricSecurityKey(key);
    var signingCredential = new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256Signature);

    opt.RequireHttpsMetadata = false;

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signingkey
    };
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://another-example.com") // Especificar dominios permitidos
            .AllowAnyHeader() // Permitir cualquier encabezado
            .AllowAnyMethod() // Permitir cualquier método HTTP (GET, POST, etc.)
            .AllowCredentials(); // Permitir cookies o credenciales
    });

    /*options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Permitir cualquier dominio
            .AllowAnyHeader()
            .AllowAnyMethod();
    });*/
});

builder.Services.AddAuthorization();
// Add db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add Energia Renovable Context for Energia Solar
builder.Services
    .AddScoped<EnergiaRenovableContext<ObtenerEnergiSolarDTO, InsertarEnergiaSolarDTO>>(provider =>
{
    var strategy = provider.GetRequiredService<EnergiaSolarConcrete>();
    return new EnergiaRenovableContext<ObtenerEnergiSolarDTO, InsertarEnergiaSolarDTO>(strategy);
});
// Add Energia Renovable Context for Energia Eolica
builder.Services
    .AddScoped<EnergiaRenovableContext<ObtenerEnergiaEolicaDTO, InsertarEnergiaEolicaDTO>>(provider =>
{
    var strategy = provider.GetRequiredService<EnergiaEolicaConcrete>();
    return new EnergiaRenovableContext<ObtenerEnergiaEolicaDTO, InsertarEnergiaEolicaDTO>(strategy);
});

builder.Services
    .AddScoped<EnergiaRenovableContext<ObtenerBiomasaDTO, InsertarBiomasaDTO>>(provider =>
    {
        var strategy = provider.GetRequiredService<BiomasaConcrete>();
        return new EnergiaRenovableContext<ObtenerBiomasaDTO, InsertarBiomasaDTO>(strategy);
    });

builder.Services
    .AddScoped<EnergiaRenovableContext<ObtenerEnergiaHidroelectricaDTO, InsertarEnergiaHidroelectricaDTO>>(provider =>
    {
        var strategy = provider.GetRequiredService<EnergiaHidroelectricaConcrete>();
        return new EnergiaRenovableContext<ObtenerEnergiaHidroelectricaDTO, InsertarEnergiaHidroelectricaDTO>(strategy);
    });

builder.Services
    .AddScoped<EnergiaRenovableContext<ObtenerEnergiaGeotermicaDTO, InsertarEnergiaGeotermicaDTO>>(provider =>
    {
        var strategy = provider.GetRequiredService<EnergiaGeotermicaConcrete>();
        return new EnergiaRenovableContext<ObtenerEnergiaGeotermicaDTO, InsertarEnergiaGeotermicaDTO>(strategy);
    });
// Add Energia Solar
builder.Services.AddScoped<EnergiaSolarConcrete>();
// Add Energia Renovable Context for Energia Solar
builder.Services.AddScoped<EnergiaEolicaConcrete>();
// Add Energia Renovable Context for Biomasa
builder.Services.AddScoped<BiomasaConcrete>();

builder.Services.AddScoped<EnergiaHidroelectricaConcrete>();

builder.Services.AddScoped<EnergiaGeotermicaConcrete>();

builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

// Aplicar CORS (antes de los controladores)
app.UseCors("AllowSpecificOrigins"); // O "AllowAll" según tu caso

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Energías Renovables v1");
        c.RoutePrefix = string.Empty; // Para que Swagger esté en la raíz del proyecto
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configurar middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
