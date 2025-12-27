// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

#if NET8_0_OR_GREATER

using System.Numerics;
using System.Text;

// ReSharper disable CheckNamespace
#if EXPLICIT
namespace Collections.Net.Extensions.Numeric;
#else
namespace System.Collections.Generic;
#endif

/// <summary>
///     Set of utility extension methods for numeric arrays and collections.
/// </summary>
public static class NumberCollectionExtensions
{
    /// <summary>
    ///     Checks whether two numeric collections are equal based on their content.
    /// </summary>
    /// <param name="source">The source numeric collection to check.</param>
    /// <param name="other">The second numeric collection to check.</param>
    /// <returns><c>true</c> if the contents of the two numeric collections are equal.</returns>
    public static bool IsEqualTo<T>(this IList<T>? source, IList<T>? other)
        where T : INumber<T>
    {
        if (ReferenceEquals(source, other))
            return true;
        if (source is null || other is null)
            return false;
        if (source.Count != other.Count)
            return false;
        for (int i = 0; i < source.Count; i++)
        {
            if (source[i] != other[i])
                return false;
        }

        return true;
    }

    /// <summary>
    ///     Checks whether two numeric collections are equal based on their content.
    /// </summary>
    /// <param name="source">The source numeric collection to check.</param>
    /// <param name="other">The second numeric collection to check.</param>
    /// <returns><c>true</c> if the contents of the two numeric collection are equal.</returns>
    public static bool IsEqualTo<T>(this IList<T> source, params T[] other)
        where T : INumber<T>
    {
        return IsEqualTo(source, (IList<T>)other);
    }

    /// <summary>
    ///     Indicates whether the specified numeric collection is <c>null</c>, does not contain any
    ///     elements or consists of only zero value items.
    /// </summary>
    /// <param name="source">The numeric collection to test.</param>
    /// <returns>
    ///     <c>true</c> if the numeric collection is <c>null</c>, does not contain any elements or
    ///     consists exclusively of zero value items.
    /// </returns>
    public static bool IsNullOrZeroed<T>(this ICollection<T>? source)
        where T : INumber<T>
    {
        return source is null || source.Count == 0 || source.All(b => b == default);
    }

