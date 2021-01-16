#region --- License & Copyright Notice ---
/*
Custom collections and collection extensions for .NET
Copyright (c) 2018-2020 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System.Collections.Generic;

#if EXPLICIT
using System;

namespace Collections.Net.ObjectModel
#else
// ReSharper disable once CheckNamespace
namespace System.Collections.ObjectModel
#endif
{
    public abstract partial class DirectoryBase<TKey, TValue>
    {
        protected virtual void InsertItem(TKey key, TValue value)
        {
            _dictionaryImpl.Add(key, value);
        }

        protected virtual void SetItem(TKey key, TValue value)
        {
            _dictionaryImpl[key] = value;
        }

        protected virtual void ClearItems()
        {
            _dictionaryImpl.Clear();
        }

        protected virtual bool RemoveItem(TKey key)
        {
            return _dictionaryImpl.Remove(key);
        }
    }

    public abstract partial class DirectoryBase<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionaryImpl = new Dictionary<TKey, TValue>();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _dictionaryImpl.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionaryImpl).GetEnumerator();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            InsertItem(item.Key, item.Value);
        }

        public void Clear()
        {
            ClearItems();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionaryImpl.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionaryImpl.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return RemoveItem(item.Key);
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count => _dictionaryImpl.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => _dictionaryImpl.IsReadOnly;

        public void Add(TKey key, TValue value)
        {
            InsertItem(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionaryImpl.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return RemoveItem(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionaryImpl.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get => _dictionaryImpl[key];
            set => SetItem(key, value);
        }

        public ICollection<TKey> Keys => _dictionaryImpl.Keys;

        public ICollection<TValue> Values => _dictionaryImpl.Values;
    }
}
