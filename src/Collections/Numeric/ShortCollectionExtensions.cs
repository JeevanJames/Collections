// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

#if NETSTANDARD2_0

using System.Globalization;
using System.Text;

// ReSharper disable CheckNamespace
#if EXPLICIT
namespace Collections.Net.Extensions.Numeric;
#else
namespace System.Collections.Generic;
#endif

/// <summary>
///     Set of utility extension methods for short arrays and collections.
/// </summary>
public static class ShortCollectionExtensions
{
    /// <summary>
    ///     Checks whether two short collections are equal based on their content.
    /// </summary>
    /// <param name="shorts">The source short collection to check.</param>
    /// <param name="other">The second short collection to check.</param>
    /// <returns><c>true</c> if the contents of the two short collections are equal.</returns>
    public static bool IsEqualTo(this IList<short>? shorts, IList<short>? other)
    {
        if (ReferenceEquals(shorts, other))
            return true;
        if (shorts is null || other is null)
            return false;
        if (shorts.Count != other.Count)
            return false;
        for (int i = 0; i < shorts.Count; i++)
        {
            if (shorts[i] != other[i])
                return false;
        }

        return true;
    }

    /// <summary>
    ///     Checks whether two short collections are equal based on their content.
    /// </summary>
    /// <param name="shorts">The source short collection to check.</param>
    /// <param name="other">The second short collection to check.</param>
    /// <returns><c>true</c> if the contents of the two short collection are equal.</returns>
    public static bool IsEqualTo(this IList<short> shorts, params short[] other)
    {
        return IsEqualTo(shorts, (IList<short>)other);
    }

    /// <summary>
    ///     Indicates whether the specified short collection is <c>null</c>, does not contain any elements or consists
    ///     of only zero value items.
    /// </summary>
    /// <param name="shorts">The short collection to test.</param>
    /// <returns>
    ///     <c>true</c> if the short collection is <c>null</c>, does not contain any elements or consists exclusively
    ///     of zero value items.
    /// </returns>
    public static bool IsNullOrZeroed(this ICollection<short>? shorts)
    {
        if (shorts is null || shorts.Count == 0)
            return true;
        return shorts.All(b => b == default);
    }

    /// <summary>
    ///     Indicates whether the specified short collection does not contain any elements or consists of only zero
    ///     value items.
    /// </summary>
    /// <param name="shorts">The short collection to test.</param>
    /// <returns>
    ///     <c>true</c> if the short collection is <c>null</c>, does not contain any elements or consists exclusively
    ///     of zero value items.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the short collection is <c>null</c>.</exception>
    public static bool IsZeroed(this ICollection<short> shorts)
    {
        if (shorts is null)
            throw new ArgumentNullException(nameof(shorts));
        return shorts.All(b => b == default);
    }

    /// <summary>
    ///     Creates a <see cref="string"/> from a short collection that concatenates each item in the collection,
    ///     separated by the specified delimiter.
    /// </summary>
    /// <param name="source">The short collection from which to create the string.</param>
    /// <param name="delimiter">The optional delimiter to separate each item in the collection.</param>
    /// <returns>The combined string.</returns>
    public static string? ToString(this IList<short>? source, string? delimiter)
    {
        if (source is null)
            return null;
        if (source.Count == 0)
            return string.Empty;

        delimiter ??= string.Empty;
        bool useDelimiter = delimiter.Length > 0;

        var result = new StringBuilder(source[0].ToString(CultureInfo.CurrentCulture),
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
    ///     Retrieves the shorts from a short array upto a specific sequence.
    /// </summary>
    /// <param name="source">The short array.</param>
    /// <param name="start">The index in the array to start searching.</param>
    /// <param name="sequence">The sequence to search for.</param>
    /// <returns>An array of shorts from the starting index to the matching sequence.</returns>
    public static short[]? GetNumbersUptoSequence(this short[] source, int start, params short[] sequence)
    {
        int sequenceIndex = IndexOfSequence(source, start, source.Length - start + 1, sequence);
        if (sequenceIndex == -1)
            return null;

        short[] result = new short[sequenceIndex - start];
        Array.Copy(source, start, result, 0, result.Length);
        return result;
    }

    public static int IndexOfSequence(this IList<short>? source, params short[] sequence) =>
        IndexOfSequence(source, 0, source?.Count ?? 0, (IList<short>)sequence);

    public static int IndexOfSequence(this IList<short>? source, int start, int count, params short[] sequence) =>
        IndexOfSequence(source, start, count, (IList<short>)sequence);

    public static int IndexOfSequence(this IList<short>? source, int start, int count, IList<short>? sequence)
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
        for (int shortIdx = start; shortIdx < endIndex; shortIdx++)
        {
            if (source[shortIdx] == sequence[sequenceIndex])
            {
                sequenceIndex++;
                if (sequenceIndex >= sequence.Count)
                    return shortIdx - sequence.Count + 1;
            }
            else
                sequenceIndex = 0;
        }

        return -1;
    }

    public static int[] IndexOfSequences(this IList<short>? source, params short[] sequence)
    {
        return IndexOfSequences(source, 0, source?.Count ?? 0, sequence);
    }

    public static int[] IndexOfSequences(this IList<short>? source, int start, int count, params short[] sequence)
    {
        return IndexOfSequences(source, start, count, (IList<short>)sequence);
    }

    public static int[] IndexOfSequences(this IList<short>? source, int start, int count, IList<short>? sequence)
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

    public static short[][] SplitBySequence(this short[] source, params short[] sequence) =>
        SplitBySequence(source, 0, source.Length, sequence);

    public static short[][] SplitBySequence(this short[] source, int start, int count, params short[] sequence)
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

        var results = new List<short[]>(locations.Length + 1);
        for (int locationIdx = 0; locationIdx < locations.Length; locationIdx++)
        {
            int startIndex = locationIdx > 0 ? locations[locationIdx - 1] + sequence.Length : start;
            int endIndex = locations[locationIdx] - 1;
            if (endIndex < startIndex)
                results.Add(Array.Empty<short>());
            else
            {
                short[] splitItems = new short[endIndex - startIndex + 1];
                Array.Copy(source, startIndex, splitItems, 0, splitItems.Length);
                results.Add(splitItems);
            }
        }

        if (locations[locations.Length - 1] + sequence.Length > start + count - 1)
            results.Add(Array.Empty<short>());
        else
        {
            short[] splitItems = new short[start + count - locations[locations.Length - 1] - sequence.Length];
            Array.Copy(source, locations[locations.Length - 1] + sequence.Length, splitItems, 0, splitItems.Length);
            results.Add(splitItems);
        }

        return results.ToArray();
    }
}

#endif
