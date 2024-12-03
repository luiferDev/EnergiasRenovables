namespace EnergiasRenovables.Model.DTO
{
    public class ObtenerEnergiSolarDTO
    {
        public int Id { get; set; }
        public decimal RadiacionSolar { get; set; }
        public decimal AnguloInclinacion { get; set; }
        public decimal EficienciaPaneles { get; set; }
        public decimal AreaPaneles { get; set; }
        public string? Codigo { get; set; }
        public string? Tipo { get; set; }
        public string? Ubicacion { get; set; }
        public decimal Eficiencia { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public string? Pais { get; set; }
        public decimal EnergiaRequerida { get; set; }
        public decimal NivelCovertura { get; set; }
        public decimal Poblacion { get; set; }

        public decimal ProduccionAnual { get; set; }
    }
}