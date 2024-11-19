using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy
{
    public class EnergiaEolicaConcrete : ICalculoStrategy<ObtenerEnergiaEolicaDTO, 
        InsertarEnergiaEolicaDTO>
    {
        private readonly ApplicationDbContext _context;

        public EnergiaEolicaConcrete(ApplicationDbContext context)
        {
            this._context = context;
        }

        public decimal CalcularProduccion()
        {
            var resultados = from e in _context.EnergiaEolicas
                             select new
                             {
                               e.Id,
                               e.DiametroTurbina,
                               e.AlturaTurbinas,
                               TotalCalculo =  e.DiametroTurbina * e.AlturaTurbinas * 1000
                             };
            return resultados.Sum(x => x.TotalCalculo);
        }

        public List<ObtenerEnergiaEolicaDTO> ObtenerEnergia()
        {
            throw new NotImplementedException();
        }

        public Task AgregarEntidadConRelacionesAsync(InsertarEnergiaEolicaDTO entidadDto)
        {
            throw new NotImplementedException();
        }

        public Task ActualizarEntidadConRelacionesAsync(InsertarEnergiaEolicaDTO entidadDto, int id)
        {
            throw new NotImplementedException();
        }

        public Task EliminarEntidadConRelacionesAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
