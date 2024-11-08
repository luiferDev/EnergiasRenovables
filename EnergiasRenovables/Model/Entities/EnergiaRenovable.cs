namespace EnergiasRenovables.Model.Entities
{
    public class EnergiaRenovable
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int TipoEnergiaId { get; set; }

        public required PlantaProduccion PlantaProduccion { get; set; }
        public required EnergiaSolar EnergiaSolar { get; set; }
        public required Biomasa Biomasa { get; set; }
        public required EnergiaHidroelectrica EnergiaHidroelectrica { get; set; }
        public required EnergiaGeotermica EnergiaGeotermica { get; set; }
        public required EnergiaEolica EnergiaEolica { get; set; }
        public required TipoEnergia TipoEnergia { get; set; }
    }
}
