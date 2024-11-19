using EnergiasRenovables.Data;

namespace EnergiasRenovables.Services
{
    public class EnergiaSolarService
    {
        public ApplicationDbContext Context { get; set; }

        public EnergiaSolarService(ApplicationDbContext applicationDbContext)
        {
            Context = applicationDbContext;
        }

        public void ObtenerEnergiaSolar()
        {
            var energiaSolar = from es in Context.EnergiaSolars
                               join er in Context.EnergiasRenovables on es.Id equals er.Id
                               join te in Context.TipoEnergias on er.TipoEnergiaId equals te.Id
                               join pp in Context.PlantaProduccions on es.Id equals pp.Id
                               join ps in Context.Paises on es.Id equals ps.id
                               select new
                               {
                                   es.Id,
                                   es.RadiacionSolar,
                                   es.AnguloInclinacion,
                                   es.EficienciaPaneles,
                                   Codigo = er.Nombre,
                                   Tipo = te.Nombre,
                                   pp.Ubicacion,
                                   pp.Eficiencia,
                                   pp.FechaCreacion,
                                   Pais = ps.Nombre,
                                   ps.EnergiaRequerida,
                                   ps.NivelCovertura,
                                   ps.Poblacion
                               };

            if (energiaSolar == null)
            {
                throw new Exception("No se encontraron datos");
            }
            energiaSolar.ToList();
        }
    }
}
