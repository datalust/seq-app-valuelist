using System;
using System.Collections.Generic;
using System.IO;
using Seq.Apps;
using Serilog;
using Serilog.Events;

namespace Seq.App.ValueList.Tests.Support
{
    class TestAppHost : IAppHost, IDisposable
    {
        public IList<LogEvent> DiagnosticsEmitted { get; } = new List<LogEvent>();

        public Host Host { get; } = Some.Host();
        public ILogger Logger { get; }
        public string StoragePath { get; } = Path.Combine(Path.GetTempPath(), typeof(TestAppHost).FullName!, Guid.NewGuid().ToString("n"), "storage");
        public Apps.App App { get; }
        
        public TestAppHost()
        {
            Logger = new LoggerConfiguration().WriteTo.Collection(DiagnosticsEmitted).CreateLogger();
            Directory.CreateDirectory(StoragePath);
            App = new Apps.App("appinstance-0", "Test", new Dictionary<string, string>(), StoragePath);
        }
        
        public void Dispose()
        {
            if (Directory.Exists(StoragePath))
                Directory.Delete(StoragePath, recursive: true);
        }
    }
}
