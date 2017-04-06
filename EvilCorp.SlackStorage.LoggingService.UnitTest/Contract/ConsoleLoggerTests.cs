using EvilCorp.SlackStorage.LoggingService.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace EvilCorp.SlackStorage.LoggingService.UnitTest.Contract
{
    [TestClass]
    public class ConsoleLoggerTests
    {
        [TestMethod, TestCategory("Contract")]
        public void ConsoleLogger_WritesToTheConsole_WhenLevelIsEqualOrHigher()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);
            var message = "There is a critical problem!";
            var sut = new ConsoleLogger(LogLevel.Information);

            sut.Log(message, LogLevel.Critical);
            Assert.AreEqual(message + Environment.NewLine, writer.ToString());

            writer = new StringWriter();
            Console.SetOut(writer);

            sut.Log(message, LogLevel.Information);
            Assert.AreEqual(message + Environment.NewLine, writer.ToString());
        }

        [TestMethod, TestCategory("Contract")]
        public void ConsoleLogger_DoesNotWrite_WhenLevelIsTooLow()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);
            var message = "Well this message is only for the reason of a message.";
            var sut = new ConsoleLogger(LogLevel.Information);

            sut.Log(message, LogLevel.Trace);
            Assert.AreEqual(string.Empty, writer.ToString());
        }
    }
}
