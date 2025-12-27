// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.ShortCollectionExtensions;

public sealed class IsZeroed_Tests
{
    [Theory, ShortArray(CollectionType.Null)]
    public void Throws_if_collection_is_null(IList<short> collection)
    {
        Should.Throw<ArgumentNullException>(() => collection.IsZeroed());
    }

    [Theory]
    [InlineData(new short[0])]
    [InlineData(new short[] {0, 0, 0, 0})]
    public void Returns_true_if_collection_is_zeroed(IList<short> collection)
    {
        collection.IsZeroed().ShouldBeTrue();
    }

    [Fact]
    public void Returns_false_if_collection_contains_nonzeroes()
    {
        IList<short> collection = new List<short> {0, 1, 0, 0};

        collection.IsZeroed().ShouldBeFalse();
    }
}
