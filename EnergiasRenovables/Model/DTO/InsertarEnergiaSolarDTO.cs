namespace EnergiasRenovables.Model.DTO
{
    public class InsertarEnergiaSolarDTO
    {
        public required AgregarEnergiaSolarDTO EnergiaSolar { get; set; }
        public required AgregarEnergiaRenovableSolarDTO EnergiaRenovable { get; set; }
        public required AgregarPlantaProduccionDTO PlantaProduccion { get; set; }
        public required AgregarPaisDTO Pais { get; set; }
    }
}
