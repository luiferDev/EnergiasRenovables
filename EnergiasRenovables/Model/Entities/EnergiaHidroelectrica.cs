namespace EnergiasRenovables.Model.Entities
{
    public class EnergiaHidroelectrica
    {
        public int Id { get; set; }
        public decimal Salto { get; set; }
        public decimal Caudal { get; set; }
        public int NumeroTurbinas { get; set; }

        public required EnergiaRenovable EnergiaRenovable { get; set; }
    }
}
