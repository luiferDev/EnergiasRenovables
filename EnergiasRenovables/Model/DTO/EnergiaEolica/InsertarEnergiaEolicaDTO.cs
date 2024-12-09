namespace EnergiasRenovables.Model.DTO
{
    public class InsertarEnergiaEolicaDto
    {
        public required AgregarEnergiaEolicaDto EnergiaEolica { get; set; }
        public required AgregarEnergiaRenovableSolarDto EnergiaRenovable { get; set; }
        public required AgregarPlantaProduccionDto PlantaProduccion { get; set; }
        public required AgregarPaisDto Pais { get; set; }
    }
}
