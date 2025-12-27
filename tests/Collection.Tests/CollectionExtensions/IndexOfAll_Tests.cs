// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.CollectionExtensions;

public sealed class IndexOfAllTests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Throws_if_collection_is_null(IList<int> collection)
    {
        // ToList is needed because the returned collection is lazy
        Should.Throw<ArgumentNullException>(() => collection.IndexOfAll(n => n % 2 == 0).ToList());
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_predicate_is_null(IList<int> collection)
    {
        // ToList is needed because the returned collection is lazy
        Should.Throw<ArgumentNullException>(() => collection.IndexOfAll(predicate: null!).ToList());
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Finds_indices_of_all_matching_elements(IList<int> collection)
    {
        IEnumerable<int> indices = collection.IndexOfAll(n => n % 2 == 0);

        indices.ShouldBe(new[] {1, 3, 5});
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Returns_empty_enumeration_if_matching_elements_not_found(IList<int> collection)
    {
        IEnumerable<int> indices = collection.IndexOfAll(n => n > 100);

        indices.ShouldBeEmpty();
    }
}
