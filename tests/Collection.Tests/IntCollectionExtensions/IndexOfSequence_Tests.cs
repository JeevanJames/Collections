// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions;

public sealed class IndexOfSequenceTests
{
    [Theory, IntArray(CollectionType.Null)]
    public void Throws_if_ints_are_null(IList<int> ints)
    {
        Should.Throw<ArgumentNullException>(() => ints.IndexOfSequence(1));
    }

    [Theory, IntArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(IList<int> ints)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ints.IndexOfSequence(-1, 10, 1));
    }

    [Theory, IntArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(IList<int> ints)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ints.IndexOfSequence(0, -1, 1));
    }

    [Theory, IntArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null(IList<int> ints)
    {
        Should.Throw<ArgumentNullException>(() => ints.IndexOfSequence(0, 100, null!));
    }

    [Theory]
    [InlineData(new[] {1, 2, 3, 4, 5, 6}, new[] {1, 2}, 0)]
    [InlineData(new[] {1, 2, 3, 4, 5, 6}, new[] {4, 5}, 3)]
    [InlineData(new[] {1, 2, 3, 4, 5, 6}, new[] {6}, 5)]
    public void Returns_index_of_existing_sequence(IList<int> ints, int[] sequence, int expectedIndex)
    {
        ints.IndexOfSequence(sequence).ShouldBe(expectedIndex);
    }

    [Theory]
    [InlineData(new[] {1, 2, 3, 4, 5, 6}, new[] {2, 1})]
    [InlineData(new[] {1, 2, 3, 4, 5, 6}, new[] {9})]
    [InlineData(new[] {1, 2, 3, 4, 5, 6}, new[] {1, 2, 4})]
    public void Returns_minus_one_if_sequence_not_found(IList<int> ints, int[] sequence)
    {
        ints.IndexOfSequence(sequence).ShouldBeLessThan(0);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 2, 3 }, 1, 5, 1)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 5 }, 2, 9, 3)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 6 }, 1, 100, 5)]
    public void Returns_index_of_existing_sequence_for_start_and_count(IList<int> ints, int[] sequence,
        int start, int count, int expectedIndex)
    {
        ints.IndexOfSequence(start, count, sequence).ShouldBe(expectedIndex);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 2 }, 1, 5)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2 }, 2, 9)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 7 }, 1, 100)]
    public void Returns_minus_one_for_start_and_count_if_sequence_not_found(IList<int> ints, int[] sequence,
        int start, int count)
    {
        ints.IndexOfSequence(start, count, sequence).ShouldBeLessThan(0);
    }
}
