// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.ShortCollectionExtensions;

public sealed class SplitBySequenceTests
{
    [Theory, ShortArray(CollectionType.Null)]
    public void Throws_if_shorts_is_null(short[] shorts)
    {
        Should.Throw<ArgumentNullException>(() => shorts.SplitBySequence(1, 2));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(short[] shorts)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => shorts.SplitBySequence<short>(-1, 100, 1, 2));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(short[] shorts)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => shorts.SplitBySequence<short>(0, -1, 1, 2));
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null_or_empty(short[] shorts)
    {
        Should.Throw<ArgumentNullException>(() => shorts.SplitBySequence(sequence: null!));
        Should.Throw<ArgumentException>(() => shorts.SplitBySequence());
    }

    [Theory, MemberData(nameof(Splits_shorts_on_existing_sequence_Data))]
    public void Splits_shorts_on_existing_sequence(short[] shorts, short[] sequence, short[][] expectedResult)
    {
        short[][] result = shorts.SplitBySequence(sequence);

        result.ShouldBe(expectedResult);
    }

    public static IEnumerable<object[]> Splits_shorts_on_existing_sequence_Data()
    {
        yield return new object[]
        {
            new short[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new short[] {4, 1},
            new short[][]
            {
                [1, 2, 3],
                [2, 3],
                [2, 3, 4],
            }
        };

        yield return new object[]
        {
            new short[] { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4 },
            new short[] { 4 },
            new short[][]
            {
                [1, 2, 3],
                [1, 2, 3],
                [1, 2, 3],
                Array.Empty<short>(),
            }
        };

        yield return new object[]
        {
            new short[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new short[] {1},
            new short[][]
            {
                Array.Empty<short>(),
                [2, 3, 4],
                [2, 3, 4],
                [2, 3, 4],
            }
        };
    }

    [Theory]
    [InlineData(new short[] { 1, 2, 3, 4, 1, 2, 3, 4 }, new short[] { 9 })]
    [InlineData(new short[] { 1, 2, 3, 4, 1, 2, 3, 4 }, new short[] { 8, 9 })]
    public void Splits_shorts_on_non_existing_sequence(short[] shorts, short[] sequence)
    {
        short[][] result = shorts.SplitBySequence(sequence);

        result.Length.ShouldBe(1);
        result[0].ShouldBeSameAs(shorts);
        result.ShouldBe([shorts]);
    }

    [Fact]
    public void Does_not_error_if_count_is_too_large()
    {
        short[] shorts = [1, 2, 3, 4, 1, 2, 3, 4];
        short[] sequence = [3];

        Should.NotThrow(() => shorts.SplitBySequence(0, 100, sequence));
    }

    //TODO: Add tests for non-standard start and count values.
}
