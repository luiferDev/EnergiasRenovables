using Microsoft.AspNetCore.Http;
using EnergiasRenovables.Model.Strategy.Context;
using Microsoft.AspNetCore.Mvc;
using EnergiasRenovables.Data;
//using EnergiasRenovables.Model.Entities;
//using EnergiasRenovables.Model.DTO;

namespace EnergiasRenovables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergiaSolarController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EnergiaSolarController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var energiaSolar = dbContext.EnergiaSolars.ToList();
            return Ok(energiaSolar);
        }

        //[HttpPost("{energia}")]
        //private IActionResult crearEnergiaSolar(AgregarEnergiaSolarDTO agregarEnergiaSolarDTO)
        //{
        //    var energiaSolar = new EnergiaSolar()
        //    {
        //        RadiacionSolar = agregarEnergiaSolarDTO.RadiacionSolar,
        //        AreaPaneles = agregarEnergiaSolarDTO.AreaPaneles,
        //        AnguloInclinacion = agregarEnergiaSolarDTO.AnguloInclinacion,
        //        EficienciaPaneles = agregarEnergiaSolarDTO.EficienciaPaneles,
        //        //EnergiaRenovable = agregarEnergiaSolarDTO.AddEnergiaRenovable
        //    };

        //    dbContext.EnergiaSolars.Add(energiaSolar);
        //    dbContext.SaveChanges();
        //    return Ok(energiaSolar);
        //}
    }
}
