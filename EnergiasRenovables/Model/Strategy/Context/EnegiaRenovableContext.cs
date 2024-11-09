using EnergiasRenovables.Model.Strategy.ConcreteStrategy;
using Microsoft.EntityFrameworkCore;

namespace EnergiasRenovables.Model.Strategy.Context
{
    public class EnegiaRenovableContext
    {

        private ICalculoStrategy _strategy;

        public void SetStrategy(ICalculoStrategy strategy)
        {
            // estrategia por defecto
            this._strategy = strategy;
        }

        public decimal CalcularEnergia()
        {
            return _strategy.CalcularProduccion();
        }

    }
}
