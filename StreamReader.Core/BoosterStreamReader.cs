using NLipsum.Core;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace StreamReader.Core
{
    public interface IStreamReaderCommon
    {
        ReaderResult<StreamInfo> GetStreamInfo();
        ReaderResult<string[]> GetLargestWords(int count);
        ReaderResult<string[]> GetSmallestWords(int count);
        ReaderResult<string[]> GetMostFrequentlyAppearingWords(int count);
        ReaderResult<CharactersInfo> GetCharacterInfo();

    }
    public class BoosterStreamReader : IStreamReaderCommon
    {
        private const int DefaultBytesNumber = 104857;// works
        private const int DefaultCountLargestWords = 5;
        private readonly int _bytes;
        private readonly ITextProcessor _textProcessor;

        public BoosterStreamReader()
        {
            _bytes = DefaultBytesNumber;
            _textProcessor = new TextProcessor();
        }
        public BoosterStreamReader(int bytes, ITextProcessor textProcessor)
        {
            _bytes = bytes <= 0 ? DefaultBytesNumber : bytes;
            _textProcessor = textProcessor;
        }

        public BoosterStreamReader(int bytes)
        {
            _bytes = bytes <= 0 ? DefaultBytesNumber : bytes;
            _textProcessor = new TextProcessor();
        }

        public ReaderResult<StreamInfo> GetStreamInfo()
        {
            try
            {
                var textResult = _textProcessor.GetText(_bytes);

                return new ReaderResult<StreamInfo>()
                {
                    Result = new StreamInfo
                    {
                        CharactersCount = GetCharactersCount(textResult),
                        WordsCount = GetWordsCount(textResult)
                    }
                };
            }
            catch (Exception ex)
            {
                return new ReaderResult<StreamInfo>
                {
                    Error = new Error
                    {
                        Message = ex.Message
                    }
                };
            }

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



        public ReaderResult<string[]> GetLargestWords(int count)
        {
            count = count >= 0 ? count : DefaultCountLargestWords;
            try
            {
                return new ReaderResult<string[]>()
                {
                    Result = GetLargestWords(_textProcessor.GetText(_bytes), count)
                };
            }
            catch (Exception ex)
            {
                return new ReaderResult<string[]>
                {
                    Error = new Error
                    {
                        Message = ex.Message
                    }
                };
            }
        }
        private string[] GetLargestWords(string textResult, int count)
        {
            var words = textResult.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            var sorted = words.OrderByDescending(word => word.Length);
            return sorted.Take(count).ToArray();
        }

        private string[] GetSmallestestWords(string textResult, int count)
        {
            var words = textResult.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            var sorted = words.OrderBy(word => word.Length);
            return sorted.Take(count).ToArray();
        }

        public ReaderResult<string[]> GetSmallestWords(int count)
        {
            count = count >= 0 ? count : DefaultCountLargestWords;
            try
            {
                return new ReaderResult<string[]>()
                {
                    Result = GetSmallestestWords(_textProcessor.GetText(_bytes), count)
                };
            }
            catch (Exception ex)
            {
                return new ReaderResult<string[]>
                {
                    Error = new Error
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        public ReaderResult<string[]> GetMostFrequentlyAppearingWords(int count)
        {
            count = count >= 0 ? count : DefaultCountLargestWords;
            try
            {
                return new ReaderResult<string[]>()
                {
                    Result = GetMostFrequentlyAppearingWords(_textProcessor.GetText(_bytes), count)
                };
            }
            catch (Exception ex)
            {
                return new ReaderResult<string[]>
                {
                    Error = new Error
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        private string[] GetMostFrequentlyAppearingWords(string text, int count)
        {
            var words = text.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            var sorted = words.OrderByDescending(word => word.Length);
            var result = words
                          .GroupBy(s => s)
                          .Where(g => g.Count() > 1)
                          .OrderByDescending(g => g.Count())
                          .Take(count)
                          .Select(g => g.Key)
                          .ToArray();

            return result;
        }

        public ReaderResult<CharactersInfo> GetCharacterInfo()
        {
            try
            {
                return new ReaderResult<CharactersInfo>()
                {
                    Result = GetCharacterInfo(_textProcessor.GetText(_bytes))
                };
            }
            catch (Exception ex)
            {
                return new ReaderResult<CharactersInfo>
                {
                    Error = new Error
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        private CharactersInfo GetCharacterInfo(string text)
        {
            var chars = text.ToCharArray();
            var freq = new Dictionary<char, int>();

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == ' ' || chars[i] == '.' || chars[i] == ',')
                    continue;

                if (!freq.ContainsKey(char.ToLower(chars[i])))
                {
                    freq.Add(char.ToLower(chars[i]), 1);
                }
                else
                {
                    freq[char.ToLower(chars[i])]++;
                }

            }

            return new CharactersInfo()
            {   
                AllCharacters = chars,
                CharactersFrequency = freq
            };
        }

    }
}
