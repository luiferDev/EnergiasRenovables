using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO.Biomasa;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy;

public class EnergiaHidroelectricaConcrete(ApplicationDbContext context): ICalculoStrategy<ObtenerEnergiaHidroelectricaDto, InsertarEnergiaHidroelectricaDto>
{
    public List<ObtenerEnergiaHidroelectricaDto> ObtenerEnergia()
    {
        var energiaHidroelectrica = from es in context.EnergiaHidroelectricas
            join er in context.EnergiasRenovables on es.Id equals er.Id
            join te in context.TipoEnergias on er.TipoEnergiaId equals te.Id
            join pp in context.PlantaProduccions on es.Id equals pp.Id
            join ps in context.Paises on es.Id equals ps.Id
            select new ObtenerEnergiaHidroelectricaDto()
            {
                Id = es.Id,
                Caudal = es.Caudal,
                Salto = es.Salto,
                NumeroTurbinas = es.NumeroTurbinas,
                Codigo = er.Nombre,
                Tipo = te.Nombre,
                Ubicacion = pp.Ubicacion,
                Eficiencia = pp.Eficiencia,
                FechaCreacion = pp.FechaCreacion,
                Pais = ps.Nombre,
                EnergiaRequerida = ps.EnergiaRequerida,
                NivelCovertura = ps.NivelCovertura,
                Poblacion = ps.Poblacion,
                ProduccionAnual = es.Caudal *
                                  es.Salto *
                                  es.NumeroTurbinas
            };

        return [.. energiaHidroelectrica];
    }

    public decimal CalcularProduccion()
    {
        var resultados =
            from p in context.EnergiaHidroelectricas
            select new
            {
                p.Id,
                p.Salto,
                p.Caudal,
                p.NumeroTurbinas,
                TotalCalculo = p.Salto *
                               p.Caudal *
                               p.NumeroTurbinas
            };

        return resultados.Sum(x => x.TotalCalculo);
    }

    public async Task AgregarEntidadConRelacionesAsync(InsertarEnergiaHidroelectricaDto entidadDto)
    {
        if (entidadDto == null)
                throw new ArgumentNullException(nameof(entidadDto));

        var energiaHidroelectricaParams = new[]
        {
            new NpgsqlParameter("@Caudal", Math.Round(entidadDto.EnergiaHidroelectrica.Caudal, 2)),
            new NpgsqlParameter("@Salto", Math.Round(entidadDto.EnergiaHidroelectrica.Salto, 2)),
            new NpgsqlParameter("@NumeroTurbinas", entidadDto.EnergiaHidroelectrica.NumeroTurbinas)
        };

        var energiaRenovableParams = new[]
        {
            new NpgsqlParameter("@Nombre", entidadDto.EnergiaRenovable.Nombre)
        };

        var plantaProduccionParams = new[]
        {
            new NpgsqlParameter("@Ubicacion", entidadDto.PlantaProduccion.Ubicacion),
            new NpgsqlParameter("@Eficacia", Math.Round(entidadDto.PlantaProduccion.Eficiencia, 2)),
            new NpgsqlParameter("@CapacidadInstalada",
                Math.Round(entidadDto.PlantaProduccion.CapacidadInstalada, 2)),
            new NpgsqlParameter("@FechaCreacion", entidadDto.PlantaProduccion.FechaCreacion)
        };

        var paisParams = new[]
        {
            new NpgsqlParameter("@NombrePais", entidadDto.Pais.Nombre),
            new NpgsqlParameter("@NivelCovertura", Math.Round(entidadDto.Pais.NivelCovertura, 2)),
            new NpgsqlParameter("@Poblacion", Math.Round(entidadDto.Pais.Poblacion, 2)),
            new NpgsqlParameter("@EnergiaRequerida", Math.Round(entidadDto.Pais.EnergiaRequerida, 2))
        };
            
        // Usar SQL parametrizado con nombres consistentes
        await context.Database.ExecuteSqlRawAsync(
            "CALL insertar_energia_hidroelectrica(@Salto, @Caudal, @NumeroTurbinas, " +
            "@Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
            "@NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
            [.. energiaHidroelectricaParams, .. energiaRenovableParams, .. plantaProduccionParams, .. paisParams]);
    }

    public async Task ActualizarEntidadConRelacionesAsync(InsertarEnergiaHidroelectricaDto entidadDto, int id)
    {
        if (entidadDto == null)
            throw new ArgumentNullException(nameof(entidadDto));

        var energiaHidroelectricaParams = new[]
        {
            new NpgsqlParameter("@idEnergia", id),
            new NpgsqlParameter("@Caudal", Math.Round(entidadDto.EnergiaHidroelectrica.Caudal, 2)),
            new NpgsqlParameter("@Salto", Math.Round(entidadDto.EnergiaHidroelectrica.Salto, 2)),
            new NpgsqlParameter("@NumeroTurbinas", entidadDto.EnergiaHidroelectrica.NumeroTurbinas)
        };

        var energiaRenovableParams = new[]
        {
            new NpgsqlParameter("@Nombre", entidadDto.EnergiaRenovable.Nombre)
        };

        var plantaProduccionParams = new[]
        {
            new NpgsqlParameter("@Ubicacion", entidadDto.PlantaProduccion.Ubicacion),
            new NpgsqlParameter("@Eficacia", Math.Round(entidadDto.PlantaProduccion.Eficiencia, 2)),
            new NpgsqlParameter("@CapacidadInstalada",
                Math.Round(entidadDto.PlantaProduccion.CapacidadInstalada, 2)),
            new NpgsqlParameter("@FechaCreacion", entidadDto.PlantaProduccion.FechaCreacion)
        };

        var paisParams = new[]
        {
            new NpgsqlParameter("@NombrePais", entidadDto.Pais.Nombre),
            new NpgsqlParameter("@NivelCovertura", Math.Round(entidadDto.Pais.NivelCovertura, 2)),
            new NpgsqlParameter("@Poblacion", Math.Round(entidadDto.Pais.Poblacion, 2)),
            new NpgsqlParameter("@EnergiaRequerida", Math.Round(entidadDto.Pais.EnergiaRequerida, 2))
        };
            
        // Usar SQL parametrizado con nombres consistentes
        await context.Database.ExecuteSqlRawAsync(
            "CALL actualizar_energia_hidroelectrica(@idEnergia, @Salto, @Caudal, @NumeroTurbinas, " +
            "@Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
            "@NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
            [.. energiaHidroelectricaParams, .. energiaRenovableParams, .. plantaProduccionParams, .. paisParams]);
    }

    public async Task EliminarEntidadConRelacionesAsync(int id)
    {
        var energiaHidroelectrica = new[]
        {
            new NpgsqlParameter("@idenergia", id)
        };
            
        await context.Database.ExecuteSqlRawAsync(
            "CALL borrar_energia_solar(@idenergia)", [.. energiaHidroelectrica]
        );
    }
}