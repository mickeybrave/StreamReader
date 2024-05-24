
namespace StreamReader.Core
{
    public class LargestWordCalculator : IStreamInfoCalculator
    {
        private readonly int _counter;
        private const int DefaultCountLargestWords = 5;

        public LargestWordCalculator(int counter)
        {
            _counter = counter;
        }
        public IStreamInfo GetStreamInfo(string text)
        {
            return new WordInfo()
            {
                Info = GetLargestWords(text, _counter)
            };
        }
        private string[] GetLargestWords(string textResult, int count)
        {
            var words = textResult.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            var sorted = words.OrderByDescending(word => word.Length);
            return sorted.Take(count).ToArray();
        }
    }
}
