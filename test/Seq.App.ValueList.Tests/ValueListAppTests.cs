using System.Collections.Generic;
using System.IO;
using Seq.App.ValueList.Tests.Support;
using Xunit;

namespace Seq.App.ValueList.Tests
{
    public class ValueListAppTests
    {
        [Fact]
        public void AppTriggersAlerts()
        {
            var app = new ValueListApp
            {
                PropertyName = "Test"
            };
            
            using var host = new TestAppHost();
            app.Attach(host);

            app.On(Some.LogEvent(new Dictionary<string, object> {["Something"] = "Irrelevant"}));

            Assert.Empty(Directory.GetFiles(host.StoragePath));

            app.On(Some.LogEvent(new Dictionary<string, object> {["Test"] = "First"}));

            var file = Assert.Single(Directory.GetFiles(host.StoragePath));
            var lines = File.ReadLines(file!);
            var line = Assert.Single(lines);
            Assert.Equal("First", line);
        }
    }
}
