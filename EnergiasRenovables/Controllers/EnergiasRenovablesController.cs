using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;
using EnergiasRenovables.Model.Strategy.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnergiasRenovables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergiasRenovablesController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        [HttpGet]
        public IActionResult ObtenerEnergiaRenovable([FromQuery] string tipoEnergia)
        {
            try
            {
                switch (tipoEnergia.ToLower())
                {
                    case "solar":
                        var solarContext = HttpContext.RequestServices
                            .GetRequiredService<EnergiaRenovableContext
                            <ObtenerEnergiSolarDTO, InsertarEnergiaSolarDTO>>();

                        var energiaSolar = solarContext.ObtenerEnergia();
                        var produccionSolar = solarContext.CalcularEnergia();

                        var resultadoSolar = new
                        {
                            EnergiaSolar = energiaSolar,
                            ProduccionTotal = produccionSolar
                        };
                        return Ok(resultadoSolar);

                    case "eolica":
                        var eolicaContext = HttpContext.RequestServices
                            .GetRequiredService<EnergiaRenovableContext
                            <ObtenerEnergiaEolicaDTO, InsertarEnergiaEolicaDTO>>();

                        var energiaEolica = eolicaContext.ObtenerEnergia();
                        var produccionEolica = eolicaContext.CalcularEnergia();

                        var resultadoEolica = new
                        {
                            EnergiaEolica = energiaEolica,
                            ProduccionTotal = produccionEolica
                        };
                        return Ok(resultadoEolica);

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
        public async Task<IActionResult> CrearEnergiaRenovable(
                [FromQuery] string tipoEnergia,
                [FromBody] InsertarEnergiaDTO insertarEnergia)
        {
            if (string.IsNullOrWhiteSpace(tipoEnergia))
            {
                return BadRequest("El tipo de energía es obligatorio en el query parameter.");
            }

            switch (tipoEnergia.ToLower())
            {
                case "solar":
                    if (insertarEnergia.EnergiaSolar == null)
                    {
                        return BadRequest("Los datos de energía solar son requeridos.");
                    }

                    var solarContext = HttpContext.RequestServices
                        .GetRequiredService
                        <EnergiaRenovableContext
                        <ObtenerEnergiSolarDTO, InsertarEnergiaSolarDTO>>();

                    await solarContext.InsertarEnergiaAsync(insertarEnergia.EnergiaSolar);
                    return Ok("Energía solar agregada correctamente.");

                case "eolica":
                    if (insertarEnergia.EnergiaEolica == null)
                    {
                        return BadRequest("Los datos de energía eólica son requeridos.");
                    }

                    var eolicaContext = HttpContext.RequestServices
                        .GetRequiredService
                        <EnergiaRenovableContext
                        <ObtenerEnergiaEolicaDTO, InsertarEnergiaEolicaDTO>>();

                    await eolicaContext.InsertarEnergiaAsync(insertarEnergia.EnergiaEolica);
                    return Ok("Energía eólica agregada correctamente.");

                default:
                    return BadRequest("Tipo de energía no válido.");
            }
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> ActualizarEnergiaRenovable(
            [FromQuery] string tipoEnergia, int id,
            [FromBody] InsertarEnergiaDTO insertarEnergia)
        {
            if (string.IsNullOrWhiteSpace(tipoEnergia))
            {
                return BadRequest("El tipo de energía es obligatorio en el query parameter.");
            }

            switch (tipoEnergia.ToLower())
            {
                case "solar":
                    if (insertarEnergia.EnergiaSolar == null)
                    {
                        return BadRequest("Los datos de energía solar son requeridos.");
                    }

                    var solarContext = HttpContext.RequestServices
                        .GetRequiredService
                        <EnergiaRenovableContext
                        <ObtenerEnergiSolarDTO, InsertarEnergiaSolarDTO>>();

                    await solarContext.ActualizarEnergiaAsync(insertarEnergia.EnergiaSolar, id);
                    return Ok("Energía solar actualizada correctamente.");

                case "eolica":
                    if (insertarEnergia.EnergiaEolica == null)
                    {
                        return BadRequest("Los datos de energía eólica son requeridos.");
                    }

                    var eolicaContext = HttpContext.RequestServices
                        .GetRequiredService
                        <EnergiaRenovableContext
                        <ObtenerEnergiaEolicaDTO, InsertarEnergiaEolicaDTO>>();

                    await eolicaContext.ActualizarEnergiaAsync(insertarEnergia.EnergiaEolica, id);
                    return Ok("Energía eólica actualizada correctamente.");

                default:
                    return BadRequest("Tipo de energía no válido.");
            }
        }

        [HttpDelete("eliminar")]
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
                    var solarContext = HttpContext.RequestServices
                        .GetRequiredService
                        <EnergiaRenovableContext
                        <ObtenerEnergiSolarDTO, InsertarEnergiaSolarDTO>>();

                    await solarContext.EliminarEnergiaAsync(id);
                    return Ok("Energía solar eliminada correctamente.");

                case "eolica":
                    var eolicaContext = HttpContext.RequestServices
                        .GetRequiredService
                        <EnergiaRenovableContext
                        <ObtenerEnergiaEolicaDTO, InsertarEnergiaEolicaDTO>>();

                    await eolicaContext.EliminarEnergiaAsync(id);
                    return Ok("Energía eólica eliminada correctamente.");

                default:
                    return BadRequest("Tipo de energía no válido.");
            }
        }

    }
}
