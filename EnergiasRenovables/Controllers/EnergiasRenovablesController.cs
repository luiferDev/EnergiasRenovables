using EnergiasRenovables.Data;
using EnergiasRenovables.Model.Strategy.ConcreteStrategy;
using EnergiasRenovables.Model.Strategy.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnergiasRenovables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergiasRenovablesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EnergiasRenovablesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult ResultadoCalculo(string tipoEnergia) 
        {
            var context = new EnegiaRenovableContext();

            if (tipoEnergia == "solar")
            {
                context.SetStrategy(new EnergiaSolarConcrete(dbContext));
            }
            else if (tipoEnergia == "eolica")
            {
                context.SetStrategy(new EnergiaEolicaConcrete(dbContext));
            }

            var result = context.CalcularEnergia();

            return Ok(result);
        }

    }
}
