using EnergiasRenovables.Model.Strategy.ConcreteStrategy;
using Microsoft.EntityFrameworkCore;

namespace EnergiasRenovables.Model.Strategy.Context
{
    public class EnegiaRenovableContext
    {

        private ICalculoStrategy _strategy;

        public enum TipoEnergia {
            Solar,
            Eolica,
            Geotermica,
            Hidroelectrica,
            Biomasa
        }

        public EnegiaRenovableContext()
        {
            // estrategia por defecto
            this._strategy = new EnergiaSolarConcrete();
        }

        public decimal CalcularProduccion(TipoEnergia option)
        {
            switch (option)
            {
                case TipoEnergia.Solar:
                    this._strategy = new EnergiaSolarConcrete();
                    break;
                case TipoEnergia.Eolica:
                    this._strategy = new EnergiaEolicaConcrete();
                    break;             
            }

           return this._strategy.CalcularProduccion();
        }


    }
}
