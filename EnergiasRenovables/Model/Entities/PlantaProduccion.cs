namespace EnergiasRenovables.Model.Entities
{
    public class PlantaProduccion
    {
        public int Id { get; set; }
        public required string Ubicacion { get; set; }
        public decimal CapacidadInstalada { get; set; }
        public decimal Eficiencia { get; set; }
        public DateOnly FechaCreacion { get; set; }

        public required EnergiaRenovable EnergiaRenovable { get; set; }

        public required Pais Pais { get; set; }
    }
}
