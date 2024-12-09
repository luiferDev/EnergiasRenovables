namespace EnergiasRenovables.Model.DTO
{
    public class ObtenerEnergiaEolicaDto
    {
        public int Id { get; set; }
        public int NumeroTurbinas { get; set; }
        public decimal VelocidadViento { get; set; }
        public decimal AlturaTurbinas { get; set; }
        public decimal DiametroTurbina { get; set; }
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
