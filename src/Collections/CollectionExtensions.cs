// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
#if EXPLICIT
namespace Collections.Net.Extensions.CollectionExtensions;
#else
namespace System.Collections.Generic;
#endif

public static class CollectionExtensions
{
    public static void AddRange<T>(this ICollection<T> collection, params T[] items) =>
        AddRange(collection, (IEnumerable<T>)items);

    /// <summary>
    ///     Adds the elements of the specified <see cref="IEnumerable{T}" /> to the collection
    /// </summary>
    /// <typeparam name="T">The type of the elements of the collection</typeparam>
    /// <param name="collection">The collection to add the items to.</param>
    /// <param name="items">
    ///     The <see cref="IEnumerable{T}" /> whose elements should be added to the end of the collection. The
    ///     <see cref="IEnumerable{T}" /> itself cannot be null, but it can contain elements that are null, if type T is a
    ///     reference type.
    /// </param>
    /// <exception cref="ArgumentNullException">collection is null</exception>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T>? items)
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));
        if (items is null)
            return;

        foreach (T item in items)
            collection.Add(item);
    }

    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T>? items, Func<T, bool> predicate)
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));
        if (items is null)
            return;

        collection.AddRange(items.Where(predicate));
    }

    public static void AddRange<TDest, TSource>(this ICollection<TDest> collection, IEnumerable<TSource>? items,
        Func<TSource, TDest> converter)
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));
        if (converter is null)
            throw new ArgumentNullException(nameof(converter));
        if (items is null)
            return;

        collection.AddRange(items.Select(converter));
    }

    public static void AddRange<TDest, TSource>(this ICollection<TDest> collection, IEnumerable<TSource>? items,
        Func<TSource, bool> predicate, Func<TSource, TDest> converter, Func<TDest, bool>? afterConvertPredicate = null)
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));
        if (converter is null)
            throw new ArgumentNullException(nameof(converter));
        if (items is null)
            return;

        IEnumerable<TDest> convertedItems = items.Where(predicate).Select(converter);
        if (afterConvertPredicate is not null)
            convertedItems = convertedItems.Where(afterConvertPredicate);
        collection.AddRange(convertedItems);
    }

    /// <summary>
    ///     Returns the range of elements from the specified start and end index of a collection.
    /// </summary>
    /// <typeparam name="T">The type of the elements of collection.</typeparam>
    /// <param name="collection">The collection.</param>
    /// <param name="start">
    ///     The inclusive start index of the range. If <c>null</c>, then this will default to zero.
    /// </param>
    /// <param name="end">
    ///     The exclusive end index of the range. If <c>null</c>, then this will default to the number of elements
    ///     in the collection.
    /// </param>
    /// <returns>An <see cref="IEnumerable{T}"/> with the elements from the specified range.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the collection is <c>null</c>.</exception>
    public static IEnumerable<T> Range<T>(this ICollection<T> collection, int? start = null, int? end = null)
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));

        int startIndex = start.GetValueOrDefault();
        int endIndex = end.GetValueOrDefault(collection.Count);

        if (startIndex < 0 || startIndex >= collection.Count)
            throw new ArgumentOutOfRangeException(nameof(start));
        if (endIndex < startIndex)
            throw new ArgumentOutOfRangeException(nameof(end), $"End index {endIndex} should be greater or equal to start index {startIndex}.");
        if (endIndex > collection.Count)
            endIndex = collection.Count;

        return collection.Skip(startIndex).Take(endIndex - startIndex);
    }

    public static IEnumerable<T> RangeOfLength<T>(this ICollection<T> collection, int? start = null,
        int? count = null)
    {
        return Range(collection, start, count.HasValue ? (int?)start.GetValueOrDefault() + count.Value : null);
    }
}
