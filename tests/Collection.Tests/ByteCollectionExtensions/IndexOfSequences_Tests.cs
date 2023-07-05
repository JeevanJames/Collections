// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ByteCollectionExtensions;

public sealed class IndexOfSequencesTests
{
    [Theory, ByteArray(CollectionType.Null)]
    public void Throws_if_bytes_are_null(IList<byte> bytes)
    {
        Should.Throw<ArgumentNullException>(() => bytes.IndexOfSequences<byte>(1));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_start_is_negative(IList<byte> bytes)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => bytes.IndexOfSequences<byte>(-1, 10, 1));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_count_is_negative(IList<byte> bytes)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => bytes.IndexOfSequences<byte>(0, -1, 1));
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Throws_if_sequence_is_null(IList<byte> bytes)
    {
        Should.Throw<ArgumentNullException>(() => bytes.IndexOfSequences(0, 100, sequence: null!));
    }

    [Theory]
    [InlineData(new byte[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new byte[] { 2, 3 }, new[] { 1, 4, 6 })]
    [InlineData(new byte[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new byte[] { 2, 3, 1 }, new[] { 1 })]
    [InlineData(new byte[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new byte[] { 1 }, new[] { 0, 3 })]
    public void Returns_indices_of_existing_sequence(IList<byte> bytes, byte[] sequence, int[] expectedIndices)
    {
        bytes.IndexOfSequences(sequence).ShouldBe(expectedIndices);
    }

    [Theory]
    [InlineData(new byte[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new byte[] { 3, 2, 1 })]
    [InlineData(new byte[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new byte[] { 2, 3, 3 })]
    [InlineData(new byte[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new byte[] { 9 })]
    public void Returns_minus_one_if_sequence_not_found(IList<byte> bytes, byte[] sequence)
    {
        bytes.IndexOfSequences(sequence).ShouldBeEmpty();
    }
}
