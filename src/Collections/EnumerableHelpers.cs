// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.Security.Cryptography;
// ReSharper disable CheckNamespace

#if EXPLICIT
namespace Collections.Net.EnumerableEx;
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

    private static readonly RNGCryptoServiceProvider _rng = new();

    /// <summary>
    ///     Creates an <see cref="IEnumerable{Byte}"/> with <paramref name="count"/> number of elements,
    ///     each initialized with a random value between <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="count">The number of items in the <see cref="IEnumerable{T}"/>.</param>
    /// <param name="min"></param>
    /// <param name="max"></param>
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
            _rng.GetBytes(buffer);
            int randomValue = (buffer[0] % zeroBasedInclusiveMax) + min;
            yield return (byte)randomValue;
        }
    }

    public static IEnumerable<int> CreateRandomInts(int count, int min = int.MinValue, int max = int.MaxValue)
    {
        byte[] buffer = new byte[sizeof(int)];
        long zeroBasedInclusiveMax = (long)max - (long)min + 1;

        for (int i = 0; i < count; i++)
        {
            _rng.GetBytes(buffer);
            long randomValue = (Math.Abs(BitConverter.ToInt32(buffer, 0)) % zeroBasedInclusiveMax) + min;
            yield return (int)randomValue;
        }
    }
}
