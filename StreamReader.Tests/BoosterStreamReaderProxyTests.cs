using Moq;
using StreamReader.Core;

namespace StreamReader.Tests
{
    public class BoosterStreamReaderProxyTests
    {


        [Fact]
        public void BoosterStreamReaderProxy_getInfo_streamInfoCalculator_is_null()
        {
            var proxy = new BoosterStreamReaderProxy("some text");

            var result = proxy.GetInfo(null);

            Assert.Null(result.Result);
            Assert.NotNull(result.Error);
            Assert.True(result.Error.Message.Contains("streamInfoCalculator"));
        }

        [Fact]
        public void BoosterStreamReaderProxy_getInfo_streamInfoCalculator_is_null_no_exception()
        {
            var streamProxy = new BoosterStreamReaderProxy("text");

            var exception = Record.Exception(() => streamProxy.GetInfo(null));
            Assert.Null(exception);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void BoosterStreamReaderProxy_getInfo_text_is_null(string text)
        {
            var streamProxy = new BoosterStreamReaderProxy(text);

            var mockCalculator = new Mock<IStreamInfoCalculator>();

            var result = streamProxy.GetInfo(mockCalculator.Object);

            Assert.Null(result.Result);
            Assert.NotNull(result.Error);

            Assert.True(result.Error.Message.Contains("text"));

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void BoosterStreamReaderProxy_getInfo_text_is_null_no_exception(string text)
        {
            var streamProxy = new BoosterStreamReaderProxy(text);

            var mockCalculator = new Mock<IStreamInfoCalculator>();

            var exception = Record.Exception(() => streamProxy.GetInfo(mockCalculator.Object));
            Assert.Null(exception);
        }


        [Fact]
        public void BoosterStreamReaderProxy_getInfo_normal_result()
        {
            var streamProxy = new BoosterStreamReaderProxy("some text");

            var mockCalculator = new Mock<IStreamInfoCalculator>();

            mockCalculator.Setup(x => x.GetStreamInfo(It.IsAny<string>())).Returns(new StreamInfo());

            var result = streamProxy.GetInfo(mockCalculator.Object);



            Assert.NotNull(result.Result);
            Assert.Null(result.Error.Message);
        }

        [Fact]
        public void BoosterStreamReaderProxy_getInfoGetStreamInfo_no_error_result()
        {
            var streamProxy = new BoosterStreamReaderProxy("some text");

            var mockCalculator = new Mock<IStreamInfoCalculator>();

            mockCalculator.Setup(x => x.GetStreamInfo(It.IsAny<string>()))
                .Throws(new IOException());

            var result = streamProxy.GetInfo(mockCalculator.Object);



            Assert.Null(result.Result);
            Assert.NotNull(result.Error.Message);

            Assert.True(result.Error.Message.Equals("I/O error occurred."));
        }

    }
}