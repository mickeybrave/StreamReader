namespace StreamReader.Core
{
    public interface ITextProcessor
    {
        string GetText(int bytesToBuffer);
    }
}
