namespace StreamReader.Core
{
    public interface IBoosterStreamReaderProxy
    {
        ReaderResult<IStreamInfo> GetInfo(IStreamInfoCalculator streamInfoCalculator);
    }

    public class BoosterStreamReaderProxy: IBoosterStreamReaderProxy
    {
        private readonly ITextProcessor _textProcessor;
        private readonly string _text;

        public BoosterStreamReaderProxy(string text)
        {
            _text = text;
        }

        public ReaderResult<IStreamInfo> GetInfo(IStreamInfoCalculator streamInfoCalculator)
        {
            try
            {
                return new ReaderResult<IStreamInfo>()
                {
                    Result = streamInfoCalculator.GetStreamInfo(_text)
                };
            }
            catch (Exception ex)
            {
                return new ReaderResult<IStreamInfo>
                {
                    Error = new Error
                    {
                        Message = ex.Message
                    }
                };
            }
        }
    }

}
