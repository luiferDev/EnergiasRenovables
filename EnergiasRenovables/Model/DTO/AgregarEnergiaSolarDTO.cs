using EnergiasRenovables.Model.Entities;

namespace EnergiasRenovables.Model.DTO
{
    public class AgregarEnergiaSolarDTO
    {
        public int Id { get; set; }
        public decimal RadiacionSolar { get; set; }
        public decimal AreaPaneles { get; set; }
        public decimal AnguloInclinacion { get; set; }
        public decimal EficienciaPaneles { get; set; }
        public decimal ProduccionEnergetica { get; set; }
    }
}
