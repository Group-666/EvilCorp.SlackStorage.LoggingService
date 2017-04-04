using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;

namespace EvilCorp.SlackStorage.LoggingService.UnitTest.DomainTypes
{
    [TestClass]
    public class LogParserTests
    {
        // var json = JObject.Parse("{ \"\":\"\", \"\":\"\", \"\": 2 }");

        [TestMethod, TestCategory("DomainTypes")]
        [ExpectedException(typeof(ArgumentException))]
        public void LogParser_CannotParseWith_NoComponent()
        {
            var json = new JObject
            {
                ["message"] = "Test message.",
                ["level"] = 2
            };

            LogParser.Parse(json);
        }

        [TestMethod, TestCategory("DomainTypes")]
        [ExpectedException(typeof(ArgumentException))]
        public void LogParser_CannotParseWith_NoMessage()
        {
            var json = new JObject
            {
                ["component"] = "TestService.",
                ["level"] = 2
            };

            LogParser.Parse(json);
        }

        [TestMethod, TestCategory("DomainTypes")]
        [ExpectedException(typeof(ArgumentException))]
        public void LogParser_CannotParseWith_NoLevel()
        {
            var json = new JObject
            {
                ["component"] = "TestService.",
                ["message"] = "Test message.",
            };

            LogParser.Parse(json);
        }

        [TestMethod, TestCategory("DomainTypes")]
        public void LogParser_CannotParseWith_NoIncurrectLevel()
        {
            var jsonLevelTooLow = new JObject
            {
                ["component"] = "TestService.",
                ["message"] = "Test message.",
                ["level"] = 0
            };
            var jsonLevelTooHeigh = new JObject
            {
                ["component"] = "TestService.",
                ["message"] = "Test message.",
                ["level"] = 6
            };

            try
            {
                LogParser.Parse(jsonLevelTooLow);
                Assert.Fail();
            }
            catch (Exception)
            {
            }
            try
            {
                LogParser.Parse(jsonLevelTooHeigh);
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }

        [TestMethod, TestCategory("DomainTypes")]
        public void LogParser_Parse_Currectly()
        {
            var json = new JObject
            {
                ["component"] = "TestService.",
                ["message"] = "Test message.",
                ["level"] = 1
            };

            var result = LogParser.Parse(json);

            Assert.AreEqual(json["component"], result.Component);
            Assert.AreEqual(json["message"], result.Message);
            Assert.AreEqual(LogLevel.Trace, result.Type);
        }
    }
}
