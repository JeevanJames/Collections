// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.ByteCollectionExtensions;

public sealed class IsEqualToTests
{
    [Theory]
    [InlineData(new byte[0])]
    [InlineData(new byte[] {1, 2, 3})]
    public void Returns_true_for_the_same_reference(IList<byte> bytes)
    {
        bytes.IsEqualTo(bytes).ShouldBeTrue();
    }

    [Theory]
    [InlineData(new byte[0], null)]
    [InlineData(new byte[] {1, 2}, null)]
    [InlineData(null, new byte[0])]
    [InlineData(null, new byte[] {1, 2})]
    public void Returns_false_if_any_collection_is_null(IList<byte>? bytes1, IList<byte>? bytes2)
    {
        bytes1.IsEqualTo(bytes2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new byte[0], new byte[] {1, 2})]
    [InlineData(new byte[] {1, 2}, new byte[0])]
    public void Returns_false_for_collections_of_different_lengths(IList<byte> bytes1, IList<byte> bytes2)
    {
        bytes1.IsEqualTo(bytes2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new byte[] {2, 1}, new byte[] {1, 2})]
    [InlineData(new byte[] {1, 2, 3, 4}, new byte[] {1, 2, 4, 3})]
    public void Returns_false_for_collections_of_different_content(IList<byte> bytes1, IList<byte> bytes2)
    {
        bytes1.IsEqualTo(bytes2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new byte[0], new byte[0])]
    [InlineData(new byte[] {1, 2}, new byte[] {1, 2})]
    [InlineData(new byte[] {1, 2, 3, 4}, new byte[] {1, 2, 3, 4})]
    public void Returns_true_for_collections_of_same_content(IList<byte> bytes1, IList<byte> bytes2)
    {
        bytes1.IsEqualTo(bytes2).ShouldBeTrue();
    }

    [Theory, ByteArray(CollectionType.NonEmpty)]
    public void Returns_true_for_params_array(IList<byte> bytes)
    {
        bytes.IsEqualTo<byte>(1, 2, 3, 4, 5, 6).ShouldBeTrue();
    }
}
