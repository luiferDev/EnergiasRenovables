namespace EnergiasRenovables.Model.DTO.Biomasa;

public class AgregarEnergiaGeotermicaDTO
{
    public int Id { get; set; }
    public decimal Caudal { get; set; }
    public int NumeroPozos { get; set; }
    public decimal TemperaturaFluidos { get; set; }

    public decimal ProduccionEnergetica { get; set; }
}