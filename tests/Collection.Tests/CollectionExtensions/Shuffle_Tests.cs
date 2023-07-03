// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

#if EXPLICIT
using Collections.Net.Extensions.EnumerableExtensions;
#endif

using Shouldly;

using Xunit;

namespace Collection.Tests.CollectionExtensions;

public sealed class ShuffleTests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Throws_if_collection_is_null(IList<int> collection)
    {
        Should.Throw<ArgumentNullException>(() => collection.Shuffle());
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_iterations_is_less_than_one(IList<int> collection)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => collection.Shuffle(0));
        Should.Throw<ArgumentOutOfRangeException>(() => collection.Shuffle(-1));
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Returns_shuffled_collection(IList<int> collection)
    {
        IEnumerable<int> shuffled = collection.Shuffle();

        shuffled.ShouldNotBeSameAs(collection);
        shuffled.ShouldNotBe(new [] {1, 2, 3, 4, 5, 6});
    }
}
