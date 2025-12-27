// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.LongCollectionExtensions;

public sealed class SplitBySequenceTests
{
    [Theory, LongArray(CollectionType.Null)]
    public void Throws_if_longs_is_null(long[] longs)
    {
        Should.Throw<ArgumentNullException>(() => longs.SplitBySequence(1, 2));
    }

    [Theory, LongArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(long[] longs)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => longs.SplitBySequence(-1, 100, 1, 2));
    }

    [Theory, LongArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(long[] longs)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => longs.SplitBySequence(0, -1, 1, 2));
    }

    [Theory, LongArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null_or_empty(long[] longs)
    {
        Should.Throw<ArgumentNullException>(() => longs.SplitBySequence(sequence: null!));
        Should.Throw<ArgumentException>(() => longs.SplitBySequence());
    }

    [Theory, MemberData(nameof(Splits_longs_on_existing_sequence_Data))]
    public void Splits_longs_on_existing_sequence(long[] longs, long[] sequence, long[][] expectedResult)
    {
        long[][] result = longs.SplitBySequence(sequence);

        result.ShouldBe(expectedResult);
    }

    public static IEnumerable<object[]> Splits_longs_on_existing_sequence_Data()
    {
        yield return new object[]
        {
            new long[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new long[] {4, 1},
            new long[][]
            {
                [1, 2, 3],
                [2, 3],
                [2, 3, 4],
            }
        };

        yield return new object[]
        {
            new long[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new long[] {4},
            new long[][]
            {
                [1, 2, 3],
                [1, 2, 3],
                [1, 2, 3],
                [], 
            }
        };

        yield return new object[]
        {
            new long[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new long[] {1},
            new long[][]
            {
                [], 
                [2, 3, 4],
                [2, 3, 4],
                [2, 3, 4],
            }
        };
    }

    [Theory]
    [InlineData(new long[] {1, 2, 3, 4, 1, 2, 3, 4}, new long[] {9})]
    [InlineData(new long[] {1, 2, 3, 4, 1, 2, 3, 4}, new long[] {8, 9})]
    public void Splits_longs_on_non_existing_sequence(long[] longs, long[] sequence)
    {
        long[][] result = longs.SplitBySequence(sequence);

        result.Length.ShouldBe(1);
        result[0].ShouldBeSameAs(longs);
        result.ShouldBe([longs]);
    }

    [Fact]
    public void Does_not_error_if_count_is_too_large()
    {
        long[] longs = [1, 2, 3, 4, 1, 2, 3, 4];
        long[] sequence = [3];

        Should.NotThrow(() => longs.SplitBySequence(0, 100, sequence));
    }

    //TODO: Add tests for non-standard start and count values.
}
