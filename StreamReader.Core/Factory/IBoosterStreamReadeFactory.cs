namespace StreamReader.Core
{
    public interface IBoosterStreamReadeFactory
    {
        IStreamInfoCalculator GetCalculator(CalculationTypes calculationTypes, int counter = 0);
    }
}
