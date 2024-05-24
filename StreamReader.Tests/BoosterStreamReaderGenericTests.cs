using Moq;
using StreamReader.Core;

namespace StreamReader.Tests
{
    public class BoosterStreamReaderGenericTests
    {
        [Fact]
        public void ReaderStreamInfoResult_NoException_Test()
        {
            BoosterStreamReaderGeneric boosterStreamReader = new BoosterStreamReaderGeneric();

            var exception = Record.Exception(() => boosterStreamReader.GetStreamInfo());
            Assert.Null(exception);

        }

        [Fact]
        public void ReaderStreamInfoResult_Negative_constractor_param_input_NoException_Test()
        {
            BoosterStreamReaderGeneric boosterStreamReader = new BoosterStreamReaderGeneric(-1);

            var exception = Record.Exception(() => boosterStreamReader.GetStreamInfo());
            Assert.Null(exception);

        }


        [Fact]
        public void ReaderStreamInfoResult_has_result_have_values_NoException_Test()
        {
            BoosterStreamReaderGeneric boosterStreamReader = new BoosterStreamReaderGeneric();

            var res = boosterStreamReader.GetStreamInfo();

            Assert.NotEqual(0, ((StreamInfo)res.Result).WordsCount);
            Assert.NotEqual(0, ((StreamInfo)res.Result).CharactersCount);
        }


        [Theory]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        [InlineData(100000)]
        public void BoosterStreamReaderGeneric_GetStreamInfo_integration_has_result_validate_character_Count_Test(int bytesCount)
        {
            BoosterStreamReaderGeneric boosterStreamReader = new BoosterStreamReaderGeneric(bytesCount);

            var res = boosterStreamReader.GetStreamInfo();


            Assert.NotEqual(0, ((StreamInfo)res.Result).CharactersCount);
            Assert.Equal(bytesCount, ((StreamInfo)res.Result).CharactersCount);

        }

        [Fact]
        public void GetLargestWords_has_No_errors_Negative_method_param_Test()
        {
            BoosterStreamReaderGeneric boosterStreamReader = new BoosterStreamReaderGeneric();

            var exception = Record.Exception(() => boosterStreamReader.GetLargestWords(5));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void GetLargestWords_has_result_integration_not_empty_Test(int wordsCount)
        {
            BoosterStreamReaderGeneric boosterStreamReader = new BoosterStreamReaderGeneric();

            var res = boosterStreamReader.GetLargestWords(wordsCount);

            Assert.NotEmpty((res.Result as WordInfo).Info);


        }
        [Theory]
        [InlineData(100, 5)]
        [InlineData(1000, 10)]
        [InlineData(10000, 100)]
        public void GetLargestWords_has_result_not_empty_short_text_Test(int textLenth, int wordsCount)
        {
            BoosterStreamReaderGeneric boosterStreamReader = new BoosterStreamReaderGeneric(textLenth);

            var res = boosterStreamReader.GetLargestWords(wordsCount);

            Assert.NotEmpty(((WordInfo)res.Result).Info);


        }

        [Theory]
        [InlineData(100, 5)]
        [InlineData(1000, 10)]
        [InlineData(10000, 100)]
        public void GetSmallestWords_has_result_not_empty_short_text_Test(int textLenth, int wordsCount)
        {
            BoosterStreamReaderGeneric boosterStreamReader = new BoosterStreamReaderGeneric(textLenth);

            var res = boosterStreamReader.GetSmallestWords(wordsCount);

            Assert.NotEmpty(((WordInfo)res.Result).Info);


        }


    }
}