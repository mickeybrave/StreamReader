namespace StreamReader.Core
{
    public class BoosterStreamReaderGeneric : IStreamReaderCommonGeneric
    {
        /// <summary>
        /// There is a problem in in WordStream --> Read(). if the requested number of bytes/characters is too long, the method execution is frozen and does not execute. According to my observation it is not safe to request more than 100000. The number of character that makes the method frozen is not consistent.
        /// </summary>
        private const int DefaultBytesNumber = 100000;

        private readonly int _bytes;
        private readonly ITextProcessor _textProcessor;
        private readonly IBoosterStreamReaderProxy _boosterStreamReadeProxy;
        private readonly IBoosterStreamReadeFactory _boosterStreamReadeFactory;

        public BoosterStreamReaderGeneric()
        {
            _bytes = DefaultBytesNumber;
            _textProcessor = new TextProcessor();
            _boosterStreamReadeProxy = new BoosterStreamReaderProxy(_textProcessor.GetText(_bytes));
            _boosterStreamReadeFactory = new BoosterStreamReaderFactory();
        }
        public BoosterStreamReaderGeneric(int bytes,
            ITextProcessor textProcessor,
            IBoosterStreamReaderProxy boosterStreamReadeProxy,
            IBoosterStreamReadeFactory boosterStreamReadeFactory)
        {
            _bytes = bytes <= 0 ? DefaultBytesNumber : bytes;
            _textProcessor = textProcessor;
            _boosterStreamReadeProxy = boosterStreamReadeProxy;
            _boosterStreamReadeFactory = boosterStreamReadeFactory;
        }

        public BoosterStreamReaderGeneric(int bytes)
        {
            _bytes = bytes <= 0 ? DefaultBytesNumber : bytes;
            _textProcessor = new TextProcessor();
            _boosterStreamReadeProxy = new BoosterStreamReaderProxy(_textProcessor.GetText(_bytes));
            _boosterStreamReadeFactory = new BoosterStreamReaderFactory();
        }

        public ReaderResult<IStreamInfo> GetStreamInfo()
        {
            IStreamInfoCalculator streamInfoCalculator =
                _boosterStreamReadeFactory.GetCalculator(CalculationTypes.StreamInfo);
            return _boosterStreamReadeProxy.GetInfo(streamInfoCalculator);
        }

        public ReaderResult<IStreamInfo> GetCharacterInfo()
        {
            IStreamInfoCalculator streamInfoCalculator =
                _boosterStreamReadeFactory.GetCalculator(CalculationTypes.CharInfo);
            return _boosterStreamReadeProxy.GetInfo(streamInfoCalculator);
        }

        public ReaderResult<IStreamInfo> GetLargestWords(int count)
        {
            IStreamInfoCalculator streamInfoCalculator =
               _boosterStreamReadeFactory.GetCalculator(CalculationTypes.LargestWord, count);
            return _boosterStreamReadeProxy.GetInfo(streamInfoCalculator);
        }

        public ReaderResult<IStreamInfo> GetSmallestWords(int count)
        {
            IStreamInfoCalculator streamInfoCalculator =
              _boosterStreamReadeFactory.GetCalculator(CalculationTypes.SmallestWord, count);
            return _boosterStreamReadeProxy.GetInfo(streamInfoCalculator);
        }

        public ReaderResult<IStreamInfo> GetMostFrequentlyAppearingWords(int count)
        {
            IStreamInfoCalculator streamInfoCalculator =
               _boosterStreamReadeFactory
               .GetCalculator(CalculationTypes.MostFrequentlyAppearingWord, count);
            return _boosterStreamReadeProxy.GetInfo(streamInfoCalculator);
        }

    }
}
