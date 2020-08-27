using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Seq.App.ValueList
{
    class PersistentValueSet
    {
        readonly string _filePath;
        readonly HashSet<string> _values = new HashSet<string>();

        public PersistentValueSet(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadLines(_filePath))
                {
                    if (!string.IsNullOrEmpty(line))
                        _values.Add(line);
                }
            }
        }

        public bool TryAdd(string item)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentException("Item cannot be null or empty.", nameof(item));

            if (_values.Contains(item))
                return false;

            _values.Add(item);
            File.WriteAllLines(_filePath, ToOrderedArray());
            return true;
        }

        public string[] ToOrderedArray() => _values.OrderBy(s => s).ToArray();
    }
}