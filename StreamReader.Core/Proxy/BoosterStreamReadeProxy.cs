namespace StreamReader.Core
{
    public class BoosterStreamReaderProxy : IBoosterStreamReaderProxy
    {
        private readonly string _text;
        public BoosterStreamReaderProxy(string text)
        {
            _text = text;
        }

        public ReaderResult<IStreamInfo> GetInfo(IStreamInfoCalculator streamInfoCalculator)
        {
            try
            {
                if (streamInfoCalculator == null)
                {
                    return new ReaderResult<IStreamInfo>
                    {
                        Error = new Error
                        {
                            Message = $"{nameof(streamInfoCalculator)} cannot be empty"
                        }
                    };
                }

                if (string.IsNullOrEmpty(_text))
                {
                    return new ReaderResult<IStreamInfo>
                    {
                        Error = new Error
                        {
                            Message = "provided text cannot be empty"
                        }
                    };
                }

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
