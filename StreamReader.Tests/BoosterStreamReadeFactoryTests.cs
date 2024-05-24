using Moq;
using StreamReader.Core;

namespace StreamReader.Tests
{
    public class BoosterStreamReadeFactoryTests
    {

        private readonly BoosterStreamReaderFactory _factory = new BoosterStreamReaderFactory();

        [Fact]
        public void BoosterStreamReadeFactory_GetCalculator_Validate_CharInfo_return_type()
        {
            var result = _factory.GetCalculator(CalculationTypes.CharInfo);
            Assert.True(result.GetType() == typeof(CharacterInfoCalculator));
        }

        [Fact]
        public void BoosterStreamReadeFactory_GetCalculator_Validate_StreamInfo_return_type()
        {
            var result = _factory.GetCalculator(CalculationTypes.StreamInfo);
            Assert.True(result.GetType() == typeof(StreamInfoCalculator));
        }

        [Fact]
        public void BoosterStreamReadeFactory_GetCalculator_Validate_LargestWord_return_type()
        {
            var result = _factory.GetCalculator(CalculationTypes.LargestWord);
            Assert.True(result.GetType() == typeof(LargestWordCalculator));
        }

        [Fact]
        public void BoosterStreamReadeFactory_GetCalculator_Validate_SmallestWord_return_type()
        {
            var result = _factory.GetCalculator(CalculationTypes.SmallestWord);
            Assert.True(result.GetType() == typeof(SmallestWordCalculator));
        }

        [Fact]
        public void BoosterStreamReadeFactory_GetCalculator_Validate_MostFrequentlyAppearingWord_return_type()
        {
            var result = _factory.GetCalculator(CalculationTypes.MostFrequentlyAppearingWord);
            Assert.True(result.GetType() == typeof(MostFrequentlyAppearingWordsCalculator));
        }

        [Fact]
        public void BoosterStreamReadeFactory_GetCalculator_Validate_Not_Equal_to_invalid_type_return_type()
        {
            var result = _factory.GetCalculator(CalculationTypes.CharInfo);
            Assert.True(result.GetType() != typeof(MostFrequentlyAppearingWordsCalculator));
        }


    }
}