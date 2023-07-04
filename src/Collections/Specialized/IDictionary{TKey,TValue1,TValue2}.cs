// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

#if EXPLICIT
namespace Collections.Net.Specialized;
#else
// ReSharper disable once CheckNamespace
namespace System.Collections.Specialized;
#endif

public interface IDictionary<in TKey, TValue1, TValue2>
    where TKey : notnull
{
    void Add(TKey key, TValue1 value1, TValue2 value2);

    void Add(TKey key, (TValue1 Value1, TValue2 Value2) value);

    bool TryGetValue(TKey key, out TValue1 value1, out TValue2 value2);

    (TValue1, TValue2) this[TKey key] { get; set; }
}
