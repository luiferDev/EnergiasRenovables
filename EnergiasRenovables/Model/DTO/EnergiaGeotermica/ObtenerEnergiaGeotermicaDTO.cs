namespace EnergiasRenovables.Model.DTO.Biomasa;

public class ObtenerEnergiaGeotermicaDto
{
    public int Id { get; set; }
    public decimal Caudal { get; set; }
    public int NumeroPozos { get; set; }
    public decimal TemperaturaFluidos { get; set; }
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