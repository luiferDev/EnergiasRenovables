using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy
{
    public class EnergiaSolarConcrete : ICalculoStrategy<ObtenerEnergiSolarDTO, 
        InsertarEnergiaSolarDTO>
    {
        private readonly ApplicationDbContext _context;

        public EnergiaSolarConcrete(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ObtenerEnergiSolarDTO> ObtenerEnergia()
        {
            var energiaSolar = from es in _context.EnergiaSolars
                               join er in _context.EnergiasRenovables on es.Id equals er.Id
                               join te in _context.TipoEnergias on er.TipoEnergiaId equals te.Id
                               join pp in _context.PlantaProduccions on es.Id equals pp.Id
                               join ps in _context.Paises on es.Id equals ps.id
                               select new ObtenerEnergiSolarDTO
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
            var resultados = from p in _context.EnergiaSolars
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

        // ejecutar procedimiento almacenado asincrono para insertar energia solar en db
        public async Task AgregarEntidadConRelacionesAsync(InsertarEnergiaSolarDTO entidadDto)
        {
            // Parámetros para Energia Solar
            var energiaSolar = new[]
            {
            new SqlParameter("@radiacionsolar", SqlDbType.Decimal)
            {
                Value = Math.Round(entidadDto.EnergiaSolar.RadiacionSolar,2),
                Precision = 5,
                Scale = 2
            },
            new SqlParameter("@anguloinclinacion", SqlDbType.Decimal)
            {
                Value = Math.Round(entidadDto.EnergiaSolar.AnguloInclinacion,2),
                Precision = 5,
                Scale = 2
            },
            new SqlParameter("@eficienciapaneles", SqlDbType.Decimal)
            {
                Value = Math.Round(entidadDto.EnergiaSolar.EficienciaPaneles,2),
                Precision = 5,
                Scale = 2
            },
            new SqlParameter("@areapaneles", SqlDbType.Decimal)
            {
                Value = Math.Round(entidadDto.EnergiaSolar.AreaPaneles,2),
                Precision = 5,
                Scale = 2
            }
        };

            // Parámetros para Energia Renovable
            var energiaRenovable = new[]
            {
            new SqlParameter("@nombre", entidadDto.EnergiaRenovable.Nombre)
        };

            // Parámetros para planta de produccion
            var plantaProduccion = new[]
            {
            new SqlParameter("@ubicacion", entidadDto.PlantaProduccion.Ubicacion),
            new SqlParameter("@eficacia", SqlDbType.Decimal)
        {
            Value = Math.Round(entidadDto.PlantaProduccion.Eficiencia,2),
            Precision = 5,
            Scale = 2
        },
        new SqlParameter("@capacidadinstalada", SqlDbType.Decimal)
        {
                Value = Math.Round(entidadDto.PlantaProduccion.CapacidadInstalada,2),
                Precision = 5,
                Scale = 2
        },
            new SqlParameter("@fechacreacion", entidadDto.PlantaProduccion.FechaCreacion)
        };

            var pais = new[]
            {
            new SqlParameter("@nombrepais", entidadDto.Pais.Nombre),
            new SqlParameter("@nivelcovertura", SqlDbType.Decimal)
            {
                Value = Math.Round(entidadDto.Pais.NivelCovertura, 2),
                Precision = 5,
                Scale = 2
            },
            new SqlParameter("@poblacion", SqlDbType.Decimal)
            {
                Value = Math.Round(entidadDto.Pais.Poblacion,2),
                Precision = 5,
                Scale = 2
            },
            new SqlParameter("@energiaRequerida", SqlDbType.Decimal)
            {
                Value = Math.Round(entidadDto.Pais.EnergiaRequerida),
                Precision = 5,
                Scale = 2
            }
        };
            // Ejecuta el procedimiento almacenado
            await _context.Database.ExecuteSqlRawAsync(
                  "EXEC Insertar_energia_solar @radiacionsolar, " +
                  " @areapaneles, @anguloinclinacion, @eficienciapaneles, " +
                  " @nombre, @ubicacion, @capacidadinstalada, @eficacia, @fechacreacion, " +
                  " @nombrepais, @energiaRequerida, @nivelcovertura, @poblacion",
                  [.. energiaSolar, .. energiaRenovable, .. plantaProduccion, .. pais]);
        }

        public async Task ActualizarEntidadConRelacionesAsync(InsertarEnergiaSolarDTO entidadDto, int id)
        {
            var energiaSolar = new[]
            {
                new SqlParameter("@idEnergiaSolar", id),
                new SqlParameter("@radiacionsolar", SqlDbType.Decimal)
                {
                    Value = Math.Round(entidadDto.EnergiaSolar.RadiacionSolar,2),
                    Precision = 5,
                    Scale = 2
                },
                new SqlParameter("@anguloinclinacion", SqlDbType.Decimal)
                {
                    Value = Math.Round(entidadDto.EnergiaSolar.AnguloInclinacion,2),
                    Precision = 5,
                    Scale = 2
                },
                new SqlParameter("@eficienciapaneles", SqlDbType.Decimal)
                {
                    Value = Math.Round(entidadDto.EnergiaSolar.EficienciaPaneles,2),
                    Precision = 5,
                    Scale = 2
                },
                new SqlParameter("@areapaneles", SqlDbType.Decimal)
                {
                    Value = Math.Round(entidadDto.EnergiaSolar.AreaPaneles,2),
                    Precision = 5,
                    Scale = 2
                }
            };

            // Parámetros para Energia Renovable
            var energiaRenovable = new[]
            {
                new SqlParameter("@nombre", entidadDto.EnergiaRenovable.Nombre)
            };

            // Parámetros para planta de produccion
            var plantaProduccion = new[]
            {
                new SqlParameter("@ubicacion", entidadDto.PlantaProduccion.Ubicacion),
                new SqlParameter("@eficacia", SqlDbType.Decimal)
                {
                    Value = Math.Round(entidadDto.PlantaProduccion.Eficiencia,2),
                    Precision = 5,
                    Scale = 2
                },
                new SqlParameter("@capacidadinstalada", SqlDbType.Decimal)
                {
                        Value = Math.Round(entidadDto.PlantaProduccion.CapacidadInstalada,2),
                        Precision = 5,
                        Scale = 2
                },
                new SqlParameter("@fechacreacion", entidadDto.PlantaProduccion.FechaCreacion)
            };

            var pais = new[]
            {
                new SqlParameter("@nombrepais", entidadDto.Pais.Nombre),
                new SqlParameter("@nivelcovertura", SqlDbType.Decimal)
                {
                    Value = Math.Round(entidadDto.Pais.NivelCovertura, 2),
                    Precision = 5,
                    Scale = 2
                },
                new SqlParameter("@poblacion", SqlDbType.Decimal)
                {
                    Value = Math.Round(entidadDto.Pais.Poblacion,2),
                    Precision = 5,
                    Scale = 2
                },
                new SqlParameter("@energiaRequerida", SqlDbType.Decimal)
                {
                    Value = Math.Round(entidadDto.Pais.EnergiaRequerida),
                    Precision = 5,
                    Scale = 2
                }
            };
            // Ejecuta el procedimiento almacenado
            await _context.Database.ExecuteSqlRawAsync(
                  "EXEC actualizar_energia_solar @idEnergiaSolar, @radiacionsolar, " +
                  " @areapaneles, @anguloinclinacion, @eficienciapaneles, " +
                  " @nombre, @ubicacion, @capacidadinstalada, @eficacia, @fechacreacion, " +
                  " @nombrepais, @energiaRequerida, @nivelcovertura, @poblacion",
                  [.. energiaSolar, .. energiaRenovable, .. plantaProduccion, .. pais]);
        }

        public async Task EliminarEntidadConRelacionesAsync(int id)
        {
            var energiaSolar = new[]
            {
                new SqlParameter("@idEnegiaSolar", id)
            };
            await _context.Database.ExecuteSqlRawAsync(
                  "EXEC borrar_energia_solar @idEnegiaSolar"
                  , energiaSolar);
        }
    }
}
