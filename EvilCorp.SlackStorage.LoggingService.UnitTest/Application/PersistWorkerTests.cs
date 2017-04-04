using EvilCorp.SlackStorage.LoggingService.Application;
using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.UnitTest.Application
{
    [TestClass]
    public class PersistWorkerTests
    {
        [TestMethod, TestCategory("Application")]
        public void PersistWorker_()
        {
            var queue = new ConcurrentQueue<LogEntry>();
            queue.Enqueue(new LogEntry("TestService", "Test message", LogLevel.Trace));

            var contextMock = new Mock<IPersistWorkerContext>();
            contextMock.Setup(c => c.QueueOfWork).Returns(queue);

            var worker = new PersistWorker(contextMock.Object, null);

            var tokenSource = new CancellationTokenSource();

            worker.Run(tokenSource.Token).ContinueWith(t =>
            {
                if (t.Exception != null)
                    Assert.Fail(t.Exception.Message);
            });

            Task.Delay(new TimeSpan(0, 0, 2)).Wait();
            tokenSource.Cancel();
        }
    }
}
