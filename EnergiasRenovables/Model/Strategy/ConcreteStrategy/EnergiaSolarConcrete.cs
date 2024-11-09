using EnergiasRenovables.Data;
using Microsoft.EntityFrameworkCore;

namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy
{
    public class EnergiaSolarConcrete : ICalculoStrategy
    {
        private readonly ApplicationDbContext _context;

        public EnergiaSolarConcrete(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal CalcularProduccion()
        {
            var resultados = from p in _context.EnergiaSolars
                             select new
                             {
                                 p.Id,
                                 p.AreaPaneles,
                                 p.RadiacionSolar,
                                 p.AnguloInclinacion,
                                 p.EficienciaPaneles,
                                 TotalCalculo = p.AreaPaneles * 
                                 p.RadiacionSolar * 
                                 p.AnguloInclinacion * 
                                 p.EficienciaPaneles
                             };

            foreach (var item in resultados)
            {
                Console.WriteLine($"ID: {item.Id}, " +
                    $"Area Paneles: {item.AreaPaneles}, " +
                    $"Radiacion Solar: {item.RadiacionSolar}, " +
                    $"Angulo Inclinacion: {item.AnguloInclinacion}, " +
                    $"Eficiencia Paneles: {item.EficienciaPaneles}, " +
                    $"Total Calculo: {item.TotalCalculo}");
            }
            
            return resultados.Sum(x => x.TotalCalculo);
        }
    }
}
