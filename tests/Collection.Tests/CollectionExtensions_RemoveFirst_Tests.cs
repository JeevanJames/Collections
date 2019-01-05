﻿using System;
using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_RemoveFirst_Tests
    {
        [Fact]
        public void Throws_if_collection_is_null()
        {
            IList<int> collection = null;

            Should.Throw<ArgumentNullException>(() => collection.RemoveFirst(n => n % 2 == 0));
        }

        [Fact]
        public void Throws_if_predicate_is_null()
        {
            IList<int> collection = new[] {1, 2};
            Should.Throw<ArgumentNullException>(() => collection.RemoveFirst(null));
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new [] {1, 3, 5, 7, 9})]
        public void Returns_false_if_matching_element_not_found(IList<int> collection)
        {
            collection.RemoveFirst(n => n % 2 == 0).ShouldBeFalse();
        }

        [Fact]
        public void Removes_first_matching_element()
        {
            IList<int> collection = new List<int> {1, 2, 3, 4, 5, 6};

            bool removed = collection.RemoveFirst(n => n % 2 == 0);

            removed.ShouldBeTrue();
            collection.Count.ShouldBe(5);
            collection.ShouldContain(n => n == 2, 0);
        }
    }
}