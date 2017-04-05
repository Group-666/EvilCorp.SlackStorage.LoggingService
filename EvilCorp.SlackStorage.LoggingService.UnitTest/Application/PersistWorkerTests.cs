using EvilCorp.SlackStorage.LoggingService.Application;
using EvilCorp.SlackStorage.LoggingService.DataAccess;
using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.UnitTest.Application
{
    [TestClass]
    public class PersistWorkerTests
    {
        [TestMethod, TestCategory("Application")]
        public void PersistWorker_ProcessEntry_Currectly()
        {
            var log = new LogEntry("TestService", "Test message", LogLevel.Trace);
            var tokenSource = new CancellationTokenSource();
            var contextMock = CreateContextMock(log);
            var repositoryMock = CreateRepositoryMock(log);
            var done = false;
            var error = false;

            var sut = new PersistWorker(contextMock, repositoryMock);
            sut.Run(tokenSource.Token).ContinueWith(t =>
            {
                if (t.Exception != null)
                    error = true;
                done = true;
            });


            Task.Delay(200).ContinueWith(t => tokenSource.Cancel());
            while (!done)
            {
                Task.Delay(10);
            }
            Assert.IsFalse(error);
        }

        private ILogRepository CreateRepositoryMock(LogEntry log)
        {
            var repositoryMock = new Mock<ILogRepository>();

            repositoryMock.Setup(r => r.Add(It.Is<LogEntry>(l => l == log))).Returns(Task.Run(() => { }));

            return repositoryMock.Object;
        }

        private IPersistWorkerContext CreateContextMock(LogEntry log = null)
        {
            var queue = new ConcurrentQueue<LogEntry>();
            queue.Enqueue(log);

            var contextMock = new Mock<IPersistWorkerContext>();
            contextMock.Setup(c => c.QueueOfWork).Returns(queue);

            return contextMock.Object;
        }
    }
}
