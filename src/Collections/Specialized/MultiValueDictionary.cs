// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

#if EXPLICIT
namespace Collections.Net.Specialized
#else
// ReSharper disable once CheckNamespace
namespace System.Collections.Specialized
#endif
{
    public partial class MultiValueDictionary<TKey, TValue> : Dictionary<TKey, IList<TValue>>, IMultiValueDictionary<TKey, TValue>
    {
        public void Add(TKey key, params TValue[] values)
        {
            Add(key, (IEnumerable<TValue>)values);
        }

        public void Add(TKey key, IEnumerable<TValue> values)
        {
            if (!TryGetValue(key, out IList<TValue> valuesEntry))
            {
                valuesEntry = CreateList();
                if (valuesEntry is null)
                {
                    string errorMessage =
                        $"The {GetType().Name}.CreateList method return a null IList<{typeof(TValue).FullName}."
                        + Environment.NewLine
                        + "Ensure that the method return a non-null value.";
                    throw new InvalidOperationException(errorMessage);
                }

                base.Add(key, valuesEntry);
            }

            foreach (TValue value in values)
                valuesEntry.Add(value);
        }

        protected virtual IList<TValue> CreateList()
        {
            return new List<TValue>();
        }
    }

    public partial class MultiValueDictionary<TKey, TValue> : ILookup<TKey, TValue>
    {
        IEnumerator<IGrouping<TKey, TValue>> IEnumerable<IGrouping<TKey, TValue>>.GetEnumerator()
        {
            foreach (KeyValuePair<TKey, IList<TValue>> kvp in this)
                yield return new GroupingImpl<TKey, TValue>(kvp.Key, kvp.Value);
        }

        bool ILookup<TKey, TValue>.Contains(TKey key)
        {
            return ContainsKey(key);
        }

        IEnumerable<TValue> ILookup<TKey, TValue>.this[TKey key]
            => TryGetValue(key, out IList<TValue> values) ? values : Enumerable.Empty<TValue>();
    }

    internal sealed class GroupingImpl<TKey, TValue> : IGrouping<TKey, TValue>
    {
        private readonly TKey _key;
        private readonly IList<TValue> _values;

        internal GroupingImpl(TKey key, IList<TValue> values)
        {
            _key = key;
            _values = values;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        TKey IGrouping<TKey, TValue>.Key => _key;
    }
}
