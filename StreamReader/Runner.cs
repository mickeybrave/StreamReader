using StreamReader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamReader
{
    public class Runner
    {
        private IStreamReaderCommonGeneric _streamReaderCommonGeneric;


        private const int DefaultSmallestWordsNumber = 5;
        private const int DefaultLargestWordsNumber = 5;
        private const int DefaultFreqAppearingNumber = 5;

        private const int MinimalTextRange = 1000;
        private const int MaximmalTextRange = 10000;


        public async Task<string> ExtractText()
        {
            return await Task.Run(() =>
            {
                Random r = new Random();
                int rInt = r.Next(MinimalTextRange, MaximmalTextRange);

                _streamReaderCommonGeneric = new BoosterStreamReaderGeneric(rInt);

                var largestWordsRes = (WordInfo)_streamReaderCommonGeneric.GetLargestWords(DefaultLargestWordsNumber).Result;

                var smallestWordsRes = (WordInfo)_streamReaderCommonGeneric.GetSmallestWords(DefaultSmallestWordsNumber).Result;
                var streamInfo = (StreamInfo)_streamReaderCommonGeneric.GetStreamInfo().Result;
                var charsInfo = (CharactersInfo)_streamReaderCommonGeneric.GetCharacterInfo().Result;
                var freqApearingWords = (WordInfo)_streamReaderCommonGeneric.GetMostFrequentlyAppearingWords(DefaultFreqAppearingNumber).Result;

                var sb = new StringBuilder();

                sb.Append($" There are {string.Join(',', streamInfo.CharactersCount)} characters in the stream");
                sb.Append(Environment.NewLine);

                sb.Append($" There are {string.Join(',', streamInfo.WordsCount)} words in the stream");
                sb.Append(Environment.NewLine);

                sb.Append($"{DefaultLargestWordsNumber} largest words are {string.Join(',', largestWordsRes.Info)}");
                sb.Append(Environment.NewLine);

                sb.Append($"{DefaultSmallestWordsNumber} smallest words are {string.Join(',', smallestWordsRes.Info)}");
                sb.Append(Environment.NewLine);

                sb.Append($"Most frequently appearing words are {string.Join(',', freqApearingWords.Info)}");
                sb.Append(Environment.NewLine);

                sb.Append($" All characters are {string.Join(',', charsInfo.AllCharacters)}");
                sb.Append(Environment.NewLine);

                sb.Append($" All characters are {string.Join(',', charsInfo.CharactersFrequency.Select(s => $"Character {s.Key} appears {s.Value} times"))}");

                return sb.ToString();
            });


        }
    }
}
