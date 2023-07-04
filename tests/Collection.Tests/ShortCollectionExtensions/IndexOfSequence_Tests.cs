// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ShortCollectionExtensions;

public sealed class IndexOfSequenceTests
{
    [Theory, ShortArray(CollectionType.Null)]
    public void Throws_if_shorts_are_null(IList<short> shorts)
    {
        Should.Throw<ArgumentNullException>(() => shorts.IndexOfSequence(1));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(IList<short> shorts)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => shorts.IndexOfSequence(-1, 10, 1));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(IList<short> shorts)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => shorts.IndexOfSequence(0, -1, 1));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null(IList<short> shorts)
    {
        Should.Throw<ArgumentNullException>(() => shorts.IndexOfSequence(0, 100, sequence: null!));
    }

    [Theory]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {1, 2}, 0)]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {4, 5}, 3)]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {6}, 5)]
    public void Returns_index_of_existing_sequence(IList<short> shorts, short[] sequence, int expectedIndex)
    {
        shorts.IndexOfSequence(sequence).ShouldBe(expectedIndex);
    }

    [Theory]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {2, 1})]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {9})]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {1, 2, 4})]
    public void Returns_minus_one_if_sequence_not_found(IList<short> shorts, short[] sequence)
    {
        shorts.IndexOfSequence(sequence).ShouldBeLessThan(0);
    }

    [Theory]
    [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 2, 3 }, 1, 5, 1)]
    [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 4, 5 }, 2, 9, 3)]
    [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 6 }, 1, 100, 5)]
    public void Returns_index_of_existing_sequence_for_start_and_count(IList<short> shorts, short[] sequence, int start, int count, int expectedIndex)
    {
        shorts.IndexOfSequence(start, count, sequence).ShouldBe(expectedIndex);
    }

    [Theory]
    [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 3, 2 }, 1, 5)]
    [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 1, 2 }, 2, 9)]
    [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 7 }, 1, 100)]
    public void Returns_minus_one_for_start_and_count_if_sequence_not_found(IList<short> shorts, short[] sequence, int start, int count)
    {
        shorts.IndexOfSequence(start, count, sequence).ShouldBeLessThan(0);
    }
}
