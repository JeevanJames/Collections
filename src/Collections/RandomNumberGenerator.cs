#region --- License & Copyright Notice ---
/*
Custom collections and collection extensions for .NET
Copyright (c) 2018-2019 Jeevan James
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

namespace System.Collections.Generic
{
    internal sealed class Rng
    {
#if NETSTANDARD1_3
        private readonly Random _generator = new Random((int)DateTime.Now.Ticks);
        private readonly int _count;

        internal Rng(int count)
        {
            _count = count;
        }

        internal int Next()
        {
            return _generator.Next(_count);
        }
#else
        private readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
        private readonly byte[] _buffer = new byte[sizeof(int)];
        private readonly int _count;

        internal Rng(int count)
        {
            _count = count;
        }

        internal int Next()
        {
            _generator.GetBytes(_buffer);
            return Math.Abs(BitConverter.ToInt32(_buffer, 0) % _count);
        }
#endif
    }
}