namespace EnergiasRenovables.Model.DTO
{
    public class InsertarEnergiaEolicaDTO
    {
        public required AgregarEnergiaEolicaDTO EnergiaEolica { get; set; }
        public required AgregarEnergiaRenovableSolarDTO EnergiaRenovable { get; set; }
        public required AgregarPlantaProduccionDTO PlantaProduccion { get; set; }
        public required AgregarPaisDTO Pais { get; set; }
    }
}
