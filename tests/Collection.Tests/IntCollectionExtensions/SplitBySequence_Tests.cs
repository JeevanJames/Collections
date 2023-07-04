// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions;

public sealed class SplitBySequenceTests
{
    [Theory, IntArray(CollectionType.Null)]
    public void Throws_if_ints_is_null(int[] ints)
    {
        Should.Throw<ArgumentNullException>(() => ints.SplitBySequence(1, 2));
    }

    [Theory, IntArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(int[] ints)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ints.SplitBySequence(-1, 100, 1, 2));
    }

    [Theory, IntArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(int[] ints)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ints.SplitBySequence(0, -1, 1, 2));
    }

    [Theory, IntArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null_or_empty(int[] ints)
    {
        Should.Throw<ArgumentNullException>(() => ints.SplitBySequence(sequence: null!));
        Should.Throw<ArgumentException>(() => ints.SplitBySequence());
    }

    [Theory, MemberData(nameof(Splits_ints_on_existing_sequence_Data))]
    public void Splits_ints_on_existing_sequence(int[] ints, int[] sequence, int[][] expectedResult)
    {
        int[][] result = ints.SplitBySequence(sequence);

        result.ShouldBe(expectedResult);
    }

    public static IEnumerable<object[]> Splits_ints_on_existing_sequence_Data()
    {
        yield return new object[]
        {
            new int[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new int[] {4, 1},
            new int[][]
            {
                new [] {1, 2, 3},
                new [] {2, 3},
                new [] {2, 3, 4},
            }
        };

        yield return new object[]
        {
            new int[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new int[] {4},
            new int[][]
            {
                new [] {1, 2, 3},
                new [] {1, 2, 3},
                new [] {1, 2, 3},
                Array.Empty<int>(), 
            }
        };

        yield return new object[]
        {
            new int[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new int[] {1},
            new int[][]
            {
                Array.Empty<int>(), 
                new [] {2, 3, 4},
                new [] {2, 3, 4},
                new [] {2, 3, 4},
            }
        };
    }

    [Theory]
    [InlineData(new [] {1, 2, 3, 4, 1, 2, 3, 4}, new [] {9})]
    [InlineData(new [] {1, 2, 3, 4, 1, 2, 3, 4}, new [] {8, 9})]
    public void Splits_ints_on_non_existing_sequence(int[] ints, int[] sequence)
    {
        int[][] result = ints.SplitBySequence(sequence);

        result.Length.ShouldBe(1);
        result[0].ShouldBeSameAs(ints);
        result.ShouldBe(new int[][] {ints});
    }

    [Fact]
    public void Does_not_error_if_count_is_too_large()
    {
        int[] ints = {1, 2, 3, 4, 1, 2, 3, 4};
        int[] sequence = {3};

        Should.NotThrow(() => ints.SplitBySequence(0, 100, sequence));
    }

    //TODO: Add tests for non-standard start and count values.
}
