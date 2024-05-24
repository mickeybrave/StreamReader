
namespace StreamReader.Core
{
    public class MostFrequentlyAppearingWordsCalculator : IStreamInfoCalculator
    {
        private readonly int _counter;
        public MostFrequentlyAppearingWordsCalculator(int counter)
        {
            _counter = counter;
        }
        public IStreamInfo GetStreamInfo(string text)
        {
            return new WordInfo
            {
                Info = GetMostFrequentlyAppearingWords(text, _counter)
            };
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
    }
}
