using EnergiasRenovables.Model.DTO.Biomasa;

namespace EnergiasRenovables.Model.DTO
{
    public class InsertarEnergiaDto
    {
        public InsertarEnergiaSolarDto? EnergiaSolar { get; set; }
        public InsertarEnergiaEolicaDto? EnergiaEolica { get; set; }
        public InsertarBiomasaDto? Biomasa { get; set; }
        public InsertarEnergiaHidroelectricaDto? Hidroelectrica { get; set; }
        public InsertarEnergiaGeotermicaDto? Geotermica { get; set; }
    }
}
