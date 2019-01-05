﻿using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_IsNullOrEmpty_Tests
    {
        [Theory]
        [InlineData((IEnumerable<int>)null)]
        [InlineData(new int[0])]
        public void Returns_true_if_collection_is_null_or_empty(IEnumerable<int> collection)
        {
            collection.IsNullOrEmpty().ShouldBeTrue();
        }

        [Theory, MemberData(nameof(NotEmptyCollections))]
        public void Returns_false_if_collection_is_not_empty(IEnumerable<int> collection)
        {
            collection.IsNullOrEmpty().ShouldBeFalse();
        }

        public static IEnumerable<object[]> NotEmptyCollections()
        {
            yield return new object[] {Enumerable.Range(1, 5)};
            yield return new object[] {new[] {1, 2, 3, 4, 5}};
        }
    }
}