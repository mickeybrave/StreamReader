﻿namespace StreamReader.Core
{
    public class BoosterStreamReaderFactory : IBoosterStreamReadeFactory
    {
        public IStreamInfoCalculator GetCalculator(CalculationTypes calculationTypes,
            int counter = 0)
        {
            switch (calculationTypes)
            {
                case CalculationTypes.StreamInfo:
                    return new StreamInfoCalculator();
                case CalculationTypes.CharInfo:
                    return new CharacterInfoCalculator();
                case CalculationTypes.LargestWord:
                    return new LargestWordCalculator(counter);
                case CalculationTypes.SmallestWord:
                   return new SmallestWordCalculator(counter);
                case CalculationTypes.MostFrequentlyAppearingWord:
                    return new MostFrequentlyAppearingWordsCalculator(counter);
            }

            throw new NotImplementedException($"Given type {calculationTypes} is not supported");
        }
    }

}
