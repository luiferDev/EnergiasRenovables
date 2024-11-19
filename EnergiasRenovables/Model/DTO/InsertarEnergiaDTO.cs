namespace EnergiasRenovables.Model.DTO
{
    public class InsertarEnergiaDTO
    {
        public required InsertarEnergiaSolarDTO EnergiaSolar { get; set; }
        public required InsertarEnergiaEolicaDTO EnergiaEolica { get; set; }
    }
}
