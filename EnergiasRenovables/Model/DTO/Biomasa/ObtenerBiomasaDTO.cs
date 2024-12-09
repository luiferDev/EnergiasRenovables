namespace EnergiasRenovables.Model.DTO.Biomasa;

public class ObtenerBiomasaDto
{
    public int Id { get; set; }
    public required string Origen { get; set; }
    public decimal Cantidad { get; set; }
    public decimal ContenidoEnergetico { get; set; }
    public required string MetodoConversion { get; set; }
    public string? Codigo { get; set; }
    public string? Tipo { get; set; }
    public string? Ubicacion { get; set; }
    public decimal Eficiencia { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public string? Pais { get; set; }
    public decimal EnergiaRequerida { get; set; }
    public decimal NivelCovertura { get; set; }
    public decimal Poblacion { get; set; }

    public decimal ProduccionAnual { get; set; }
    
}