// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ByteCollectionExtensions;

public sealed class IndexOfSequenceTests
{
    [Theory, ByteArray(CollectionType.Null)]
    public void Throws_if_bytes_are_null(IList<byte> bytes)
    {
        Should.Throw<ArgumentNullException>(() => bytes.IndexOfSequence(1));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(IList<byte> bytes)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => bytes.IndexOfSequence(-1, 10, 1));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(IList<byte> bytes)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => bytes.IndexOfSequence(0, -1, 1));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null(IList<byte> bytes)
    {
        Should.Throw<ArgumentNullException>(() => bytes.IndexOfSequence(0, 100, null!));
    }

    [Theory]
    [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {1, 2}, 0)]
    [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {4, 5}, 3)]
    [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {6}, 5)]
    public void Returns_index_of_existing_sequence(IList<byte> bytes, byte[] sequence, int expectedIndex)
    {
        bytes.IndexOfSequence(sequence).ShouldBe(expectedIndex);
    }

    [Theory]
    [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {2, 1})]
    [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {9})]
    [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {1, 2, 4})]
    public void Returns_minus_one_if_sequence_not_found(IList<byte> bytes, byte[] sequence)
    {
        bytes.IndexOfSequence(sequence).ShouldBeLessThan(0);
    }

    [Theory]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6 }, new byte[] { 2, 3 }, 1, 5, 1)]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6 }, new byte[] { 4, 5 }, 2, 9, 3)]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6 }, new byte[] { 6 }, 1, 100, 5)]
    public void Returns_index_of_existing_sequence_for_start_and_count(IList<byte> bytes, byte[] sequence,
        int start, int count, int expectedIndex)
    {
        bytes.IndexOfSequence(start, count, sequence).ShouldBe(expectedIndex);
    }

    [Theory]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6 }, new byte[] { 3, 2 }, 1, 5)]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6 }, new byte[] { 1, 2 }, 2, 9)]
    [InlineData(new byte[] { 1, 2, 3, 4, 5, 6 }, new byte[] { 7 }, 1, 100)]
    public void Returns_minus_one_for_start_and_count_if_sequence_not_found(IList<byte> bytes, byte[] sequence,
        int start, int count)
    {
        bytes.IndexOfSequence(start, count, sequence).ShouldBeLessThan(0);
    }
}
