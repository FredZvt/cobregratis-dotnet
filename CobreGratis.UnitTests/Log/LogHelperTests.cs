using BielSystems.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Log
{
    [TestClass]
    public class LogHelperTests
    {
        public Mock<ILogger> LoggerMock { get; set; }
        public LogHelper LogHelper { get; set; }

        public void InitFullLogHelper()
        {
            this.LoggerMock = new Mock<ILogger>();
            this.LogHelper = new LogHelper(this.LoggerMock.Object);
        }

        public void InitEmptyLogHelper()
        {
            this.LogHelper = new LogHelper(null);
        }

        [TestMethod]
        public void LogHelper_should_not_do_anything_when_Logger_is_null()
        {
            InitEmptyLogHelper();
            this.LogHelper.Log("dummy value");
            this.LogHelper.Log("dummy value", 1, "2", 3.0M);
        }

        [TestMethod]
        public void LogHelper_should_pass_message_to_Logger()
        {
            InitFullLogHelper();
            this.LoggerMock.Setup(l => l.Log(It.IsAny<string>())).Verifiable();
            this.LogHelper.Log("dummy value");
            this.LoggerMock.VerifyAll();
        }

        [TestMethod]
        public void LogHelper_should_pass_formatted_message_to_Logger()
        {
            InitFullLogHelper();
            this.LoggerMock.Setup(l => l.Log("format here 1, 2, 3")).Verifiable();
            this.LogHelper.Log("format here {0}, {1}, {2}", 1, "2", 3);
            this.LoggerMock.VerifyAll();
        }
    }
}
