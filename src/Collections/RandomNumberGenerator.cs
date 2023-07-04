// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.Security.Cryptography;

#if EXPLICIT
namespace Collections.Net;
#else
// ReSharper disable once CheckNamespace
namespace System.Collections.Generic;
#endif

/// <summary>
///     Random number generator used internally for any code that requires random numbers;
/// </summary>
internal sealed class Rng
{
#if NETSTANDARD2_0
    private readonly RNGCryptoServiceProvider _generator = new();
#endif
    private readonly byte[] _buffer = new byte[sizeof(int)];

    /// <summary>
    ///     Exclusive max value of the generated random number.
    /// </summary>
    private readonly int _max;

    /// <summary>
    ///     Initializes an instance of the <see cref="Rng"/> class with the exclusive <paramref name="max"/> value
    ///     to generate.
    /// </summary>
    /// <param name="max">Exclusive max value of the generated random number.</param>
    internal Rng(int max)
    {
        _max = max;
    }

    /// <summary>
    ///     Generates a random number from 0 to <see cref="_max"/>
    /// </summary>
    /// <returns>The random number.</returns>
    internal int Next()
    {
#if NETSTANDARD2_0
        _generator.GetBytes(_buffer);
#else
        RandomNumberGenerator.Fill(_buffer);
#endif
        return Math.Abs(BitConverter.ToInt32(_buffer, 0) % _max);
    }
}
