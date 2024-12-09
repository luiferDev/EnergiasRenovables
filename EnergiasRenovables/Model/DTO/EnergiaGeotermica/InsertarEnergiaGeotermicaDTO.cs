namespace EnergiasRenovables.Model.DTO.Biomasa;

public class InsertarEnergiaGeotermicaDto
{
    public required AgregarEnergiaGeotermicaDto EnergiaGeotermica { get; set; }
    public required AgregarEnergiaRenovableSolarDto EnergiaRenovable { get; set; }
    public required AgregarPlantaProduccionDto PlantaProduccion { get; set; }
    public required AgregarPaisDto Pais { get; set; }
}