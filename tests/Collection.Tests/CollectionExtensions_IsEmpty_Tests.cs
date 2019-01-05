using System;
using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_IsEmpty_Tests
    {
        [Fact]
        public void Throws_if_collection_is_null()
        {
            IEnumerable<int> collection = null;
            Should.Throw<ArgumentNullException>(() => collection.IsEmpty());
        }

        [Fact]
        public void Returns_true_if_collection_is_empty()
        {
            IEnumerable<int> collection = new int[0];
            collection.IsEmpty().ShouldBeTrue();
        }

        [Theory, MemberData(nameof(NotEmptyCollections))]
        public void Returns_false_if_collection_is_not_empty(IEnumerable<int> collection)
        {
            collection.IsEmpty().ShouldBeFalse();
        }

        public static IEnumerable<object[]> NotEmptyCollections()
        {
            yield return new object[] {Enumerable.Range(1, 5)};
            yield return new object[] {new[] {1, 2, 3, 4, 5}};
        }
    }
}