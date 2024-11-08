namespace EnergiasRenovables.Model.Entities
{
    public class Biomasa
    {
        public int Id { get; set; }
        public required string Origen { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ContenidoEnergetico { get; set; }
        public required string MetodoConversion { get; set; }

        public required EnergiaRenovable EnergiaRenovable { get; set; }
    }
}
