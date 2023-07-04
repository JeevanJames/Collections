// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

#if EXPLICIT
namespace Collections.Net.Specialized;
#else
// ReSharper disable once CheckNamespace
namespace System.Collections.Specialized;
#endif

public interface IMultiValueDictionary<TKey, TValue> : IDictionary<TKey, IList<TValue>>
{
    void Add(TKey key, params TValue[] values);

    void Add(TKey key, IEnumerable<TValue> values);
}
