// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions
{
    public sealed class IsEqualTo_Tests
    {
        [Theory]
        [InlineData(new int[0])]
        [InlineData(new int[] {1, 2, 3})]
        public void Returns_true_for_the_same_reference(IList<int> ints)
        {
            ints.IsEqualTo(ints).ShouldBeTrue();
        }

        [Theory]
        [InlineData(new int[0], null)]
        [InlineData(new int[] {1, 2}, null)]
        [InlineData(null, new int[0])]
        [InlineData(null, new int[] {1, 2})]
        public void Returns_false_if_any_collection_is_null(IList<int> ints1, IList<int> ints2)
        {
            ints1.IsEqualTo(ints2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new int[0], new int[] {1, 2})]
        [InlineData(new int[] {1, 2}, new int[0])]
        public void Returns_false_for_collections_of_different_lengths(IList<int> ints1, IList<int> ints2)
        {
            ints1.IsEqualTo(ints2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new int[] {2, 1}, new int[] {1, 2})]
        [InlineData(new int[] {1, 2, 3, 4}, new int[] {1, 2, 4, 3})]
        public void Returns_false_for_collections_of_different_content(IList<int> ints1, IList<int> ints2)
        {
            ints1.IsEqualTo(ints2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new int[0], new int[0])]
        [InlineData(new int[] {1, 2}, new int[] {1, 2})]
        [InlineData(new int[] {1, 2, 3, 4}, new int[] {1, 2, 3, 4})]
        public void Returns_true_for_collections_of_same_content(IList<int> ints1, IList<int> ints2)
        {
            ints1.IsEqualTo(ints2).ShouldBeTrue();
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Returns_true_for_params_array(IList<int> ints)
        {
            ints.IsEqualTo(1, 2, 3, 4, 5, 6).ShouldBeTrue();
        }
    }
}
