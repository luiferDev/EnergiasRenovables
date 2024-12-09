using EnergiasRenovables.Model.Entities;

namespace EnergiasRenovables.Model.DTO
{
    public class AgregarPaisDto
    {
        public required string Nombre { get; set; }
        public decimal EnergiaRequerida { get; set; }
        public decimal NivelCovertura { get; set; }
        public decimal Poblacion { get; set; }
    }
}
