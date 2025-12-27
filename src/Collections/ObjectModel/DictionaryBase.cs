using System.Diagnostics.CodeAnalysis;

#if EXPLICIT
using System.Collections;

namespace Collections.Net.ObjectModel;
#else
// ReSharper disable once CheckNamespace
namespace System.Collections.ObjectModel;
#endif

public abstract partial class DictionaryBase<TKey, TValue> : IDictionary<TKey, TValue>
    where TKey : notnull
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

#if NET8_0_OR_GREATER
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
#else
    public bool TryGetValue(TKey key, out TValue value)
#endif
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

public abstract partial class DictionaryBase<TKey, TValue>
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
