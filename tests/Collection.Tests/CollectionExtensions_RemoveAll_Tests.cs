using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_RemoveAll_Tests
    {
        [Fact]
        public void Throws_if_collection_is_null()
        {
            IList<int> collection = null;

            Should.Throw<ArgumentNullException>(() => collection.RemoveAll(n => n % 2 == 0));
        }

        [Fact]
        public void Throws_if_predicate_is_null()
        {
            IList<int> collection = new List<int> {1, 2, 3, 4, 5, 6};

            Should.Throw<ArgumentNullException>(() => collection.RemoveAll(null));
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new[] {1, 3, 5, 7, 9})]
        public void Returns_zero_if_no_matching_elements_found(IList<int> collection)
        {
            collection.RemoveAll(n => n % 2 == 0).ShouldBe(0);
        }

        [Fact]
        public void Removes_all_matching_elements()
        {
            IList<int> collection = new List<int> {1, 2, 3, 4, 5, 6};

            int removedCount = collection.RemoveAll(n => n % 2 == 0);

            removedCount.ShouldBe(3);
            collection.Count.ShouldBe(3);
            collection.ShouldAllBe(n => n % 2 != 0);
        }
    }
}