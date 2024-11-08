using EnergiasRenovables.Model.Entities;

namespace EnergiasRenovables.Model.DTO
{
    public class AgregarEnergiaSolarDTO
    {
        public decimal RadiacionSolar { get; set; }
        public decimal AreaPaneles { get; set; }
        public decimal AnguloInclinacion { get; set; }
        public decimal EficienciaPaneles { get; set; }
        public decimal ProduccionEnergetica { get; set; }

        public required AgregarEnergiaRenovableSolarDTO AddEnergiaRenovable { get; set; }
    }
}
