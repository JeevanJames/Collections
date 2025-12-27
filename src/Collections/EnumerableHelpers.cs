// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

#if EXPLICIT
using System.Collections;
#endif

#if NET8_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.Security.Cryptography;

// ReSharper disable CheckNamespace
#if EXPLICIT
namespace Collections.Net;
#else
namespace System.Collections.Generic;
#endif

public static class EnumerableHelpers
{
    /// <summary>
    ///     Creates an <see cref="IEnumerable{T}"/> with <paramref name="count"/> number of items,
    ///     all initialized to the same <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The element type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <param name="count">The number of items in the <see cref="IEnumerable{T}"/>.</param>
    /// <param name="value">The value to assign to each element in the <see cref="IEnumerable{T}"/>.</param>
    /// <returns>
    ///     An <see cref="IEnumerable{T}"/> instance with <paramref name="count"/> elements, each with
    ///     the same <paramref name="value"/>.
    /// </returns>
    public static IEnumerable<T> Create<T>(int count, T value)
    {
        if (count <= 0)
            yield break;
        for (int i = 0; i < count; i++)
            yield return value;
    }

    /// <summary>
    ///     Creates an <see cref="IEnumerable{T}"/> with <paramref name="count"/> number of elements,
    ///     each initialized with a value from the <paramref name="valueFactory"/> delegate.
    /// </summary>
    /// <typeparam name="T">The element type of the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <param name="count">The number of items in the <see cref="IEnumerable{T}"/>.</param>
    /// <param name="valueFactory">
    ///     The delegate to generate the value to assign to each element in the <see cref="IEnumerable{T}"/>.
    /// </param>
    /// <returns>
    ///     An <see cref="IEnumerable{T}"/> instance with <paramref name="count"/> elements, with each
    ///     value assigned from the <paramref name="valueFactory"/> delegate.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="valueFactory"/> is <c>null</c>.</exception>
    public static IEnumerable<T> Create<T>(int count, Func<int, T> valueFactory)
    {
        if (valueFactory is null)
            throw new ArgumentNullException(nameof(valueFactory));
        if (count <= 0)
            yield break;
        for (int i = 0; i < count; i++)
            yield return valueFactory(i);
    }

#if NETSTANDARD2_0
    private static readonly RNGCryptoServiceProvider _rng = new();
#endif

    /// <summary>
    ///     Creates an <see cref="IEnumerable{Byte}"/> with <paramref name="count"/> number of elements,
    ///     each initialized with a random value between <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="count">The number of items in the <see cref="IEnumerable{Byte}"/>.</param>
    /// <param name="min">The minimum inclusive value of the random numbers to generate.</param>
    /// <param name="max">The maximum inclusive value of the random numbers to generate.</param>
    /// <returns>
    ///     An <see cref="IEnumerable{Byte}"/> instance with <paramref name="count"/> elements, with
    ///     each value being a random value.
    /// </returns>
    public static IEnumerable<byte> CreateRandomBytes(int count, byte min = byte.MinValue, byte max = byte.MaxValue)
    {
        byte[] buffer = new byte[sizeof(byte)];
        int zeroBasedInclusiveMax = max - min + 1;

        for (int i = 0; i < count; i++)
        {
#if NETSTANDARD2_0
            _rng.GetBytes(buffer);
#else
            RandomNumberGenerator.Fill(buffer);
#endif
            int randomValue = (buffer[0] % zeroBasedInclusiveMax) + min;
            yield return (byte)randomValue;
        }
    }

