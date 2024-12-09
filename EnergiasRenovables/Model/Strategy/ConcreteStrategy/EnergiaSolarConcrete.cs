using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy
{
    public class EnergiaSolarConcrete(ApplicationDbContext context) : ICalculoStrategy<ObtenerEnergiSolarDto,
        InsertarEnergiaSolarDto>
    {
        public List<ObtenerEnergiSolarDto> ObtenerEnergia()
        {
            var energiaSolar = from es in context.EnergiaSolars
                join er in context.EnergiasRenovables on es.Id equals er.Id
                join te in context.TipoEnergias on er.TipoEnergiaId equals te.Id
                join pp in context.PlantaProduccions on es.Id equals pp.Id
                join ps in context.Paises on es.Id equals ps.Id
                select new ObtenerEnergiSolarDto
                {
                    Id = es.Id,
                    RadiacionSolar = es.RadiacionSolar,
                    AnguloInclinacion = es.AnguloInclinacion,
                    EficienciaPaneles = es.EficienciaPaneles,
                    AreaPaneles = es.AreaPaneles,
                    Codigo = er.Nombre,
                    Tipo = te.Nombre,
                    Ubicacion = pp.Ubicacion,
                    Eficiencia = pp.Eficiencia,
                    FechaCreacion = pp.FechaCreacion,
                    Pais = ps.Nombre,
                    EnergiaRequerida = ps.EnergiaRequerida,
                    NivelCovertura = ps.NivelCovertura,
                    Poblacion = ps.Poblacion,
                    ProduccionAnual = es.RadiacionSolar *
                                      es.AnguloInclinacion *
                                      es.EficienciaPaneles *
                                      es.AreaPaneles
                };

            return [.. energiaSolar];
        }

        public decimal CalcularProduccion()
        {
            var resultados =
                from p in context.EnergiaSolars
                select new
                {
                    p.Id,
                    p.AreaPaneles,
                    p.RadiacionSolar,
                    p.AnguloInclinacion,
                    p.EficienciaPaneles,
                    TotalCalculo = p.AreaPaneles *
                                   p.RadiacionSolar *
                                   p.AnguloInclinacion *
                                   p.EficienciaPaneles
                };

            return resultados.Sum(x => x.TotalCalculo);
        }

        public async Task AgregarEntidadConRelacionesAsync(InsertarEnergiaSolarDto entidadDto)
        {
            if (entidadDto == null)
                throw new ArgumentNullException(nameof(entidadDto));

            var energiaSolarParams = new[]
            {
                new NpgsqlParameter("@RadiacionSolar", Math.Round(entidadDto.EnergiaSolar.RadiacionSolar, 2)),
                new NpgsqlParameter("@AnguloInclinacion", Math.Round(entidadDto.EnergiaSolar.AnguloInclinacion, 2)),
                new NpgsqlParameter("@EficienciaPaneles", Math.Round(entidadDto.EnergiaSolar.EficienciaPaneles, 2)),
                new NpgsqlParameter("@AreaPaneles", Math.Round(entidadDto.EnergiaSolar.AreaPaneles, 2))
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
                "CALL insertar_energia_solar(@RadiacionSolar, @AnguloInclinacion, @EficienciaPaneles, @AreaPaneles, " +
                "@Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
                "@NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
                [.. energiaSolarParams, .. energiaRenovableParams, .. plantaProduccionParams, .. paisParams]);
        }
        
        public async Task ActualizarEntidadConRelacionesAsync(InsertarEnergiaSolarDto entidadDto, int id)
        {
            // Parámetros para Energia Solar
            var energiaSolar = new[]
            {
                new NpgsqlParameter("@idEnergiaSolar", id),
                new NpgsqlParameter("@RadiacionSolar",
                    Math.Round(entidadDto.EnergiaSolar.RadiacionSolar, 2)),
                new NpgsqlParameter("@AnguloInclinacion",
                    Math.Round(entidadDto.EnergiaSolar.AnguloInclinacion, 2)),
                new NpgsqlParameter("@EficienciaPaneles",
                    Math.Round(entidadDto.EnergiaSolar.EficienciaPaneles, 2)),
                new NpgsqlParameter("@AreaPaneles",
                    Math.Round(entidadDto.EnergiaSolar.AreaPaneles, 2))
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
                "CALL actualizar_energia_solar (@idEnergiaSolar, @RadiacionSolar, " +
                " @AreaPaneles, @AnguloInclinacion, @EficienciaPaneles, " +
                " @Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
                " @NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
                [.. energiaSolar, .. energiaRenovable, .. plantaProduccion, .. pais]);
        }

        public async Task EliminarEntidadConRelacionesAsync(int id)
        {
            var energiaSolar = new[]
            {
                new NpgsqlParameter("@idenergiaSolar", id)
            };
            
            await context.Database.ExecuteSqlRawAsync(
                "CALL borrar_energia_solar(@idenergiaSolar)", [.. energiaSolar]
                );
        }
    }
}