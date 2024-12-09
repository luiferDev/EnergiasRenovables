namespace EnergiasRenovables.Model.DTO.Biomasa;

public class InsertarEnergiaHidroelectricaDto
{
    public required AgregarEnergiaHidroelectricaDto EnergiaHidroelectrica { get; set; }
    public required AgregarEnergiaRenovableSolarDto EnergiaRenovable { get; set; }
    public required AgregarPlantaProduccionDto PlantaProduccion { get; set; }
    public required AgregarPaisDto Pais { get; set; }
}