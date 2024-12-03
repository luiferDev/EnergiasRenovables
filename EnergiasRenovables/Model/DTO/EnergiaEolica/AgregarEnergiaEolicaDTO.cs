namespace EnergiasRenovables.Model.DTO
{
    public class AgregarEnergiaEolicaDTO
    {
        public int Id { get; set; }
        public int NumeroTurbinas { get; set; }
        public decimal VelocidadViento { get; set; }
        public decimal AlturaTurbinas { get; set; }
        public decimal DiametroTurbina { get; set; }

        public decimal ProduccionEnergetica { get; set; }
    }
}
