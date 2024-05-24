using StreamReader.Core;

namespace StreamReader.Tests.Calculators
{
    public class LargestWordCalculatorTests
    {
        [Theory]
        [InlineData("asd asdasd asdasdasd asdasdasdasd asdasdasdasdasd", "asdasdasdasdasd", 0, 5)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "aasdasd", 0, 5)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "aasdasd", 0, 3)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "asd", 2, 5)]
        [InlineData("asdx aa asd aasdasd aa a d a a aasdasd c asd", "asdx", 2, 5)]
        [InlineData("asdxa asdx aa asd aasdasd aa a d a a aasdasd c asd", "asdx", 3, 5)]
        public void GetLargestWords_Test(string text, string word, int position, int wordsCount)
        {
            var largestWordCalculator = new LargestWordCalculator(wordsCount);

            var res = (WordInfo)largestWordCalculator.GetStreamInfo(text);

            Assert.Equal(res.Info[position], word);
        }

        [Fact]
        public void GetLargestWords_Test_Negative_no_error_count_tests()
        {
            var largestWordCalculator = new LargestWordCalculator(-1);
            var exception = Record.Exception(() => largestWordCalculator.GetStreamInfo("text"));
            Assert.Null(exception);
        }
    }
}