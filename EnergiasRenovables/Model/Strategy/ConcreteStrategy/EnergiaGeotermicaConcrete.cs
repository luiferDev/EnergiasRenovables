using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO.Biomasa;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy;

public class EnergiaGeotermicaConcrete(ApplicationDbContext context): ICalculoStrategy<ObtenerEnergiaGeotermicaDto, InsertarEnergiaGeotermicaDto>
{
    public List<ObtenerEnergiaGeotermicaDto> ObtenerEnergia()
    {
        var energiaGeotermica = from es in context.EnergiaGeotermicas
            join er in context.EnergiasRenovables on es.Id equals er.Id
            join te in context.TipoEnergias on er.TipoEnergiaId equals te.Id
            join pp in context.PlantaProduccions on es.Id equals pp.Id
            join ps in context.Paises on es.Id equals ps.Id
            select new ObtenerEnergiaGeotermicaDto()
            {
                Id = es.Id,
                Caudal = es.Caudal,
                NumeroPozos = es.NumeroPozos,
                TemperaturaFluidos = es.TemperaturaFluidos,
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
                                  es.NumeroPozos *
                                  es.TemperaturaFluidos
            };

        return [.. energiaGeotermica];
    }

    public decimal CalcularProduccion()
    {
        var resultados =
            from p in context.EnergiaGeotermicas
            select new
            {
                p.Id,
                p.Caudal,
                p.NumeroPozos,
                p.TemperaturaFluidos,
                TotalCalculo = p.Caudal *
                               p.NumeroPozos *
                               p.TemperaturaFluidos
            };

        return resultados.Sum(x => x.TotalCalculo);
    }

    public async Task AgregarEntidadConRelacionesAsync(InsertarEnergiaGeotermicaDto entidadDto)
    {
        if (entidadDto == null)
                throw new ArgumentNullException(nameof(entidadDto));

        var enegiaGeotermicaParams = new[]
        {
            new NpgsqlParameter("@Caudal", Math.Round(entidadDto.EnergiaGeotermica.Caudal, 2)),
            new NpgsqlParameter("@NumeroPozos", entidadDto.EnergiaGeotermica.NumeroPozos),
            new NpgsqlParameter("@TemperaturaFluidos", Math.Round(entidadDto.EnergiaGeotermica.TemperaturaFluidos, 2))
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
            "CALL insertar_energia_geotermica(@Caudal, @NumeroPozos, @TemperaturaFluidos,  " +
            "@Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
            "@NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
            [.. enegiaGeotermicaParams, .. energiaRenovableParams, .. plantaProduccionParams, .. paisParams]);
    }

    public async Task ActualizarEntidadConRelacionesAsync(InsertarEnergiaGeotermicaDto entidadDto, int id)
    {
        if (entidadDto == null)
            throw new ArgumentNullException(nameof(entidadDto));

        var enegiaGeotermicaParams = new[]
        {
            new NpgsqlParameter("@idEnergia", id),
            new NpgsqlParameter("@Caudal", Math.Round(entidadDto.EnergiaGeotermica.Caudal, 2)),
            new NpgsqlParameter("@NumeroPozos", entidadDto.EnergiaGeotermica.NumeroPozos),
            new NpgsqlParameter("@TemperaturaFluidos", Math.Round(entidadDto.EnergiaGeotermica.TemperaturaFluidos, 2))
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
            "CALL actualizar_energia_geotermica(@idEnergia, @Caudal, @NumeroPozos, @TemperaturaFluidos,  " +
            "@Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
            "@NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
            [.. enegiaGeotermicaParams, .. energiaRenovableParams, .. plantaProduccionParams, .. paisParams]);
    }

    public async Task EliminarEntidadConRelacionesAsync(int id)
    {
        var energiaGeotermica = new[]
        {
            new NpgsqlParameter("@idenergia", id)
        };
            
        await context.Database.ExecuteSqlRawAsync(
            "CALL borrar_energia_solar(@idenergia)", [.. energiaGeotermica]
        );
    }
}