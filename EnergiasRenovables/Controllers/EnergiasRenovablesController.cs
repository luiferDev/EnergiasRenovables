using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;
using EnergiasRenovables.Model.DTO.Biomasa;
using EnergiasRenovables.Model.Strategy.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnergiasRenovables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergiasRenovablesController(
        ApplicationDbContext dbContext,
        EnergiaRenovableContext<ObtenerEnergiSolarDto, InsertarEnergiaSolarDto?> solarContext,
        EnergiaRenovableContext<ObtenerEnergiaEolicaDto, InsertarEnergiaEolicaDto?> eolicaContext,
        EnergiaRenovableContext<ObtenerBiomasaDto, InsertarBiomasaDto?> biomasaContext,
        EnergiaRenovableContext<ObtenerEnergiaHidroelectricaDto, InsertarEnergiaHidroelectricaDto?> hidroContext,
        EnergiaRenovableContext<ObtenerEnergiaGeotermicaDto, InsertarEnergiaGeotermicaDto?> geoContext
        ) : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        [HttpGet]
        [Authorize]
        public IActionResult ObtenerEnergiaRenovable([FromQuery] string tipoEnergia)
        {
            try
            {
                switch (tipoEnergia.ToLower())
                {
                    case "solar":
                        
                        var energiaSolar = solarContext.ObtenerEnergia();
                        var produccionSolar = solarContext.CalcularEnergia();

                        var resultadoSolar = new
                        {
                            EnergiaSolar = energiaSolar,
                            ProduccionTotal = produccionSolar
                        };
                        return Ok(resultadoSolar);

                    case "eolica":

                        var energiaEolica = eolicaContext.ObtenerEnergia();
                        var produccionEolica = eolicaContext.CalcularEnergia();

                        var resultadoEolica = new
                        {
                            EnergiaEolica = energiaEolica,
                            ProduccionTotal = produccionEolica
                        };
                        return Ok(resultadoEolica);
                    
                    case "biomasa":

                        var biomasa = biomasaContext.ObtenerEnergia();
                        var produccionBiomasa = biomasaContext.CalcularEnergia();

                        var resultadoBiomasa = new
                        {
                            Biomasa = biomasa,
                            ProduccionTotal = produccionBiomasa
                        };
                        return Ok(resultadoBiomasa);

                    case "hidroelectrica":

                        var energiaHidroelectrica = hidroContext.ObtenerEnergia();
                        var produccionHidroelectrica = hidroContext.CalcularEnergia();

                        var resultadoHidro = new
                        {
                            hidroelectrica = energiaHidroelectrica,
                            produccionTotal = produccionHidroelectrica
                        };

                        return Ok(resultadoHidro);
                    
                    case "geotermica":

                        var energiaGeotermica = geoContext.ObtenerEnergia();
                        var produccionGeotermica = geoContext.CalcularEnergia();

                        var resultadoGeo = new
                        {
                            geotermica = energiaGeotermica,
                            produccionTotal = produccionGeotermica
                        };

                        return Ok(resultadoGeo);

                    default:
                        return BadRequest("Tipo de energía no válido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        [HttpPost("crear")]
        [Authorize]
        public async Task<IActionResult> CrearEnergiaRenovable(
                [FromQuery] string tipoEnergia,
                [FromBody] InsertarEnergiaDto insertarEnergia)
        {
            if (string.IsNullOrWhiteSpace(tipoEnergia))
            {
                return BadRequest("El tipo de energía es obligatorio en el query parameter.");
            }

            switch (tipoEnergia.ToLower())
            {
                case "solar":

                    await solarContext.InsertarEnergiaAsync(insertarEnergia.EnergiaSolar);
                    return Ok("Energía solar agregada correctamente.");

                case "eolica":

                    await eolicaContext.InsertarEnergiaAsync(insertarEnergia.EnergiaEolica);
                    return Ok("Energía eólica agregada correctamente.");
                
                case "biomasa" :

                    await biomasaContext.InsertarEnergiaAsync(insertarEnergia.Biomasa);
                    return Ok("Biomasa agregada correctamente");
                
                case "hidrelectrica":

                    await hidroContext.InsertarEnergiaAsync(insertarEnergia.Hidroelectrica);
                    return Ok("Energía Hidroelectrica agregada correctamente");
                
                case "geotermica":

                    await geoContext.InsertarEnergiaAsync(insertarEnergia.Geotermica);
                    return Ok("Energia Geotérmica agregada correctamente");

                default:
                    return BadRequest("Tipo de energía no válido.");
            }
        }

        [HttpPut("actualizar")]
        [Authorize]
        public async Task<IActionResult> ActualizarEnergiaRenovable(
            [FromQuery] string tipoEnergia, int id,
            [FromBody] InsertarEnergiaDto insertarEnergia)
        {
            if (string.IsNullOrWhiteSpace(tipoEnergia))
            {
                return BadRequest("El tipo de energía es obligatorio en el query parameter.");
            }

            switch (tipoEnergia.ToLower())
            {
                case "solar":
                    
                    await solarContext.ActualizarEnergiaAsync(insertarEnergia.EnergiaSolar, id);
                    return Ok("Energía solar actualizada correctamente.");

                case "eolica":

                    await eolicaContext.ActualizarEnergiaAsync(insertarEnergia.EnergiaEolica, id);
                    return Ok("Energía eólica actualizada correctamente.");
                
                case "biomasa":

                    await biomasaContext.ActualizarEnergiaAsync(insertarEnergia.Biomasa, id);
                    return Ok("Energia biomasa actualizada correctamente");
                    
                case "hidroelectrica":

                    await hidroContext.ActualizarEnergiaAsync(insertarEnergia.Hidroelectrica, id);
                    return Ok("Energia Hidroeléctrica actualizada correctamente");
                
                case "geotermica":

                    await geoContext.ActualizarEnergiaAsync(insertarEnergia.Geotermica, id);
                    return Ok("Energia Geotérmica actualizada correctamente");
                
                default:
                    return BadRequest("Tipo de energía no válido.");
            }
        }

        [HttpDelete("eliminar")]
        [Authorize]
        public async Task<IActionResult> EliminarEnergiaRenovable(
            [FromQuery] string tipoEnergia, int id)
        {
            if (string.IsNullOrWhiteSpace(tipoEnergia))
            {
                return BadRequest("El tipo de energía es obligatorio en el query parameter.");
            }

            switch (tipoEnergia.ToLower())
            {
                case "solar":

                    await solarContext.EliminarEnergiaAsync(id);
                    return Ok("Energía solar eliminada correctamente.");

                case "eolica":

                    await eolicaContext.EliminarEnergiaAsync(id);
                    return Ok("Energía eólica eliminada correctamente.");
                
                case "biomasa":

                    await biomasaContext.EliminarEnergiaAsync(id);
                    return Ok("Energia biomasa eliminada correcamente");
                
                case "hidroelectrica":

                    await hidroContext.EliminarEnergiaAsync(id);
                    return Ok("Energia Hidroelectrica eliminada correctamente");
                
                case "geotermica" :

                    await geoContext.EliminarEnergiaAsync(id);
                    return Ok("Energia Geotermica eliminada correctamente");

                default:
                    return BadRequest("Tipo de energía no válido.");
            }
        }

    }
}
