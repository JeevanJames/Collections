// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ByteCollectionExtensions;

public sealed class SplitBySequenceTests
{
    [Theory, ByteArray(CollectionType.Null)]
    public void Throws_if_bytes_is_null(byte[] bytes)
    {
        Should.Throw<ArgumentNullException>(() => bytes.SplitBySequence(1, 2));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(byte[] bytes)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => bytes.SplitBySequence(-1, 100, 1, 2));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(byte[] bytes)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => bytes.SplitBySequence(0, -1, 1, 2));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null_or_empty(byte[] bytes)
    {
        Should.Throw<ArgumentNullException>(() => bytes.SplitBySequence(sequence: null!));
        Should.Throw<ArgumentException>(() => bytes.SplitBySequence());
    }

    [Theory, MemberData(nameof(Splits_bytes_on_existing_sequence_Data))]
    public void Splits_bytes_on_existing_sequence(byte[] bytes, byte[] sequence, byte[][] expectedResult)
    {
        byte[][] result = bytes.SplitBySequence(sequence);

        result.ShouldBe(expectedResult);
    }

    public static IEnumerable<object[]> Splits_bytes_on_existing_sequence_Data()
    {
        yield return new object[]
        {
            new byte[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new byte[] {4, 1},
            new byte[][]
            {
                new byte[] {1, 2, 3},
                new byte[] {2, 3},
                new byte[] {2, 3, 4},
            }
        };

        yield return new object[]
        {
            new byte[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new byte[] {4},
            new byte[][]
            {
                new byte[] {1, 2, 3},
                new byte[] {1, 2, 3},
                new byte[] {1, 2, 3},
                Array.Empty<byte>(), 
            }
        };

        yield return new object[]
        {
            new byte[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
            new byte[] {1},
            new byte[][]
            {
                Array.Empty<byte>(), 
                new byte[] {2, 3, 4},
                new byte[] {2, 3, 4},
                new byte[] {2, 3, 4},
            }
        };
    }

    [Theory]
    [InlineData(new byte[] {1, 2, 3, 4, 1, 2, 3, 4}, new byte[] {9})]
    [InlineData(new byte[] {1, 2, 3, 4, 1, 2, 3, 4}, new byte[] {8, 9})]
    public void Splits_bytes_on_non_existing_sequence(byte[] bytes, byte[] sequence)
    {
        byte[][] result = bytes.SplitBySequence(sequence);

        result.Length.ShouldBe(1);
        result[0].ShouldBeSameAs(bytes);
        result.ShouldBe(new byte[][] { bytes });
    }

    [Fact]
    public void Does_not_error_if_count_is_too_large()
    {
        byte[] bytes = {1, 2, 3, 4, 1, 2, 3, 4};
        byte[] sequence = {3};

        Should.NotThrow(() => bytes.SplitBySequence(0, 100, sequence));
    }

    //TODO: Add tests for non-standard start and count values.
}
