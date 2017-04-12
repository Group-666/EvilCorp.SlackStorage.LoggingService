using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.Contract
{
    public class HttpServiceLogger : ILogger
    {
        private HttpClient _client = new HttpClient();
        private readonly string _connectionString;
        private readonly string _componentName;
        private readonly int _logLevel;

        public HttpServiceLogger(string connectionString, string componentName, LogLevel logLevel)
        {
            _connectionString = connectionString;
            _componentName = componentName;
            _logLevel = (int)logLevel;
        }

        public void Log(string message, LogLevel level)
        {
            if ((int)level < _logLevel)
                return;

            var url = string.Concat(_connectionString, "/api/log/");

            // Sends log async but is not waited for.
            Task.Run(() => SendLog(url, message, level));
        }

        private async Task SendLog(string url, string message, LogLevel level)
        {
            try
            {
                var content = CreateContent(message, level);
                await _client.PostAsync(url, content);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private StringContent CreateContent(string message, LogLevel level)
        {
            var json = new JObject
            {
                ["componet"] = _componentName,
                ["message"] = message,
                ["level"] = (int)level
            };

            var content = new StringContent(json.ToString());
            content.Headers.Clear();
            content.Headers.Add("content-type", "application/json");

            return content;
        }
    }
}
