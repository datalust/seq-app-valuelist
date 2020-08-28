using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Seq.App.ValueList.Tests.Support
{
    class CollectionSink : ILogEventSink
    {
        readonly ICollection<LogEvent> _collection;

        public CollectionSink(ICollection<LogEvent> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public void Emit(LogEvent logEvent)
        {
            _collection.Add(logEvent);
        }
    }
}