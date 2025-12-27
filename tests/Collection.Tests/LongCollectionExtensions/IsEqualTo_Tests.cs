// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.LongCollectionExtensions;

public sealed class IsEqualTo_Tests
{
    [Theory]
    [InlineData(new long[0])]
    [InlineData(new long[] {1, 2, 3})]
    public void Returns_true_for_the_same_reference(IList<long> longs)
    {
        longs.IsEqualTo(longs).ShouldBeTrue();
    }

    [Theory]
    [InlineData(new long[0], null)]
    [InlineData(new long[] {1, 2}, null)]
    [InlineData(null, new long[0])]
    [InlineData(null, new long[] {1, 2})]
    public void Returns_false_if_any_collection_is_null(IList<long>? longs1, IList<long>? longs2)
    {
        longs1.IsEqualTo(longs2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new long[0], new long[] {1, 2})]
    [InlineData(new long[] {1, 2}, new long[0])]
    public void Returns_false_for_collections_of_different_lengths(IList<long> longs1, IList<long> longs2)
    {
        longs1.IsEqualTo(longs2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new long[] {2, 1}, new long[] {1, 2})]
    [InlineData(new long[] {1, 2, 3, 4}, new long[] {1, 2, 4, 3})]
    public void Returns_false_for_collections_of_different_content(IList<long> longs1, IList<long> longs2)
    {
        longs1.IsEqualTo(longs2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new long[0], new long[0])]
    [InlineData(new long[] {1, 2}, new long[] {1, 2})]
    [InlineData(new long[] {1, 2, 3, 4}, new long[] {1, 2, 3, 4})]
    public void Returns_true_for_collections_of_same_content(IList<long> longs1, IList<long> longs2)
    {
        longs1.IsEqualTo(longs2).ShouldBeTrue();
    }

    [Theory, LongArray(CollectionType.NonEmpty)]
    public void Returns_true_for_params_array(IList<long> longs)
    {
        longs.IsEqualTo(1, 2, 3, 4, 5, 6).ShouldBeTrue();
    }
}
