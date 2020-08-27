using System;
using System.Collections.Generic;
using System.IO;
using Seq.Apps;
using Serilog;

namespace Seq.App.ValueList.Tests.Support
{
    class TestAppHost : IAppHost, IDisposable
    {
        public Apps.App App { get; } = new Apps.App("appinstance-0", "Test", new Dictionary<string, string>(), "./storage");
        public Host Host { get; } = Some.Host();
        public ILogger Logger { get; } = new LoggerConfiguration().CreateLogger();
        public string StoragePath { get; } = Path.Combine(Path.GetTempPath(), typeof(TestAppHost).FullName!, "storage");

        public TestAppHost()
        {
            Directory.CreateDirectory(StoragePath);
        }
        
        public void Dispose()
        {
            if (Directory.Exists(StoragePath))
                Directory.Delete(StoragePath);
        }
    }
}
