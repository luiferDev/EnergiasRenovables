namespace EnergiasRenovables.Model.Entities
{
    public class EnergiaEolica
    {
        public int Id { get; set; }
        public int NumeroTurbinas { get; set; }
        public decimal VelocidadViento { get; set; }
        public decimal AlturaTurbinas { get; set; }
        public decimal DiametroTurbina { get; set; }

        public required EnergiaRenovable EnergiaRenovable { get; set; }
    }
}
