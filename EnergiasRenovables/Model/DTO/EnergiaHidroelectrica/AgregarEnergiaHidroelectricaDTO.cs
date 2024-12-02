namespace EnergiasRenovables.Model.DTO.Biomasa;

public class AgregarEnergiaHidroelectricaDTO
{
    public int Id { get; set; }
    public decimal Salto { get; set; }
    public decimal Caudal { get; set; }
    public int NumeroTurbinas { get; set; }

    public decimal ProduccionEnergetica { get; set; }
}