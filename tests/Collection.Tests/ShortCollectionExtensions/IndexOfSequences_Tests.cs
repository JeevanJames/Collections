// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ShortCollectionExtensions;

public sealed class IndexOfSequencesTests
{
    [Theory, ShortArray(CollectionType.Null)]
    public void Throws_if_shorts_are_null(IList<short> shorts)
    {
        Should.Throw<ArgumentNullException>(() => shorts.IndexOfSequences<short>(1));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(IList<short> shorts)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => shorts.IndexOfSequences<short>(-1, 10, 1));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(IList<short> shorts)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => shorts.IndexOfSequences<short>(0, -1, 1));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null(IList<short> shorts)
    {
        Should.Throw<ArgumentNullException>(() => shorts.IndexOfSequences(0, 100, sequence: null!));
    }

    [Theory]
    [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 2, 3 }, new[] { 1, 4, 6 })]
    [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 2, 3, 1 }, new[] { 1 })]
    [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 1 }, new[] { 0, 3 })]
    public void Returns_indices_of_existing_sequence(IList<short> shorts, short[] sequence, int[] expectedIndices)
    {
        shorts.IndexOfSequences(sequence).ShouldBe(expectedIndices);
    }

    [Theory]
    [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 3, 2, 1 })]
    [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 2, 3, 3 })]
    [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 9 })]
    public void Returns_minus_one_if_sequence_not_found(IList<short> shorts, short[] sequence)
    {
        shorts.IndexOfSequences(sequence).ShouldBeEmpty();
    }
}