    /// <summary>
    ///     Indicates whether the specified numeric collection does not contain any elements or consists
    ///     of only zero value items.
    /// </summary>
    /// <param name="source">The numeric collection to test.</param>
    /// <returns>
    ///     <c>true</c> if the numeric collection is <c>null</c>, does not contain any elements or
    ///     consists exclusively of zero value items.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the numeric collection is <c>null</c>.</exception>
    public static bool IsZeroed<T>(this ICollection<T> source)
        where T : INumber<T>
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        return source.All(b => b == default);
    }

    /// <summary>
    ///     Creates a <see cref="string"/> from a numeric collection that concatenates each item in
    ///     the collection, separated by the specified delimiter.
    /// </summary>
    /// <param name="source">The numeric collection from which to create the string.</param>
    /// <param name="delimiter">The optional delimiter to separate each item in the collection.</param>
    /// <returns>The combined string.</returns>
    public static string? ToString<T>(this IList<T>? source, string? delimiter)
        where T : INumber<T>
    {
        if (source is null)
            return null;
        if (source.Count == 0)
            return string.Empty;

        delimiter ??= string.Empty;
        bool useDelimiter = delimiter.Length > 0;

        var result = new StringBuilder(source[0].ToString(),
            (source.Count * 3) + ((source.Count - 1) * delimiter.Length));
        for (int i = 1; i < source.Count; i++)
        {
            if (useDelimiter)
                result.Append(delimiter);
            result.Append(source[i]);
        }

        return result.ToString();
    }

    /// <summary>
    ///     Retrieves the elements from a numeric array upto a specific sequence.
    /// </summary>
    /// <param name="source">The numeric array.</param>
    /// <param name="start">The index in the array to start searching.</param>
    /// <param name="sequence">The sequence to search for.</param>
    /// <returns>An array of numbers from the starting index to the matching sequence.</returns>
    public static T[]? GetNumbersUptoSequence<T>(this T[] source, int start, params T[] sequence)
        where T : INumber<T>
    {
        int sequenceIndex = IndexOfSequence(source, start, source.Length - start + 1, sequence);
        if (sequenceIndex == -1)
            return null;

        var result = new T[sequenceIndex - start];
        Array.Copy(source, start, result, 0, result.Length);
        return result;
    }

    /// <summary>
    ///     Returns the index of the first occurrence of the specified sequence in the numeric collection.
    /// </summary>
    /// <typeparam name="T">The type of the numeric collection's elements.</typeparam>
    /// <param name="source">The numeric collection.</param>
    /// <param name="sequence">The sequence to find in the numeric collection.</param>
    /// <returns>
    ///     The index of the sequence within the numeric collection, or <c>-1</c> if the sequence could
    ///     not be found.
    /// </returns>
    public static int IndexOfSequence<T>(this IList<T>? source, params T[] sequence)
        where T : INumber<T>
    {
        return IndexOfSequence(source, 0, source?.Count ?? 0, (IList<T>)sequence);
    }

    public static int IndexOfSequence<T>(this IList<T>? source, int start, int count, params T[] sequence)
        where T : INumber<T>
    {
        return IndexOfSequence(source, start, count, (IList<T>)sequence);
    }

    public static int IndexOfSequence<T>(this IList<T>? source, int start, int count, IList<T>? sequence)
        where T : INumber<T>
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));

        int sequenceIndex = 0;
        int endIndex = Math.Min(source.Count, start + count);
        for (int i = start; i < endIndex; i++)
        {
            if (source[i] == sequence[sequenceIndex])
            {
                sequenceIndex++;
                if (sequenceIndex >= sequence.Count)
                    return i - sequence.Count + 1;
            }
            else
                sequenceIndex = 0;
        }

        return -1;
    }

    public static int[] IndexOfSequences<T>(this IList<T>? source, params T[] sequence)
        where T : INumber<T>
    {
        return IndexOfSequences(source, 0, source?.Count ?? 0, sequence);
    }

    public static int[] IndexOfSequences<T>(this IList<T>? source, int start, int count, params T[] sequence)
        where T : INumber<T>
    {
        return IndexOfSequences(source, start, count, (IList<T>)sequence);
    }

    public static int[] IndexOfSequences<T>(this IList<T>? source, int start, int count, IList<T>? sequence)
        where T : INumber<T>
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start));
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));

        var locations = new List<int>();

        int sequenceLocation = IndexOfSequence(source, start, count, sequence);
        while (sequenceLocation >= 0)
        {
            locations.Add(sequenceLocation);
            count -= sequenceLocation - start + 1;
            start = sequenceLocation + sequence.Count;
            sequenceLocation = IndexOfSequence(source, start, count, sequence);
        }

        return locations.ToArray();
    }

    public static T[][] SplitBySequence<T>(this T[] source, params T[] sequence)
        where T : INumber<T>
    {
        return SplitBySequence(source, 0, source.Length, sequence);
    }

    public static T[][] SplitBySequence<T>(this T[] source, int start, int count, params T[] sequence)
        where T : INumber<T>
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start));
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (sequence.Length == 0)
            throw new ArgumentException("Sequence cannot be empty.", nameof(sequence));

        if (start + count > source.Length)
            count = source.Length - start;

        int[] locations = IndexOfSequences(source, start, count, sequence);
        if (locations.Length == 0)
            return new[] { source };

        var results = new List<T[]>(locations.Length + 1);
        for (int locationIdx = 0; locationIdx < locations.Length; locationIdx++)
        {
            int startIndex = locationIdx > 0 ? locations[locationIdx - 1] + sequence.Length : start;
            int endIndex = locations[locationIdx] - 1;
            if (endIndex < startIndex)
                results.Add(Array.Empty<T>());
            else
            {
                var splitItems = new T[endIndex - startIndex + 1];
                Array.Copy(source, startIndex, splitItems, 0, splitItems.Length);
                results.Add(splitItems);
            }
        }

        if (locations[^1] + sequence.Length > start + count - 1)
            results.Add(Array.Empty<T>());
        else
        {
            var splitItems = new T[start + count - locations[^1] - sequence.Length];
            Array.Copy(source, locations[^1] + sequence.Length, splitItems, 0, splitItems.Length);
            results.Add(splitItems);
        }

        return results.ToArray();
    }
}

#endif
