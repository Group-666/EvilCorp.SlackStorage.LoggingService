using EvilCorp.SlackStorage.LoggingService.DataAccess;
using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.UnitTest.DataAccess
{
    [TestClass]
    public class LogRepositoryTests
    {
        [TestMethod, TestCategory("DataAccess")]
        public void LogRepository_Adds_Currectly()
        {
            var logEntry = new LogEntry("TestService", "Test message.", DateTime.Now, LogLevel.Trace);
            var clientMock = CreateMongoMock(logEntry);

            var sut = new LogRepository(clientMock);

            sut.Add(logEntry).Wait();
        }

        private IMongoClient CreateMongoMock(LogEntry log)
        {
            var clientMock = new Mock<IMongoClient>();
            var dbMock = new Mock<IMongoDatabase>();
            var collectionMock = new Mock<IMongoCollection<LogEntry>>();

            clientMock.Setup(c => c.GetDatabase(It.Is<string>(s => s == "LoggingService"), null)).Returns(dbMock.Object);
            dbMock.Setup(d => d.GetCollection<LogEntry>(It.Is<string>(s => s == "Logs"), null)).Returns(collectionMock.Object);
            collectionMock.Setup(c => c.InsertOneAsync(It.Is<LogEntry>(l => l == log), null, new CancellationToken())).Returns(Task.Run(() => { }));

            return clientMock.Object;
        }
    }
}
