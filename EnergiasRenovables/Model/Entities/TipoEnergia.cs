namespace EnergiasRenovables.Model.Entities
{
    public class TipoEnergia
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        public virtual List<EnergiaRenovable> EnergiasRenovables { get; set; } = new();
    }
}
