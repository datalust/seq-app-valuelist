using System;
using System.IO;
using System.Threading.Tasks;
using Seq.Apps;
using Seq.Apps.LogEvents;

// ReSharper disable MemberCanBePrivate.Global, UnusedType.Global, UnusedAutoPropertyAccessor.Global

namespace Seq.App.ValueList
{
    [SeqApp("Property Value List", Description = "Track distinct values of a particular event property.")]
    public class ValueListApp : SeqApp, ISubscribeTo<LogEventData>
    {
        PersistentValueSet _values;
        
        [SeqAppSetting(
            DisplayName = "Property name", 
            HelpText = "The name of the event property to track. Only non-whitespace string values are considered.")]
        public string PropertyName { get; set; }
        
        protected override void OnAttached()
        {
            _values = new PersistentValueSet(Path.Combine(App.StoragePath, "values.txt"));
        }

        public void On(Event<LogEventData> evt)
        {
            if (evt == null) throw new ArgumentNullException(nameof(evt));

            if (!evt.Data.Properties.TryGetValue(PropertyName, out var value) ||
                !(value is string s) ||
                string.IsNullOrWhiteSpace(s))
            {
                return;
            }

            if (_values.TryAdd(s))
            {
                Log.ForContext("AllValues", _values.ToOrderedArray())
                    .Information("New value {NewValue} detected for property {PropertyName}", s, PropertyName);
            }
        }
    }
}
