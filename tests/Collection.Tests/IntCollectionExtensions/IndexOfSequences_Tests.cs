// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions
{
    public sealed class IndexOfSequences_Tests
    {
        [Theory, IntArray(CollectionType.Null)]
        public void Throws_if_ints_are_null(IList<int> ints)
        {
            Should.Throw<ArgumentNullException>(() => ints.IndexOfSequences(1));
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Throws_if_start_is_negative(IList<int> ints)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => ints.IndexOfSequences(-1, 10, 1));
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Throws_if_count_is_negative(IList<int> ints)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => ints.IndexOfSequences(0, -1, 1));
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Throws_if_sequence_is_null(IList<int> ints)
        {
            Should.Throw<ArgumentNullException>(() => ints.IndexOfSequences(0, 100, null));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new int[] { 2, 3 }, new[] { 1, 4, 6 })]
        [InlineData(new int[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new int[] { 2, 3, 1 }, new[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new int[] { 1 }, new[] { 0, 3 })]
        public void Returns_indices_of_existing_sequence(IList<int> ints, int[] sequence, int[] expectedIndices)
        {
            ints.IndexOfSequences(sequence).ShouldBe(expectedIndices);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new int[] { 3, 2, 1 })]
        [InlineData(new int[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new int[] { 2, 3, 3 })]
        [InlineData(new int[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new int[] { 9 })]
        public void Returns_minus_one_if_sequence_not_found(IList<int> ints, int[] sequence)
        {
            ints.IndexOfSequences(sequence).ShouldBeEmpty();
        }
    }
}
