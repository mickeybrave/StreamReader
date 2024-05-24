using Moq;
using StreamReader.Core;
using static System.Net.Mime.MediaTypeNames;

namespace StreamReader.Tests.Calculators
{
    public class SmallestWordCalculatorTests
    {
        [Theory]
        [InlineData("asd asdasd asdasdasd asdasdasdasd asdasdasdasdasd", "asd", 0, 5)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "a", 0, 5)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "a", 0, 3)]
        [InlineData("asd aa asd aasdasd aa d a aasdasd c asd", "aa", 3, 5)]
        public void GetSmallestestWords_Test(string text, string word, int position, int wordsCount)
        {

            var smallestWordCalculator = new SmallestWordCalculator(wordsCount);

            var res = (WordInfo)smallestWordCalculator.GetStreamInfo(text);

            Assert.Equal(res.Info[position], word);
        }

        [Fact]
        public void GetSmallestestWords_Test_Negative_no_error_count_tests()
        {
            var smallestWordCalculator = new SmallestWordCalculator(-1);
            var exception = Record.Exception(() => smallestWordCalculator.GetStreamInfo("text"));
            Assert.Null(exception);

        }

        [Fact]
        public void GetSmallestestWords_Test_0_count_tests()
        {
            var smallestWordCalculator = new SmallestWordCalculator(0);

            var res = (WordInfo)smallestWordCalculator.GetStreamInfo("text");

            Assert.Empty(res.Info);

        }
    }
}