namespace EnergiasRenovables.Model.Entities
{
    public class Pais
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public decimal EnergiaRequerida { get; set; }
        public decimal NivelCovertura { get; set; }
        public decimal Poblacion { get; set; }
        public int PlantaProduccionId { get; set; }

        public required List<PlantaProduccion> PlantaProduccion { get; set; }
    }
}