    /// <summary>
    ///     Creates an <see cref="IEnumerable{Int32}"/> with <paramref name="count"/> number of elements,
    ///     each initialized with a random value between <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="count">The number of items in the <see cref="IEnumerable{Int32}"/>.</param>
    /// <param name="min">The minimum inclusive value of the random numbers to generate.</param>
    /// <param name="max">The maximum inclusive value of the random numbers to generate.</param>
    /// <returns>
    ///     An <see cref="IEnumerable{Int32}"/> instance with <paramref name="count"/> elements, with
    ///     each value being a random value.
    /// </returns>
    public static IEnumerable<int> CreateRandomInts(int count, int min = int.MinValue, int max = int.MaxValue)
    {
        byte[] buffer = new byte[sizeof(int)];
        long zeroBasedInclusiveMax = max - min + 1;

        for (int i = 0; i < count; i++)
        {
#if NETSTANDARD2_0
            _rng.GetBytes(buffer);
#else
            RandomNumberGenerator.Fill(buffer);
#endif
            long randomValue = (Math.Abs(BitConverter.ToInt32(buffer, 0)) % zeroBasedInclusiveMax) + min;
            yield return (int)randomValue;
        }
    }

    public static IEnumerable<float> CreateRandomFloats(int count, float min = float.MinValue,
        float max = float.MaxValue)
    {
        byte[] buffer = new byte[sizeof(int)];
        double zeroBasedInclusiveMax = max - min + 1;

        for (int i = 0; i < count; i++)
        {
#if NETSTANDARD2_0
            _rng.GetBytes(buffer);
#else
            RandomNumberGenerator.Fill(buffer);
#endif
            double randomValue = (Math.Abs(BitConverter.ToSingle(buffer, 0)) % zeroBasedInclusiveMax) + min;
            yield return (float)randomValue;
        }
    }

    /// <summary>
    ///     Tries to allocate an <see cref="IList{T}"/> of the same size as the <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="T">The type of the list to allocate.</typeparam>
    /// <param name="source"></param>
    /// <returns>
    ///     An empty list with the initial size set to the size of the <paramref name="source"/> collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is <c>null</c>.</exception>
    public static IList<T> TryAllocateListOfSize<T>(IEnumerable<T> source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return source switch
        {
            ICollection<T> list => new List<T>(list.Count),
            IReadOnlyCollection<T> readOnlyList => new List<T>(readOnlyList.Count),
            _ => new List<T>()
        };
    }

    public static IList<TItem> TryAllocateListOfSize<TSource, TItem>(IEnumerable<TSource> source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return source switch
        {
            ICollection<TSource> list => new List<TItem>(list.Count),
            IReadOnlyCollection<TSource> readOnlyList => new List<TItem>(readOnlyList.Count),
            _ => new List<TItem>()
        };
    }

#if NETSTANDARD2_0
    public static bool TryAllocateCollectionOfSize<T>(IEnumerable<T> source,
        out IList<T>? collection,
        Func<int, IList<T>>? collectionFactory = null)
#else
    public static bool TryAllocateCollectionOfSize<T>(IEnumerable<T> source,
        [NotNullWhen(true)] out IList<T>? collection,
        Func<int, IList<T>>? collectionFactory = null)
#endif
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        switch (source)
        {
            case ICollection<T> list:
                collection = collectionFactory is not null ? collectionFactory(list.Count) : new List<T>(list.Count);
                return true;
            case IReadOnlyCollection<T> readOnlyList:
                collection = collectionFactory is not null
                    ? collectionFactory(readOnlyList.Count)
                    : new List<T>(readOnlyList.Count);
                return true;
            default:
                collection = null;
                return false;
        }
    }

#if NETSTANDARD2_0
    public static bool TryAllocateCollectionOfSize<TSource, TItem>(IEnumerable<TSource> source,
        out IList<TItem>? collection,
        Func<int, IList<TItem>>? collectionFactory = null)
#else
    public static bool TryAllocateCollectionOfSize<TSource, TItem>(IEnumerable<TSource> source,
        [NotNullWhen(true)] out IList<TItem>? collection,
        Func<int, IList<TItem>>? collectionFactory = null)
