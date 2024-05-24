namespace StreamReader.Core
{
    public interface IBoosterStreamReaderProxy
    {
        ReaderResult<IStreamInfo> GetInfo(IStreamInfoCalculator streamInfoCalculator);
    }
}
