namespace StreamReader.Core
{
    public class SmallestWordCalculator : IStreamInfoCalculator
    {
        private readonly int _counter;
        public SmallestWordCalculator(int counter)
        {
            _counter = counter;
        }
        public IStreamInfo GetStreamInfo(string text)
        {
            return new WordInfo
            {
                Info = GetSmallestestWords(text, _counter)
            };
        }

        private string[] GetSmallestestWords(string textResult, int count)
        {
            var words = textResult.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            var sorted = words.OrderBy(word => word.Length);
            return sorted.Take(count).ToArray();
        }
    }
}
