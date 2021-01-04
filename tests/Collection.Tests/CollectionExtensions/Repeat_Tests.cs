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

using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Enumerable;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class Repeat_Tests
    {
        [Fact]
        public void Throws_if_collection_is_null()
        {
            IEnumerable<int> collection = null;

            // Need to call ToList because the returned IEnumerable is lazy.
            Should.Throw<ArgumentNullException>(() => collection.Repeat(3).ToList());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Throws_if_count_is_negative_or_zero(int count)
        {
            IEnumerable<int> collection = new[] {1, 2, 3};

            // Need to call ToList because the returned IEnumerable is lazy.
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Repeat(count).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Creates_collection_with_repeated_items(int count)
        {
            IEnumerable<int> collection = new[] {5, 6, 7};

            IEnumerable<int> repeated = collection.Repeat(count);

            repeated.Count().ShouldBe(collection.Count() * count);
        }
    }
}
