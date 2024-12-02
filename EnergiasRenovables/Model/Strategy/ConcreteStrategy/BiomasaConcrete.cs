using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO.Biomasa;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy;

public class BiomasaConcrete(ApplicationDbContext context): ICalculoStrategy<ObtenerBiomasaDTO, InsertarBiomasaDTO>
{
    public List<ObtenerBiomasaDTO> ObtenerEnergia()
    {
        var biomasa = from es in context.Biomasa
            join er in context.EnergiasRenovables on es.Id equals er.Id
            join te in context.TipoEnergias on er.TipoEnergiaId equals te.Id
            join pp in context.PlantaProduccions on es.Id equals pp.Id
            join ps in context.Paises on es.Id equals ps.id
            select new ObtenerBiomasaDTO()
            {
                Id = es.Id,
                Origen = es.Origen,
                Cantidad = es.Cantidad,
                ContenidoEnergetico = es.ContenidoEnergetico,
                MetodoConversion = es.MetodoConversion,
                Codigo = er.Nombre,
                Tipo = te.Nombre,
                Ubicacion = pp.Ubicacion,
                Eficiencia = pp.Eficiencia,
                FechaCreacion = pp.FechaCreacion,
                Pais = ps.Nombre,
                EnergiaRequerida = ps.EnergiaRequerida,
                NivelCovertura = ps.NivelCovertura,
                Poblacion = ps.Poblacion,
                ProduccionAnual = es.Cantidad *
                                  es.ContenidoEnergetico 
            };

        return [.. biomasa];
    }

    public decimal CalcularProduccion()
    {
        var resultados =
            from p in context.Biomasa
            select new
            {
                p.Id,
                p.Origen,
                p.Cantidad,
                p.MetodoConversion,
                p.ContenidoEnergetico,
                TotalCalculo = p.Cantidad *
                               p.ContenidoEnergetico
            };

        return resultados.Sum(x => x.TotalCalculo);
    }

    public async Task AgregarEntidadConRelacionesAsync(InsertarBiomasaDTO entidadDto)
    {
        if (entidadDto == null)
                throw new ArgumentNullException(nameof(entidadDto));

        var biomasaParams = new[]
        {
            new NpgsqlParameter("@Origen", entidadDto.Biomasa.Origen),
            new NpgsqlParameter("@Cantidad", Math.Round(entidadDto.Biomasa.Cantidad, 2)),
            new NpgsqlParameter("@ContenidoEnergetico", Math.Round(entidadDto.Biomasa.ContenidoEnergetico, 2)),
            new NpgsqlParameter("@MetodoConversion", entidadDto.Biomasa.MetodoConversion)
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
            "CALL insertar_biomasa(@Origen, @Cantidad, @ContenidoEnergetico, @MetodoConversion, " +
            "@Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
            "@NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
            [.. biomasaParams, .. energiaRenovableParams, .. plantaProduccionParams, .. paisParams]);
    }

    public async Task ActualizarEntidadConRelacionesAsync(InsertarBiomasaDTO entidadDto, int id)
    {
        var biomasaParams = new[]
            {
                new NpgsqlParameter("@idBiomasa", id),
                new NpgsqlParameter("@Origen", entidadDto.Biomasa.Origen),
                new NpgsqlParameter("@Cantidad", Math.Round(entidadDto.Biomasa.Cantidad, 2)),
                new NpgsqlParameter("@ContenidoEnergetico", Math.Round(entidadDto.Biomasa.ContenidoEnergetico, 2)),
                new NpgsqlParameter("@MetodoConversion", entidadDto.Biomasa.MetodoConversion)
            };
            // Parámetros para Energia Renovable
            var energiaRenovable = new[]
            {
                new NpgsqlParameter("@Nombre", entidadDto.EnergiaRenovable.Nombre)
            };

            // Parámetros para planta de producción
            var plantaProduccion = new[]
            {
                new NpgsqlParameter("@Ubicacion", entidadDto.PlantaProduccion.Ubicacion),
                new NpgsqlParameter("@Eficacia", Math.Round(entidadDto.PlantaProduccion.Eficiencia, 2)),
                new NpgsqlParameter("@CapacidadInstalada",
                    Math.Round(entidadDto.PlantaProduccion.CapacidadInstalada, 2)),
                new NpgsqlParameter("@FechaCreacion", entidadDto.PlantaProduccion.FechaCreacion)
            };

            // Parámetros para País
            var pais = new[]
            {
                new NpgsqlParameter("@NombrePais", entidadDto.Pais.Nombre),
                new NpgsqlParameter("@NivelCovertura", Math.Round(entidadDto.Pais.NivelCovertura, 2)),
                new NpgsqlParameter("@Poblacion", Math.Round(entidadDto.Pais.Poblacion, 2)),
                new NpgsqlParameter("@EnergiaRequerida", Math.Round(entidadDto.Pais.EnergiaRequerida, 2))
            };

            // Ejecuta el procedimiento almacenado
            await context.Database.ExecuteSqlRawAsync(
                "CALL actualizar_biomasa(@idBiomasa, @Origen, " +
                " @Cantidad, @ContenidoEnergetico, @MetodoConversion, " +
                " @Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
                " @NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
                [.. biomasaParams, .. energiaRenovable, .. plantaProduccion, .. pais]);
    }

    public async Task EliminarEntidadConRelacionesAsync(int id)
    {
        var biomasa = new[]
        {
            new NpgsqlParameter("@idBiomasa", id)
        };
            
        await context.Database.ExecuteSqlRawAsync(
            "CALL borrar_energia_solar(@idBiomasa)", [.. biomasa]
        );
    }
}