namespace EnergiasRenovables.Model.DTO.Biomasa;

public class AgregarBiomasaDto
{
    public int Id { get; set; }
    public required string Origen { get; set; }
    public decimal Cantidad { get; set; }
    public decimal ContenidoEnergetico { get; set; }
    public required string MetodoConversion { get; set; }

    public decimal ProduccionEnergetica { get; set; }
}