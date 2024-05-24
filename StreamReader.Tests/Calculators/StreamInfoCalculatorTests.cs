using StreamReader.Core;

namespace StreamReader.Tests.Calculators
{
    public class StreamInfoCalculatorTests
    {

        private readonly StreamInfoCalculator _calculator = new StreamInfoCalculator();

        [Fact]
        public void BoosterStreamReader_has_result_have_values_NoException_Test()
        {
            var res = (StreamInfo)_calculator.GetStreamInfo("a");

            Assert.NotEqual(0, res.WordsCount);
            Assert.NotEqual(0, res.CharactersCount);
        }


        [Theory]
        [InlineData("asd asdasd", 2, 10)]
        [InlineData("asdasd", 1, 6)]
        [InlineData("asd asdasd asdasdasd asdasdasdasd asdasdasdasdasd", 5, 49)]
        public void BoosterStreamReader_has_result_custom_text_Test(string text,
            int wordsCount, int charCount)
        {

            var res = (StreamInfo)_calculator.GetStreamInfo(text);

            Assert.Equal(wordsCount, res.WordsCount);
            Assert.Equal(charCount, res.CharactersCount);
        }
    }
}