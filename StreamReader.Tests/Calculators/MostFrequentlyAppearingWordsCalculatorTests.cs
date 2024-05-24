using Moq;
using StreamReader.Core;

namespace StreamReader.Tests.Calculators
{
    public class MostFrequentlyAppearingWordsCalculatorTests
    {


        [Theory]
        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Sharjah,Lahore,Cairo", "Dubai", 0, 5)]
        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Sharjah,Lahore,Cairo", "Lahore", 1, 5)]

        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Cairo,Cairo,Sharjah,Lahore,Cairo", "Cairo", 0, 5)]
        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Cairo,Cairo,Sharjah,Lahore,Cairo", "Dubai", 1, 5)]

        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Cairo,Cairo,Sharjah,Lahore,Cairo", "Lahore", 2, 5)]

        public void GetMostFrequentlyAppearingWords_Test(string text, string word, int position, int wordsCount)
        {

            var mostFrequentlyAppearingWordsCalculator = new MostFrequentlyAppearingWordsCalculator(wordsCount);

            var res = (WordInfo)mostFrequentlyAppearingWordsCalculator.GetStreamInfo(text);

            Assert.Equal(res.Info[position], word);
        }



        [Fact]
        public void GetMostFrequentlyAppearingWords_Test_0_count_tests()
        {
            var mostFrequentlyAppearingWordsCalculator = new MostFrequentlyAppearingWordsCalculator(0);

            var res = (WordInfo)mostFrequentlyAppearingWordsCalculator.GetStreamInfo("text");

            Assert.Empty(res.Info);
        }

        [Fact]
        public void GetMostFrequentlyAppearingWords_Test_Negative_no_error_count_tests()
        {
            var mostFrequentlyAppearingWordsCalculator = new MostFrequentlyAppearingWordsCalculator(-1);
            var exception = Record.Exception(() => mostFrequentlyAppearingWordsCalculator.GetStreamInfo("text"));
            Assert.Null(exception);

        }



    }
}