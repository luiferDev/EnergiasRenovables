
namespace EnergiasRenovables.Model.Entities
{
    public class EnergiaSolar
    {
        public int Id { get; set; }
        public decimal RadiacionSolar { get; set; }
        public decimal AreaPaneles { get; set; }
        public decimal AnguloInclinacion { get; set; }
        public decimal EficienciaPaneles { get; set; }

        public virtual EnergiaRenovable EnergiaRenovable { get; set; }
    }
}
