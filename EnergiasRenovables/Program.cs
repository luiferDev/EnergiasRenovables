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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
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