#endif
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        switch (source)
        {
            case ICollection<TSource> list:
                collection = collectionFactory is not null
                    ? collectionFactory(list.Count)
                    : new List<TItem>(list.Count);
                return true;
            case IReadOnlyCollection<TSource> readOnlyList:
                collection = collectionFactory is not null
                    ? collectionFactory(readOnlyList.Count)
                    : new List<TItem>(readOnlyList.Count);
                return true;
            default:
                collection = null;
                return false;
        }
    }

#if NETSTANDARD2_0
    public static bool TryAllocateCollectionOfSize<T, TCollection>(IEnumerable<T> source,
        Func<int, TCollection> collectionFactory,
        out TCollection? collection)
        where TCollection : IEnumerable<T>
#else
    public static bool TryAllocateCollectionOfSize<T, TCollection>(IEnumerable<T> source,
        Func<int, TCollection> collectionFactory,
        [NotNullWhen(true)] out TCollection? collection)
        where TCollection : IEnumerable<T>
#endif
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (collectionFactory is null)
            throw new ArgumentNullException(nameof(collectionFactory));

        switch (source)
        {
            case ICollection<T> list:
                collection = collectionFactory(list.Count);
                return true;
            case IReadOnlyCollection<T> readOnlyList:
                collection = collectionFactory(readOnlyList.Count);
                return true;
            default:
                collection = default;
                return false;
        }
    }

#if NETSTANDARD2_0
    public static bool TryAllocateCollectionOfSize<TSource, TItem, TCollection>(IEnumerable<TSource> source,
        Func<int, TCollection> collectionFactory,
        out TCollection? collection)
        where TCollection : IEnumerable<TItem>
#else
    public static bool TryAllocateCollectionOfSize<TSource, TItem, TCollection>(IEnumerable<TSource> source,
        Func<int, TCollection> collectionFactory,
        [NotNullWhen(true)] out TCollection? collection)
        where TCollection : IEnumerable<TItem>
#endif
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (collectionFactory is null)
            throw new ArgumentNullException(nameof(collectionFactory));

        switch (source)
        {
            case ICollection<TSource> list:
                collection = collectionFactory(list.Count);
                return true;
            case IReadOnlyCollection<TSource> readOnlyList:
                collection = collectionFactory(readOnlyList.Count);
                return true;
            default:
                collection = default;
                return false;
        }
    }

    /// <summary>
    ///     Generates a sequence of integers within a specified range.
    /// </summary>
    /// <param name="start">The value of the first number in the sequence.</param>
    /// <param name="end">
    ///     The maximum possible value to generate in the sequence. This number may or may not be generated
    ///     depending on the <paramref name="increment"/> value.
    /// </param>
    /// <param name="increment">The amount to increment between numbers in the sequence. Defaults to 1.</param>
    /// <returns>An <see cref="IEnumerable{Int32}"/> that contains the range of integers.</returns>
    public static IEnumerable<int> Range(int start, int end, int increment = 1)
    {
        if (increment == 0)
            throw new ArgumentException("Cannot have an increment of 0.", nameof(increment));
        if (end > start && increment < 0)
            throw new ArgumentException("Increment cannot be negative when the range is increasing.", nameof(increment));
        if (end < start && increment > 0)
            throw new ArgumentException("Increment cannot be positive when the range is decreasing.", nameof(increment));

        return start == end ? new[] { start } : new RangeEnumerator(start, end, increment);
    }

    private sealed class RangeEnumerator : IEnumerable<int>, IEnumerator<int>
    {
        private readonly int _start;
        private readonly int _end;
        private readonly int _increment;
        private long _current;

        internal RangeEnumerator(int start, int end, int increment)
        {
            _start = start;
            _end = end;
            _increment = increment;
            _current = long.MaxValue;
        }

        public IEnumerator<int> GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool MoveNext()
        {
            if (_current == long.MaxValue)
            {
                _current = _start;
                return true;
            }

            _current += _increment;
            return (_end <= _start || _current <= _end) && (_end >= _start || _current >= _end);
        }

        public void Reset()
        {
            _current = long.MaxValue;
        }

        public int Current => (int)_current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _current = long.MaxValue;
        }
    }
}
