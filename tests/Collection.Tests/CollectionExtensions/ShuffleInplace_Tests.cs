// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.CollectionExtensions;

public sealed class ShuffleInplace_Tests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Throws_if_collection_is_null(IList<int> collection)
    {
        Should.Throw<ArgumentNullException>(() => collection.ShuffleInplace());
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_iterations_is_less_than_one(IList<int> collection)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => collection.ShuffleInplace(0));
        Should.Throw<ArgumentOutOfRangeException>(() => collection.ShuffleInplace(-1));
    }

    [Fact]
    public void Shuffles_collection_in_place()
    {
        int[] collection = [1, 2, 3, 4, 5, 6, 7, 8];
        
        collection.ShuffleInplace();

        collection.ShouldNotBe([1, 2, 3, 4, 5, 6, 7, 8]);
    }
}
