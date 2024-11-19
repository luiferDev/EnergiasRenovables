namespace EnergiasRenovables.Model.Strategy.Context
{
    public class EnergiaRenovableContext<T, U>
    {
        private readonly ICalculoStrategy<T, U> _strategy;

        public EnergiaRenovableContext(ICalculoStrategy<T, U> strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public decimal CalcularEnergia()
        {
            return _strategy.CalcularProduccion();
        }

        public List<T> ObtenerEnergia()
        {
            return _strategy.ObtenerEnergia();
        }

        public async Task InsertarEnergiaAsync(U entidad)
        {
            await _strategy.AgregarEntidadConRelacionesAsync(entidad);
        }

        public async Task ActualizarEnergiaAsync(U entidad, int id)
        {
            await _strategy.ActualizarEntidadConRelacionesAsync(entidad, id);
        }

        public async Task EliminarEnergiaAsync(int id)
        {
            await _strategy.EliminarEntidadConRelacionesAsync(id);
        }
    }
}