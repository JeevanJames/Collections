﻿#region --- License & Copyright Notice ---
/*
Custom collections and collection extensions for .NET
Copyright (c) 2018-2020 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System.Security.Cryptography;

#if EXPLICIT
using System;
using System.Collections.Generic;

namespace Collections.Net
#else
namespace System.Collections.Generic
#endif
{
    /// <summary>
    ///     Random number generator.
    /// </summary>
    internal sealed class Rng
    {
        private readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
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
            _generator.GetBytes(_buffer);
            return Math.Abs(BitConverter.ToInt32(_buffer, 0) % _max);
        }
    }
}
