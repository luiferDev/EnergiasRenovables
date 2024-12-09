namespace EnergiasRenovables.Model.DTO.Biomasa;

public class InsertarBiomasaDto
{
    public required AgregarBiomasaDto Biomasa { get; set; }
    public required AgregarEnergiaRenovableSolarDto EnergiaRenovable { get; set; }
    public required AgregarPlantaProduccionDto PlantaProduccion { get; set; }
    public required AgregarPaisDto Pais { get; set; }
}