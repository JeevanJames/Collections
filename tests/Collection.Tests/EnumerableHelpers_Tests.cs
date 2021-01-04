#region --- License & Copyright Notice ---
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

using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;
using Xunit.Abstractions;

namespace Collection.Tests
{
    public sealed class EnumerableHelpers_Tests
    {
        private readonly ITestOutputHelper _output;

        public EnumerableHelpers_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(300, byte.MinValue, byte.MaxValue)]
        [InlineData(50, byte.MinValue, 9)]
        [InlineData(40, 201, byte.MaxValue)]
        public void Returns_random_bytes(int count, byte min, byte max)
        {
            List<byte> bytes = EnumerableHelpers.CreateRandomBytes(count, min, max).ToList();

            bytes.ShouldNotBeNull();
            bytes.Count.ShouldBe(count);
            bytes.ShouldAllBe(b => b >= min && b <= max);

            _output.WriteLine(bytes.ToString(", "));
        }

        [Theory]
        [InlineData(300, int.MinValue, int.MaxValue)]
        [InlineData(50, int.MinValue, 9)]
        [InlineData(40, 201, int.MaxValue)]
        [InlineData(40, -3, 4)]
        public void Returns_random_ints(int count, int min, int max)
        {
            List<int> bytes = EnumerableHelpers.CreateRandomInts(count, min, max).ToList();

            bytes.ShouldNotBeNull();
            bytes.Count.ShouldBe(count);
            bytes.ShouldAllBe(i => i >= min && i <= max);

            _output.WriteLine(bytes.ToString(", "));
        }
    }
}
