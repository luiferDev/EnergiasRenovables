using EnergiasRenovables.Model.Strategy.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnergiasRenovables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergiasRenovablesControlador() : ControllerBase
    {
        EnegiaRenovableContext context = new();

        [HttpGet]
        public IActionResult ObtenerProduccion()
        {
            var energiaTotal = context.CalcularProduccion(EnegiaRenovableContext.TipoEnergia.Solar);
            return Ok(energiaTotal);
        }
    }
}
