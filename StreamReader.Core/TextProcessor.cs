using System.Text;

namespace StreamReader.Core
{
    public interface ITextProcessor
    {
        string GetText(int bytesToBuffer);
    }
    public class TextProcessor : ITextProcessor
    {
        public string GetText(int bytesToBuffer)
        {
            using (var stream = new Booster.CodingTest.Library.WordStream())
            {
                byte[] buffer = new byte[bytesToBuffer]; // works
                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                return Encoding.Default.GetString(buffer);
            }
        }
    }


}
