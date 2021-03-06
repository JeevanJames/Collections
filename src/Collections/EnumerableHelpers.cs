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

namespace Collections.Net.Enumerable
#else
// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
#endif
{
    public static class EnumerableHelpers
    {
        public static IEnumerable<T> Create<T>(int count, T value)
        {
            if (count <= 0)
                yield break;
            for (int i = 0; i < count; i++)
                yield return value;
        }

        public static IEnumerable<T> Create<T>(int count, Func<int, T> valueFactory)
        {
            if (valueFactory is null)
                throw new ArgumentNullException(nameof(valueFactory));
            if (count <= 0)
                yield break;
            for (int i = 0; i < count; i++)
                yield return valueFactory(i);
        }

        private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        public static IEnumerable<byte> CreateRandomBytes(int count, byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            byte[] buffer = new byte[sizeof(byte)];
            int zeroBasedInclusiveMax = (int)max - (int)min + 1;

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
}
