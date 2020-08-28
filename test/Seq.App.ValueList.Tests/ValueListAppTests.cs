using System.Collections.Generic;
using System.IO;
using System.Linq;
using Seq.App.ValueList.Tests.Support;
using Xunit;

namespace Seq.App.ValueList.Tests
{
    public class ValueListAppTests
    {
        [Fact]
        public void AppTriggersAlerts()
        {
            using var host = new TestAppHost();

            var app = new ValueListApp
            {
                PropertyName = "Test"
            };
            
            app.Attach(host);

            app.On(Some.LogEvent(new Dictionary<string, object> {["Something"] = "Irrelevant"}));

            Assert.Empty(Directory.GetFiles(host.StoragePath));

            app.On(Some.LogEvent(new Dictionary<string, object> {["Test"] = "First"}));

            var file = Assert.Single(Directory.GetFiles(host.StoragePath));
            var lines = File.ReadLines(file!);
            var line = Assert.Single(lines);
            Assert.Equal("First", line);
            Assert.Equal(1, host.DiagnosticsEmitted.Count);
            
            app.On(Some.LogEvent(new Dictionary<string, object> {["Test"] = "First"}));
            app.On(Some.LogEvent(new Dictionary<string, object> {["Test"] = "Second"}));

            Assert.Equal(2, File.ReadLines(file).Count());
            Assert.Equal(2, host.DiagnosticsEmitted.Count);
        }
    }
}
