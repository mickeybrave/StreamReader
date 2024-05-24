using Moq;
using StreamReader.Core;

namespace StreamReader.Tests.Calculators
{
    public class CharacterInfoCalculatorTests
    {
        private readonly CharacterInfoCalculator _calculator = new CharacterInfoCalculator();

        [Theory]
        [InlineData("aaabbbbccccdddee", 16, 'a', 3)]
        [InlineData("aaabbbbccccdddee", 16, 'b', 4)]
        [InlineData("aaabbbbccccdddee", 16, 'c', 4)]
        [InlineData("aaabbbbccccdddee", 16, 'd', 3)]
        [InlineData("aaabbbbccccdddee", 16, 'e', 2)]
        [InlineData("aaQQQQ", 6, 'q', 4)]
        [InlineData("aaabbbbccccdddeeQQQQ", 20, 'q', 4)]
        [InlineData("aaa bbbb cccc ddd ee QQQQ", 25, 'q', 4)]
        [InlineData("aaa, bbbb, cccc, ddd, ee. QQQQ uuUuu", 36, 'u', 5)]
        public void GetCharacterInfo_Test(string text, int charCount, char ch, int frequency)
        {
            var res = (CharactersInfo)_calculator.GetStreamInfo(text);

            Assert.Equal(res.AllCharacters.Count(), charCount);
            Assert.Equal(res.CharactersFrequency[ch], frequency);
        }

    }
}