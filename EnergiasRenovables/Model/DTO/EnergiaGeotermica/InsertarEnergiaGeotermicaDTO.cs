namespace EnergiasRenovables.Model.DTO.Biomasa;

public class InsertarEnergiaGeotermicaDTO
{
    public required AgregarEnergiaGeotermicaDTO EnergiaGeotermica { get; set; }
    public required AgregarEnergiaRenovableSolarDTO EnergiaRenovable { get; set; }
    public required AgregarPlantaProduccionDTO PlantaProduccion { get; set; }
    public required AgregarPaisDTO Pais { get; set; }
}