using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamReader.Core
{
    public class StreamInfoCalculator : IStreamInfoCalculator
    {
        public IStreamInfo GetStreamInfo(string textResult)
        {
            return new StreamInfo
            {
                CharactersCount = GetCharactersCount(textResult),
                WordsCount = GetWordsCount(textResult)
            };
        }
        private int GetWordsCount(string textResult)
        {
            var words = textResult.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        private int GetCharactersCount(string textResult)
        {
            char[] chars = textResult.ToCharArray();
            return chars.Length;
        }
    }
}
