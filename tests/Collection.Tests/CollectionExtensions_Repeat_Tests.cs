﻿using System;
using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_Repeat_Tests
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