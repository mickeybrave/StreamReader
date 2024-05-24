namespace StreamReader.Core
{


    public class BoosterStreamReaderGeneric : IStreamReaderCommonGeneric
    {
        private const int DefaultBytesNumber = 104857;// works

        private readonly int _bytes;
        private readonly ITextProcessor _textProcessor;
        private readonly IBoosterStreamReaderProxy _boosterStreamReadeProxy;
        private readonly IBoosterStreamReadeFactory _boosterStreamReadeFactory;

        public BoosterStreamReaderGeneric()
        {
            _bytes = DefaultBytesNumber;
            _textProcessor = new TextProcessor();
            _boosterStreamReadeProxy = new BoosterStreamReaderProxy(_textProcessor.GetText(_bytes));
            _boosterStreamReadeFactory = new BoosterStreamReadeFactory();
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
            _boosterStreamReadeFactory = new BoosterStreamReadeFactory();
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
