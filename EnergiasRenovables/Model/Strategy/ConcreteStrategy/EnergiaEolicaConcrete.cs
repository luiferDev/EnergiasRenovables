using System.Data;
using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy
{
    public class EnergiaEolicaConcrete(ApplicationDbContext context) : ICalculoStrategy<ObtenerEnergiaEolicaDTO,
        InsertarEnergiaEolicaDTO>
    {
        public decimal CalcularProduccion()
        {
            var resultados = from e in context.EnergiaEolicas
                             select new
                             {
                               e.Id,
                               e.DiametroTurbina,
                               e.AlturaTurbinas,
                               TotalCalculo =  e.DiametroTurbina * e.AlturaTurbinas * 1000
                             };
            return resultados.Sum(x => x.TotalCalculo);
        }

        public List<ObtenerEnergiaEolicaDTO> ObtenerEnergia()
        {
            var energiaEolica = from es in context.EnergiaEolicas
                join er in context.EnergiasRenovables on es.Id equals er.Id
                join te in context.TipoEnergias on er.TipoEnergiaId equals te.Id
                join pp in context.PlantaProduccions on es.Id equals pp.Id
                join ps in context.Paises on es.Id equals ps.id
                select new ObtenerEnergiaEolicaDTO
                {
                    Id = es.Id,
                    NumeroTurbinas = es.NumeroTurbinas,
                    VelocidadViento = es.VelocidadViento,
                    AlturaTurbinas = es.AlturaTurbinas,
                    DiametroTurbina = es.DiametroTurbina,
                    Codigo = er.Nombre,
                    Tipo = te.Nombre,
                    Ubicacion = pp.Ubicacion,
                    Eficiencia = pp.Eficiencia,
                    FechaCreacion = pp.FechaCreacion,
                    Pais = ps.Nombre,
                    EnergiaRequerida = ps.EnergiaRequerida,
                    NivelCovertura = ps.NivelCovertura,
                    Poblacion = ps.Poblacion,
                    ProduccionAnual = es.AlturaTurbinas *
                                      es.DiametroTurbina * 1000
                };

            return [.. energiaEolica];
        }

        public async Task AgregarEntidadConRelacionesAsync(InsertarEnergiaEolicaDTO entidadDto)
        {
            if (entidadDto == null)
                throw new ArgumentNullException(nameof(entidadDto));

            var energiaEolicaParams = new[]
            {
                new NpgsqlParameter("@NumeroTurbinas", entidadDto.EnergiaEolica.NumeroTurbinas),
                new NpgsqlParameter("@VelocidadViento", Math.Round(entidadDto.EnergiaEolica.VelocidadViento, 2)),
                new NpgsqlParameter("@AlturaTurbinas", Math.Round(entidadDto.EnergiaEolica.AlturaTurbinas, 2)),
                new NpgsqlParameter("@DiametroTurbina", Math.Round(entidadDto.EnergiaEolica.DiametroTurbina, 2))
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
                "CALL insertar_energia_eolica (@NumeroTurbinas, @VelocidadViento, @AlturaTurbinas, @DiametroTurbina, " +
                "@Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
                "@NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
                [.. energiaEolicaParams, .. energiaRenovableParams, .. plantaProduccionParams, .. paisParams]);
        }

        public async Task ActualizarEntidadConRelacionesAsync(InsertarEnergiaEolicaDTO entidadDto, int id)
        {
            if (entidadDto == null)
                throw new ArgumentNullException(nameof(entidadDto));

            var energiaEolicaParams = new[]
            {
                new NpgsqlParameter("@idEnergiaEolica", id),
                new NpgsqlParameter("@NumeroTurbinas", entidadDto.EnergiaEolica.NumeroTurbinas),
                new NpgsqlParameter("@VelocidadViento", Math.Round(entidadDto.EnergiaEolica.VelocidadViento, 2)),
                new NpgsqlParameter("@AlturaTurbinas", Math.Round(entidadDto.EnergiaEolica.AlturaTurbinas, 2)),
                new NpgsqlParameter("@DiametroTurbina", Math.Round(entidadDto.EnergiaEolica.DiametroTurbina, 2))
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
                "CALL actualizar_energia_eolica (@idEnergiaSolar, @NumeroTurbinas, @VelocidadViento, @AlturaTurbinas, @DiametroTurbina, " +
                "@Nombre, @Ubicacion, @CapacidadInstalada, @Eficacia, @FechaCreacion, " +
                "@NombrePais, @EnergiaRequerida, @NivelCovertura, @Poblacion)",
                [.. energiaEolicaParams, .. energiaRenovableParams, .. plantaProduccionParams, .. paisParams]);
        }

        public async Task EliminarEntidadConRelacionesAsync(int id)
        {
            var energiaEolica = new[]
            {
                new NpgsqlParameter("@idEnergiaSolar", id)
            };
            
            await context.Database.ExecuteSqlRawAsync(
                "CALL borrar_energia_eolica(@idEnergiaSolar)", [.. energiaEolica]);
        }
    }
}
