using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EvilCorp.SlackStorage.LoggingService.UnitTest
{
    [TestClass]
    public class LogEntryTests
    {
        [TestMethod, TestCategory("DomainTypes")]
        [ExpectedException(typeof(ArgumentException))]
        public void LogEntry_CannotHave_EmptyComponent()
        {
            var entry = new LogEntry(
                string.Empty,
                "Test message",
                DateTime.Now,
                LogLevel.Trace);
        }

        [TestMethod, TestCategory("DomainTypes")]
        [ExpectedException(typeof(ArgumentException))]
        public void LogEntry_CannotHave_EmptyMessage()
        {
            var entry = new LogEntry(
                "TestService",
                string.Empty,
                DateTime.Now,
                LogLevel.Trace);
        }

        [TestMethod, TestCategory("DomainTypes")]
        public void LogEntry_Return_CurrectValues()
        {
            const string component = "TestService";
            const string message = "Test message.";
            const LogLevel level = LogLevel.Critical;

            var entry = new LogEntry(component, message, DateTime.Now, level);

            Assert.AreEqual(component, entry.Component);
            Assert.AreEqual(message, entry.Message);
            Assert.AreEqual(level, entry.Level);
        }
    }
}
 