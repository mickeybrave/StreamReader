namespace StreamReader.Core
{
    public interface IStreamReaderCommonGeneric
    {
        ReaderResult<IStreamInfo> GetStreamInfo();
        ReaderResult<IStreamInfo> GetLargestWords(int count);
        ReaderResult<IStreamInfo> GetSmallestWords(int count);
        ReaderResult<IStreamInfo> GetMostFrequentlyAppearingWords(int count);
        ReaderResult<IStreamInfo> GetCharacterInfo();
    }
}
