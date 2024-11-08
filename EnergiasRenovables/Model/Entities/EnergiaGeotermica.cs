namespace EnergiasRenovables.Model.Entities
{
    public class EnergiaGeotermica
    {
        public int Id { get; set; }
        public decimal Caudal { get; set; }
        public int NumeroPozos { get; set; }
        public decimal TemperaturaFluidos { get; set; }

        public required EnergiaRenovable EnergiaRenovable { get; set; }
    }
}
