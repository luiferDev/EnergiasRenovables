using EnergiasRenovables.Model.Entities;

namespace EnergiasRenovables.Model.DTO
{
    public class AgregarPlantaProduccionDto
    {
        public required string Ubicacion { get; set; }
        public decimal CapacidadInstalada { get; set; }
        public decimal Eficiencia { get; set; }
        public DateOnly FechaCreacion { get; set; }
    }
}
