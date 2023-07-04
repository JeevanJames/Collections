// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

#if EXPLICIT
using System.Collections;

namespace Collections.Net.Specialized;
#else
// ReSharper disable once CheckNamespace
namespace System.Collections.Specialized;
#endif

public class Dictionary<TKey, TValue1, TValue2> :
    IDictionary<TKey, (TValue1 Value1, TValue2 Value2)>,
    IDictionary<TKey, TValue1, TValue2>
    where TKey : notnull
{
    private readonly IDictionary<TKey, (TValue1 Value1, TValue2 Value2)> _innerDictionary;

    public Dictionary()
    {
        _innerDictionary = new Dictionary<TKey, (TValue1 Value1, TValue2 Value2)>();
    }

    public Dictionary(IDictionary<TKey, (TValue1 Value1, TValue2 Value2)> dictionary)
    {
        _innerDictionary = new Dictionary<TKey, (TValue1 Value1, TValue2 Value2)>(dictionary);
    }

    public Dictionary(IDictionary<TKey, (TValue1 Value1, TValue2 Value2)> dictionary, IEqualityComparer<TKey>? comparer)
    {
        _innerDictionary = new Dictionary<TKey, (TValue1 Value1, TValue2 Value2)>(dictionary, comparer);
    }

    public Dictionary(IEqualityComparer<TKey>? comparer)
    {
        _innerDictionary = new Dictionary<TKey, (TValue1 Value1, TValue2 Value2)>(comparer);
    }

    public Dictionary(int capacity)
    {
        _innerDictionary = new Dictionary<TKey, (TValue1 Value1, TValue2 Value2)>(capacity);
    }

    public Dictionary(int capacity, IEqualityComparer<TKey>? comparer)
    {
        _innerDictionary = new Dictionary<TKey, (TValue1 Value1, TValue2 Value2)>(capacity, comparer);
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)>> GetEnumerator()
    {
        return _innerDictionary.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_innerDictionary).GetEnumerator();
    }

    /// <inheritdoc />
    void ICollection<KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)>>.Add(
        KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)> item)
    {
        _innerDictionary.Add(item);
    }

    public void Add(TKey key, TValue1 value1, TValue2 value2)
    {
        _innerDictionary.Add(new KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)>(key, (value1, value2)));
    }

    public void Add(TKey key, (TValue1 Value1, TValue2 Value2) value)
    {
        _innerDictionary.Add(key, value);
    }

    /// <inheritdoc />
    public void Clear()
    {
        _innerDictionary.Clear();
    }

    /// <inheritdoc />
    bool ICollection<KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)>>.Contains(
        KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)> item)
    {
        return _innerDictionary.Contains(item);
    }

    /// <inheritdoc />
    void ICollection<KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)>>.CopyTo(
        KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)>[] array, int arrayIndex)
    {
        _innerDictionary.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    bool ICollection<KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)>>.Remove(
        KeyValuePair<TKey, (TValue1 Value1, TValue2 Value2)> item)
    {
        return _innerDictionary.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _innerDictionary.Count;

    /// <inheritdoc />
    public bool IsReadOnly => _innerDictionary.IsReadOnly;

    /// <inheritdoc />
    void IDictionary<TKey, (TValue1 Value1, TValue2 Value2)>.Add(TKey key, (TValue1 Value1, TValue2 Value2) value)
    {
        _innerDictionary.Add(key, value);
    }

    /// <inheritdoc />
    public bool ContainsKey(TKey key)
    {
        return _innerDictionary.ContainsKey(key);
    }

    /// <inheritdoc />
    public bool Remove(TKey key)
    {
        return _innerDictionary.Remove(key);
    }

    /// <inheritdoc />
    bool IDictionary<TKey, (TValue1 Value1, TValue2 Value2)>.TryGetValue(TKey key,
        out (TValue1 Value1, TValue2 Value2) value)
    {
        return _innerDictionary.TryGetValue(key, out value);
    }

    public bool TryGetValue(TKey key, out TValue1 value1, out TValue2 value2)
    {
        bool result = _innerDictionary.TryGetValue(key, out (TValue1 Value1, TValue2 Value2) value);
        value1 = value.Value1;
        value2 = value.Value2;
        return result;
    }

    /// <inheritdoc cref="IDictionary{TKey,TValue}.this" />
    public (TValue1 Value1, TValue2 Value2) this[TKey key]
    {
        get => _innerDictionary[key];
        set => _innerDictionary[key] = value;
    }

    (TValue1, TValue2) IDictionary<TKey, TValue1, TValue2>.this[TKey key]
    {
        get => _innerDictionary[key];
        set => _innerDictionary[key] = value;
    }

    /// <inheritdoc />
    public ICollection<TKey> Keys => _innerDictionary.Keys;

    /// <inheritdoc />
    public ICollection<(TValue1 Value1, TValue2 Value2)> Values => _innerDictionary.Values;
}
