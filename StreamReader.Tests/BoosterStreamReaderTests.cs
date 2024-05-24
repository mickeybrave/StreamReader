using Moq;
using StreamReader.Core;

namespace StreamReader.Tests
{
    public class BoosterStreamReaderTests
    {
        [Fact]
        public void ReaderStreamInfoResult_NoException_Test()
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader();

            var exception = Record.Exception(() => boosterStreamReader.GetStreamInfo());
            Assert.Null(exception);

        }

        [Fact]
        public void ReaderStreamInfoResult_Negative_constractor_param_input_NoException_Test()
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(-1);

            var exception = Record.Exception(() => boosterStreamReader.GetStreamInfo());
            Assert.Null(exception);

        }

        [Fact]
        public void ReaderStreamInfoResult_Negative_input_NoException_No_Error_Test()
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(-1);

            var res = boosterStreamReader.GetStreamInfo();
            Assert.Null(res.Error.Message);

        }


        [Fact]
        public void ReaderStreamInfoResult_has_result_have_values_NoException_Test()
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader();

            var res = boosterStreamReader.GetStreamInfo();

            Assert.NotEqual(0, res.Result.WordsCount);
            Assert.NotEqual(0, res.Result.CharactersCount);
        }


        [Fact]
        public void ReaderStreamInfoResult_has_result_have_values_NoException_Custom_text_Test()
        {
            var text = "asd asdasd asdasdasd asdasdasdasd asdasdasdasdasd";
            var textProcessorMock = new Mock<ITextProcessor>();
            textProcessorMock.Setup(m => m.GetText(It.IsAny<int>())).Returns(text);

            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(It.IsAny<int>(), textProcessorMock.Object);

            var res = boosterStreamReader.GetStreamInfo();

            Assert.NotEqual(0, res.Result.WordsCount);
            Assert.NotEqual(0, res.Result.CharactersCount);

            Assert.Equal(text.Length, res.Result.CharactersCount);
            Assert.Equal(5, res.Result.WordsCount);
        }

        [Theory]
        [InlineData("asd asdasd", 2)]
        [InlineData("asd asdasd asdasdasd asdasdasdasd asdasdasdasdasd", 5)]
        public void ReaderStreamInfoResult_words_NoException_Custom_multi_text_Test(string text, int wordsCount)
        {

            var textProcessorMock = new Mock<ITextProcessor>();
            textProcessorMock.Setup(m => m.GetText(It.IsAny<int>())).Returns(text);

            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(It.IsAny<int>(), textProcessorMock.Object);

            var res = boosterStreamReader.GetStreamInfo();

            Assert.Equal(wordsCount, res.Result.WordsCount);
        }

        [Theory]
        [InlineData("asd asdasd asdasdasd asdasdasdasd asdasdasdasdasd", "asdasdasdasdasd", 0, 5)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "aasdasd", 0, 5)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "aasdasd", 0, 3)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "asd", 2, 5)]
        [InlineData("asdx aa asd aasdasd aa a d a a aasdasd c asd", "asdx", 2, 5)]
        [InlineData("asdxa asdx aa asd aasdasd aa a d a a aasdasd c asd", "asdx", 3, 5)]
        public void GetLargestWords_Test(string text, string word, int position, int wordsCount)
        {

            var textProcessorMock = new Mock<ITextProcessor>();
            textProcessorMock.Setup(m => m.GetText(It.IsAny<int>())).Returns(text);

            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(It.IsAny<int>(), textProcessorMock.Object);

            var res = boosterStreamReader.GetLargestWords(wordsCount);

            Assert.Equal(res.Result[position], word);
        }

        [Theory]
        [InlineData("asd asdasd asdasdasd asdasdasdasd asdasdasdasdasd", "asd", 0, 5)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "a", 0, 5)]
        [InlineData("asd aa asd aasdasd aa a d a a aasdasd c asd", "a", 0, 3)]
        [InlineData("asd aa asd aasdasd aa d a aasdasd c asd", "aa", 3, 5)]
        public void GetSmallestestWords_Test(string text, string word, int position, int wordsCount)
        {

            var textProcessorMock = new Mock<ITextProcessor>();
            textProcessorMock.Setup(m => m.GetText(It.IsAny<int>())).Returns(text);

            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(It.IsAny<int>(), textProcessorMock.Object);

            var res = boosterStreamReader.GetSmallestWords(wordsCount);

            Assert.Equal(res.Result[position], word);
        }

        [Theory]
        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Sharjah,Lahore,Cairo", "Dubai", 0, 5)]
        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Sharjah,Lahore,Cairo", "Lahore", 1, 5)]

        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Cairo,Cairo,Sharjah,Lahore,Cairo", "Cairo", 0, 5)]
        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Cairo,Cairo,Sharjah,Lahore,Cairo", "Dubai", 1, 5)]

        [InlineData("Dubai,Karachi,Lahore,Madrid,Dubai,Sydney,Cairo,Cairo,Sharjah,Lahore,Cairo", "Lahore", 2, 5)]

        public void GetMostFrequentlyAppearingWords_Test(string text, string word, int position, int wordsCount)
        {

            var textProcessorMock = new Mock<ITextProcessor>();
            textProcessorMock.Setup(m => m.GetText(It.IsAny<int>())).Returns(text);

            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(It.IsAny<int>(), textProcessorMock.Object);

            var res = boosterStreamReader.GetMostFrequentlyAppearingWords(wordsCount);

            Assert.Equal(res.Result[position], word);
        }

        [Theory]
        [InlineData("aaabbbbccccdddee", 16, 'a', 3)]
        [InlineData("aaabbbbccccdddee", 16, 'b', 4)]
        [InlineData("aaabbbbccccdddee", 16, 'c', 4)]
        [InlineData("aaabbbbccccdddee", 16, 'd', 3)]
        [InlineData("aaabbbbccccdddee", 16, 'e', 2)]
        [InlineData("aaQQQQ", 6, 'q', 4)]
        [InlineData("aaabbbbccccdddeeQQQQ", 20, 'q', 4)]
        [InlineData("aaa bbbb cccc ddd ee QQQQ", 25, 'q', 4)]
        public void GetCharacterInfo_Test(string text, int charCount, char ch, int frequency)
        {

            var textProcessorMock = new Mock<ITextProcessor>();
            textProcessorMock.Setup(m => m.GetText(It.IsAny<int>())).Returns(text);

            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(It.IsAny<int>(), textProcessorMock.Object);

            var res = boosterStreamReader.GetCharacterInfo();

            Assert.Equal(res.Result.AllCharacters.Count(), charCount);
            Assert.Equal(res.Result.CharactersFrequency[ch], frequency);
        }



        [Theory]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        [InlineData(100000)]
        public void ReaderStreamInfoResult_has_result_validate_character_Count_Test(int bytesCount)
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(bytesCount);

            var res = boosterStreamReader.GetStreamInfo();


            Assert.NotEqual(0, res.Result.CharactersCount);
            Assert.Equal(bytesCount, res.Result.CharactersCount);

        }

        [Fact]
        public void GetLargestWords_has_No_errors_Negative_method_param_Test()
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader();

            var exception = Record.Exception(() => boosterStreamReader.GetLargestWords(5));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void GetLargestWords_has_result_not_empty_Test(int wordsCount)
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader();

            var res = boosterStreamReader.GetLargestWords(wordsCount);

            Assert.NotEmpty(res.Result);


        }
        [Theory]
        [InlineData(100, 5)]
        [InlineData(1000, 10)]
        [InlineData(10000, 100)]
        public void GetLargestWords_has_result_not_empty_short_text_Test(int textLenth, int wordsCount)
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(textLenth);

            var res = boosterStreamReader.GetLargestWords(wordsCount);

            Assert.NotEmpty(res.Result);


        }

        [Theory]
        [InlineData(100, 5)]
        [InlineData(1000, 10)]
        [InlineData(10000, 100)]
        public void GetSmallestWords_has_result_not_empty_short_text_Test(int textLenth, int wordsCount)
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(textLenth);

            var res = boosterStreamReader.GetSmallestWords(wordsCount);

            Assert.NotEmpty(res.Result);


        }

        [Theory]
        [InlineData(100, 10)]
        [InlineData(100, 5)]
        [InlineData(1000, 10)]
        [InlineData(10000, 100)]
        public void GetMostFrequentlyAppearingWords_has_result_not_empty_short_text_Test(int textLenth, int wordsCount)
        {
            BoosterStreamReader boosterStreamReader = new BoosterStreamReader(textLenth);

            var res = boosterStreamReader.GetMostFrequentlyAppearingWords(wordsCount);

            Assert.NotEmpty(res.Result);
        }


    }
}