using System.Collections.Generic;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace Seq.App.ValueList.Tests.Support
{
    static class LoggerSinkConfigurationCollectionExtensions
    {
        public static LoggerConfiguration Collection(
            this LoggerSinkConfiguration configuration,
            ICollection<LogEvent> collection)
        {
            return configuration.Sink(new CollectionSink(collection));
        }
    }
}