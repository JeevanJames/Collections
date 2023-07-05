// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
#if EXPLICIT
using Collections.Net.Extensions.ListExtensions;

namespace Collections.Net.Extensions.EnumerableExtensions;
#else
namespace System.Collections.Generic;
#endif

public static class EnumerableExtensions
{
    /// <summary>
    ///     Determines whether all items in a <paramref name="sequence"/> satisfy the specified
    ///     <paramref name="predicate"/>. If not, the first non-compliant item is returned.
    /// </summary>
    /// <typeparam name="T">The type of the items of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="predicate">The predicate to check each item against.</param>
    /// <param name="nonCompliantItem">The first non-compliant item in the sequence.</param>
    /// <returns>
    ///     <c>true</c>, if all or none of the elements in the sequence match the predicate. If some
    ///     elements match and others do not, then <c>false</c> is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown of the <paramref name="sequence"/> or <paramref name="predicate"/> is <c>null</c>.
    /// </exception>
    public static bool All<T>(this IEnumerable<T> sequence, Func<T, bool> predicate, out T nonCompliantItem)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        foreach (T item in sequence)
        {
            if (!predicate(item))
            {
                nonCompliantItem = item;
                return false;
            }
        }

        nonCompliantItem = default!;
        return true;
    }

    /// <summary>
    ///     Determines whether all or none of the elements in a <paramref name="sequence"/> match
    ///     the specified <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
    /// <returns>
    ///     <c>true</c>, if all or none of the elements in the sequence match the predicate. If some
    ///     elements match and others do not, then <c>false</c> is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the <paramref name="sequence"/> or <paramref name="predicate"/> is
    ///     <c>null</c>.
    /// </exception>
    public static bool AllOrNone<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        // Stores the result of the predicate call for the first element. If the
        bool? expectedPredicateResult = null;

        foreach (T element in sequence)
        {
            bool elementResult = predicate(element);

            // If first element, set the tracking state variable. All subsequent elements should
            // return the same value for the predicate.
            if (!expectedPredicateResult.HasValue)
                expectedPredicateResult = elementResult;

            // If the predicate for the current element is different from the tracking value, then
            // the sequence contains elements that return different values for the predicate and
            // hence return false.
            else if (elementResult != expectedPredicateResult.Value)
                return false;
        }

        return true;
    }

    public static bool AllItemsContainedIn<T>(this IEnumerable<T> sequence, IEnumerable<T> collection,
        Comparison<T>? comparison = null)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));

        Func<T, bool> check = comparison is null
            ? item => collection.Any(c => Equals(c, item))
            : item => collection.Any(c => comparison(item, c) == 0);
        return sequence.All(check);
    }

    /// <summary>
    ///     Determines whether any element of a <paramref name="sequence"/> satisfies all of the
    ///     specified conditions (<paramref name="predicates"/>)
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="predicates">
    ///     One or more functions to test the conditions for each item in the sequence.
    /// </param>
    /// <returns>
    ///     <c>true</c> if any item in the sequence satisfies all of the specified predicates;
    ///     otherwise <c>false</c>.
    /// </returns>
    public static bool AnyItemMatchesAll<T>(this IEnumerable<T> sequence, params Func<T, bool>[] predicates)
    {
        if (predicates is null)
            throw new ArgumentNullException(nameof(predicates));

        if (predicates.Length == 0)
            return true;

        if (predicates.Length == 1)
            return sequence.Any(predicates[0]);

        return sequence.Any(item => predicates.All(predicate => predicate(item)));
    }

    /// <summary>
    ///     Determines whether any element of a <paramref name="sequence"/> satisfies any of the
    ///     specified conditions (<paramref name="predicates"/>)
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="predicates">
    ///     One or more functions to test the conditions for each item in the sequence.
    /// </param>
    /// <returns>
    ///     <c>true</c> if any item in the sequence satisfies any of the specified predicates;
    ///     otherwise <c>false</c>.
    /// </returns>
    public static bool AnyItemMatchesAny<T>(this IEnumerable<T> sequence, params Func<T, bool>[] predicates)
    {
        if (predicates is null)
            throw new ArgumentNullException(nameof(predicates));

        if (predicates.Length == 0)
            return true;

        // If only one predicate, just call the original LINQ Any method.
        if (predicates.Length == 1)
            return sequence.Any(predicates[0]);

        return sequence.Any(item =>
        {
            foreach (Func<T, bool> predicate in predicates)
            {
                if (predicate(item))
                    return true;
            }

            return false;
        });
    }
    public static bool AnyItemContainedIn<T>(this IEnumerable<T> sequence, IEnumerable<T> collection,
        Comparison<T>? comparison = null)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));

        Func<T, bool> check = comparison is null
            ? item => collection.Any(c => Equals(c, item))
            : item => collection.Any(c => comparison(item, c) == 0);
        return sequence.Any(check);
    }

    public static IEnumerable<T[]> ChunkEx<T>(this IEnumerable<T> sequence, int chunkSize)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (chunkSize < 1)
            throw new ArgumentOutOfRangeException(nameof(chunkSize));

        T[]? chunk = null;
        int currentIndex = 0;

        foreach (T element in sequence)
        {
            if (currentIndex == 0)
                chunk = new T[chunkSize];
            chunk![currentIndex++] = element;
            if (currentIndex >= chunkSize)
            {
                yield return chunk;
                currentIndex = 0;
            }
        }

        if (chunk is not null && currentIndex > 0)
        {
            var trailingChunk = new T[currentIndex];
            Array.Copy(chunk, 0, trailingChunk, 0, currentIndex);
            yield return trailingChunk;
        }
    }

    /// <summary>
    ///     Performs an <paramref name="action"/> on all elements of a <paramref name="sequence"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="action">The action to perform.</param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown of the <paramref name="action"/> is <c>null</c>.
    /// </exception>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T>? sequence, Action<T> action)
    {
        if (sequence is null)
            return Enumerable.Empty<T>();
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        foreach (T item in sequence)
            action(item);
        return sequence;
    }

    /// <summary>
    ///     Performs an <paramref name="action"/> on all elements of a <paramref name="sequence"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="action">The action to perform.</param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown of the <paramref name="action"/> is <c>null</c>.
    /// </exception>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T>? sequence, Action<T, int> action)
    {
        if (sequence is null)
            return Enumerable.Empty<T>();
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        int index = 0;
        foreach (T item in sequence)
            action(item, index++);
        return sequence;
    }

    /// <summary>
    ///     Indicates whether the specified sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the sequence</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <returns><c>true</c> if the sequence is empty; otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the source sequence is <c>null</c>.
    /// </exception>
    public static bool IsEmpty<T>(this IEnumerable<T> sequence)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        return sequence is ICollection<T> coll ? coll.Count == 0 : !sequence.Any();
    }

    /// <summary>
    ///     Indicates whether the specified <paramref name="sequence"/> is not <c>null</c> and has
    ///     at least one element.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the sequence</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <returns><c>true</c>, if the sequence if not <c>null</c> and has elements.</returns>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? sequence)
    {
        return sequence is not null && sequence.Any();
    }

    /// <summary>
    ///     Indicates whether the specified sequence is null or does not contain any elements.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the sequence</typeparam>
    /// <param name="sequence"> The sequence to test. </param>
    /// <returns>
    ///     <c>true</c> if the sequence is either null or empty; otherwise <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? sequence)
    {
        if (sequence is null)
            return true;
        return sequence is ICollection<T> coll ? coll.Count == 0 : !sequence.Any();
    }

    /// <summary>
    ///     Determines whether none of the elements of the <paramref name="sequence"/> satisfies the
    ///     specified <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
    /// <returns>
    ///     <c>true</c> if none of the elements in the <paramref name="sequence"/> satisfy the
    ///     <paramref name="predicate"/>; otherwise <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the <paramref name="sequence"/> or <paramref name="predicate"/> is
    ///     <c>null</c>.
    /// </exception>
    public static bool None<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return !sequence.Any(predicate);
    }

    public static IEnumerable<T> NotOfType<T, TUnwanted>(this IEnumerable<T>? sequence)
    {
        if (sequence is null)
            return Enumerable.Empty<T>();
        return NotOfTypeIterator<T, TUnwanted>(sequence);
    }

    private static IEnumerable<T> NotOfTypeIterator<T, TUnwanted>(IEnumerable<T> sequence)
    {
        foreach (T item in sequence)
        {
            if (item is not TUnwanted)
                yield return item;
        }
    }

    /// <summary>
    ///     Checks each element of the <paramref name="sequence"/> against the specified
    ///     <paramref name="predicate"/> and returns the elements that match and those that do not
    ///     match.
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
    /// <returns>
    ///     Two collections sequences - one with the elements that match the
    ///     <paramref name="predicate"/> and another with elements that do not match.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the <paramref name="sequence"/> or <paramref name="predicate"/> is
    ///     <c>null</c>.
    /// </exception>
    public static (IEnumerable<T> matches, IEnumerable<T> mismatches) Partition<T>(this IEnumerable<T> sequence,
        Func<T, bool> predicate)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        var matches = new List<T>();
        var mismatches = new List<T>();

        foreach (T element in sequence)
        {
            if (predicate(element))
                matches.Add(element);
            else
                mismatches.Add(element);
        }

        return (matches, mismatches);
    }

    /// <summary>
    ///     Repeats the <paramref name="sequence"/> <paramref name="count"/> number of times.
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="count">The number of times to repeat the sequence.</param>
    /// <returns>The <paramref name="sequence"/> repeated <paramref name="count"/> number of times.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the <paramref name="sequence"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown of the <paramref name="count"/> is less than one.
    /// </exception>
    public static IEnumerable<T> Repeat<T>(this IEnumerable<T> sequence, int count)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        for (int i = 0; i < count; i++)
        {
            foreach (T item in sequence)
                yield return item;
        }
    }

    /// <summary>
    ///     Returns a shuffled version of the <paramref name="sequence"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements of sequence.</typeparam>
    /// <param name="sequence">The sequence.</param>
    /// <param name="iterations">The number of times to repeat the shuffle operation.</param>
    /// <returns>A shuffled version of the <paramref name="sequence"/>.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the <paramref name="sequence"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if the <paramref name="iterations"/> is less than one.
    /// </exception>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence, int iterations = 1)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (iterations < 1)
            throw new ArgumentOutOfRangeException(nameof(iterations));

        T[] array = sequence.ToArray();
        array.ShuffleInplace(iterations);
        return array;
    }

    public static IEnumerable<T> SkipEvery<T>(this IEnumerable<T> sequence, int skipCount)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (skipCount <= 1)
            throw new ArgumentOutOfRangeException(nameof(skipCount), "Skip count should be 2 or greater.");
        return SkipEveryIterator(sequence, skipCount);
    }

    private static IEnumerable<T> SkipEveryIterator<T>(IEnumerable<T> sequence, int skipCount)
    {
        int counter = skipCount;
        foreach (T item in sequence)
        {
            if (counter == skipCount)
            {
                yield return item;
                counter = 1;
            }
            else
                counter++;
        }
    }

    public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> sequence,
        Func<TInput, TOutput> converter)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (converter is null)
            throw new ArgumentNullException(nameof(converter));

        return sequence.Select(converter).ToArray();
    }

    public static T[] ToArray<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return sequence.Where(predicate).ToArray();
    }

    /// <summary>
    ///     Converts selected elements of the specified sequence to an array of another type.
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TOutput">The type of elements in the target array.</typeparam>
    /// <param name="sequence">The source sequence.</param>
    /// <param name="predicate">
    ///     A delegate that controls the elements that are included in the target array.
    /// </param>
    /// <param name="converter">
    ///     A delegate that converts elements from the source sequence type to the target array
    ///     element type.
    /// </param>
    /// <returns>An array of the target element type.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the source sequence, <c>predicate</c> delegate or <c>converter</c> delegate are null.
    /// </exception>
    public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> sequence, Func<TInput, bool> predicate,
        Func<TInput, TOutput> converter)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));
        if (converter is null)
            throw new ArgumentNullException(nameof(converter));

        return sequence.Where(predicate).Select(converter).ToArray();
    }

    public static List<TOutput> ToList<TInput, TOutput>(this IEnumerable<TInput> sequence,
        Func<TInput, TOutput> converter)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (converter is null)
            throw new ArgumentNullException(nameof(converter));

        return sequence.Select(converter).ToList();
    }

    public static List<T> ToList<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return sequence.Where(predicate).ToList();
    }

    public static List<TOutput> ToList<TInput, TOutput>(this IEnumerable<TInput> sequence,
        Func<TInput, bool> predicate, Func<TInput, TOutput> converter)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));
        if (converter is null)
            throw new ArgumentNullException(nameof(converter));

        return sequence.Where(predicate).Select(converter).ToList();
    }

    public static IEnumerable<T> WhereAny<T, TOther>(this IEnumerable<T> sequence, IEnumerable<TOther> other,
        Func<T, TOther, bool> predicate)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return sequence.Where(element => other.Any(otherElement => predicate(element, otherElement)));
    }

    public static IEnumerable<T> WhereAny<T>(this IEnumerable<T> sequence, IEnumerable<T> other,
        Func<T, T, bool>? predicate = null)
    {
        // TODO: Check if T implements any of the equality interfaces and if so, try comparing
        // with those.
        predicate ??= (item, otherItem) => ReferenceEquals(item, otherItem);
        return sequence.WhereAny<T, T>(other, predicate);
    }

    public static IEnumerable<string> WhereAny(this IEnumerable<string> sequence, IEnumerable<string> other,
        StringComparison comparison = default)
    {
        return sequence.WhereAny<string>(other, (item, otherItem) => string.Equals(item, otherItem, comparison));
    }

    public static IEnumerable<T> WhereNotAny<T, TOther>(this IEnumerable<T> sequence, IEnumerable<TOther> other,
        Func<T, TOther, bool> predicate)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return sequence.Where(element => !other.Any(otherElement => predicate(element, otherElement)));
    }

    public static IEnumerable<T> WhereNotAny<T>(this IEnumerable<T> sequence, IEnumerable<T> other,
        Func<T, T, bool>? predicate = null)
    {
        // TODO: Check if T implements any of the equality interfaces and if so, try comparing
        // with those.
        predicate ??= (item, otherItem) => ReferenceEquals(item, otherItem);
        return sequence.WhereNotAny<T, T>(other, predicate);
    }

    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return sequence.Where(element => !predicate(element));
    }

    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> sequence, Func<T, int, bool> predicate)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return sequence.Where((element, i) => !predicate(element, i));
    }

    public static IEnumerable<T> Union<T>(this IEnumerable<T> sequence, T item)
    {
        return sequence.Union(new[] {item});
    }

    public static IEnumerable<T> Union<T>(this IEnumerable<T> sequence, T item, IEqualityComparer<T> comparer)
    {
        return sequence.Union(new[] {item}, comparer);
    }

    public static IEnumerable<T> Union<T>(this IEnumerable<T> sequence, params T[] items)
    {
        return sequence.Union((IEnumerable<T>)items);
    }
}
