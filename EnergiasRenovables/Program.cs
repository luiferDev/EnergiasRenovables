using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;
using EnergiasRenovables.Model.Strategy.ConcreteStrategy;
using EnergiasRenovables.Model.Strategy.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add db context
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
// Add Energia Solar
builder.Services.AddScoped<EnergiaSolarConcrete>();
// Add Energia Renovable Context for Energia Solar
builder.Services.AddScoped<EnergiaEolicaConcrete>();



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
