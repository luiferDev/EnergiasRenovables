namespace EnergiasRenovables.Model.DTO
{
    public class InsertarEnergiaSolarDto
    {
        public required AgregarEnergiaSolarDto EnergiaSolar { get; set; }
        public required AgregarEnergiaRenovableSolarDto EnergiaRenovable { get; set; }
        public required AgregarPlantaProduccionDto PlantaProduccion { get; set; }
        public required AgregarPaisDto Pais { get; set; }
    }
}
