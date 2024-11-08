namespace EnergiasRenovables.Model.Strategy.ConcreteStrategy
{
    public class EnergiaEolicaConcrete : ICalculoStrategy
    {

        public EnergiaEolicaConcrete() { }

        public decimal CalcularProduccion()
        {
            decimal radiacion = 450.2M;
            decimal eficiencia = 0.9M;
            decimal area = 120.70M;
            decimal angulo = 49.23M;
            return area * angulo * eficiencia * radiacion;
        }
    }
}
