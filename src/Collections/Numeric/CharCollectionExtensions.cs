// Copyright (c) 2018-2026 Jeevan James
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
///     Set of utility extension methods for char arrays and collections.
/// </summary>
public static class CharCollectionExtensions
{
    /// <summary>
    ///     Checks whether two char collections are equal based on their content.
    /// </summary>
    /// <param name="chars">The source char collection to check.</param>
    /// <param name="other">The second char collection to check.</param>
    /// <returns><c>true</c> if the contents of the two char collections are equal.</returns>
    public static bool IsEqualTo(this IList<char>? chars, IList<char>? other)
    {
        if (ReferenceEquals(chars, other))
            return true;
        if (chars is null || other is null)
            return false;
        if (chars.Count != other.Count)
            return false;
        for (int i = 0; i < chars.Count; i++)
        {
            if (chars[i] != other[i])
                return false;
        }

        return true;
    }

    /// <summary>
    ///     Checks whether two char collections are equal based on their content.
    /// </summary>
    /// <param name="chars">The source char collection to check.</param>
    /// <param name="other">The second char collection to check.</param>
    /// <returns><c>true</c> if the contents of the two char collection are equal.</returns>
    public static bool IsEqualTo(this IList<char> chars, params char[] other)
    {
        return IsEqualTo(chars, (IList<char>)other);
    }

    /// <summary>
    ///     Indicates whether the specified char collection is <c>null</c>, does not contain any elements or consists
    ///     of only zero value items.
    /// </summary>
    /// <param name="chars">The char collection to test.</param>
    /// <returns>
    ///     <c>true</c> if the char collection is <c>null</c>, does not contain any elements or consists exclusively
    ///     of zero value items.
    /// </returns>
    public static bool IsNullOrZeroed(this ICollection<char>? chars)
    {
        return chars is null || chars.Count == 0 || chars.All(b => b == default);
    }

    /// <summary>
    ///     Indicates whether the specified char collection does not contain any elements or consists of only zero
    ///     value items.
    /// </summary>
    /// <param name="chars">The char collection to test.</param>
    /// <returns>
    ///     <c>true</c> if the char collection is <c>null</c>, does not contain any elements or consists exclusively
    ///     of zero value items.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the char collection is <c>null</c>.</exception>
    public static bool IsZeroed(this ICollection<char> chars)
    {
        if (chars is null)
            throw new ArgumentNullException(nameof(chars));
        return chars.All(b => b == default);
    }

    /// <summary>
    ///     Creates a <see cref="string"/> from a char collection that concatenates each item in the collection,
    ///     separated by the specified delimiter.
    /// </summary>
    /// <param name="source">The char collection from which to create the string.</param>
    /// <param name="delimiter">The optional delimiter to separate each item in the collection.</param>
    /// <returns>The combined string.</returns>
    public static string? ToString(this IList<char>? source, string? delimiter)
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
    ///     Retrieves the chars from a char array upto a specific sequence.
    /// </summary>
    /// <param name="source">The char array.</param>
    /// <param name="start">The index in the array to start searching.</param>
    /// <param name="sequence">The sequence to search for.</param>
    /// <returns>An array of chars from the starting index to the matching sequence.</returns>
    public static char[]? GetNumbersUptoSequence(this char[] source, int start, params char[] sequence)
    {
        int sequenceIndex = IndexOfSequence(source, start, source.Length - start + 1, sequence);
        if (sequenceIndex == -1)
            return null;

        char[] result = new char[sequenceIndex - start];
        Array.Copy(source, start, result, 0, result.Length);
        return result;
    }

    public static int IndexOfSequence(this IList<char>? source, params char[] sequence) =>
        IndexOfSequence(source, 0, source?.Count ?? 0, (IList<char>)sequence);

    public static int IndexOfSequence(this IList<char>? source, int start, int count, params char[] sequence) =>
        IndexOfSequence(source, start, count, (IList<char>)sequence);

    public static int IndexOfSequence(this IList<char>? source, int start, int count, IList<char>? sequence)
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
        for (int charIdx = start; charIdx < endIndex; charIdx++)
        {
            if (source[charIdx] == sequence[sequenceIndex])
            {
                sequenceIndex++;
                if (sequenceIndex >= sequence.Count)
                    return charIdx - sequence.Count + 1;
            }
            else
                sequenceIndex = 0;
        }

        return -1;
    }

    public static int[] IndexOfSequences(this IList<char>? source, params char[] sequence)
    {
        return IndexOfSequences(source, 0, source?.Count ?? 0, sequence);
    }

    public static int[] IndexOfSequences(this IList<char>? source, int start, int count, params char[] sequence)
    {
        return IndexOfSequences(source, start, count, (IList<char>)sequence);
    }

    public static int[] IndexOfSequences(this IList<char>? source, int start, int count, IList<char>? sequence)
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

    public static char[][] SplitBySequence(this char[] source, params char[] sequence) =>
        SplitBySequence(source, 0, source.Length, sequence);

    public static char[][] SplitBySequence(this char[] source, int start, int count, params char[] sequence)
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

        var results = new List<char[]>(locations.Length + 1);
        for (int locationIdx = 0; locationIdx < locations.Length; locationIdx++)
        {
            int startIndex = locationIdx > 0 ? locations[locationIdx - 1] + sequence.Length : start;
            int endIndex = locations[locationIdx] - 1;
            if (endIndex < startIndex)
                results.Add(Array.Empty<char>());
            else
            {
                char[] splitItems = new char[endIndex - startIndex + 1];
                Array.Copy(source, startIndex, splitItems, 0, splitItems.Length);
                results.Add(splitItems);
            }
        }

        if (locations[locations.Length - 1] + sequence.Length > start + count - 1)
            results.Add(Array.Empty<char>());
        else
        {
            char[] splitItems = new char[start + count - locations[locations.Length - 1] - sequence.Length];
            Array.Copy(source, locations[locations.Length - 1] + sequence.Length, splitItems, 0, splitItems.Length);
            results.Add(splitItems);
        }

        return results.ToArray();
    }
}

#endif
