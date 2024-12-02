using EnergiasRenovables.Model.DTO.Biomasa;

namespace EnergiasRenovables.Model.DTO
{
    public class InsertarEnergiaDTO
    {
        public InsertarEnergiaSolarDTO? EnergiaSolar { get; set; }
        public InsertarEnergiaEolicaDTO? EnergiaEolica { get; set; }
        public InsertarBiomasaDTO? Biomasa { get; set; }
        public InsertarEnergiaHidroelectricaDTO? Hidroelectrica { get; set; }
        public InsertarEnergiaGeotermicaDTO? Geotermica { get; set; }
    }
}
