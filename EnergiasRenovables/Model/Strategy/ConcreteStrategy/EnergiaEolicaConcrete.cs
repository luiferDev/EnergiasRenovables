using EnergiasRenovables.Data;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy
{
    public class EnergiaEolicaConcrete : ICalculoStrategy
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
    }
}
