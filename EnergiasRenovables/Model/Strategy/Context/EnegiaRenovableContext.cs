using EnergiasRenovables.Model.DTO;

namespace EnergiasRenovables.Model.Strategy.Context
{
    public class EnergiaRenovableContext<T, TU>(ICalculoStrategy<T, TU> strategy)
    {
        private readonly ICalculoStrategy<T, TU> _strategy = 
            strategy ?? throw new ArgumentNullException(nameof(strategy));

        public decimal CalcularEnergia()
        {
            return _strategy.CalcularProduccion();
        }

        public List<T> ObtenerEnergia()
        {
            return _strategy.ObtenerEnergia();
        }

        public async Task InsertarEnergiaAsync(TU entidad)
        {
            await _strategy.AgregarEntidadConRelacionesAsync(entidad);
        }

        public async Task ActualizarEnergiaAsync(TU entidad, int id)
        {
            await _strategy.ActualizarEntidadConRelacionesAsync(entidad, id);
        }

        public async Task EliminarEnergiaAsync(int id)
        {
            await _strategy.EliminarEntidadConRelacionesAsync(id);
        }
    }
}