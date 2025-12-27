// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

#if EXPLICIT
using Collections.Net.Extensions.EnumerableExtensions;
#endif

using Shouldly;
using Xunit;

namespace Collection.Tests.CollectionExtensions;

public sealed class CollectionExtensionsWhereNotTests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Throws_if_collection_is_null(IEnumerable<int> collection)
    {
        Should.Throw<ArgumentNullException>(() => collection.WhereNot(n => n % 2 == 0));
        Should.Throw<ArgumentNullException>(() => collection.WhereNot((n, i) => n % 2 == 0));
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_predicate_is_null(IEnumerable<int> collection)
    {
        Should.Throw<ArgumentNullException>(() => collection.WhereNot((Func<int, bool>)null!));
        Should.Throw<ArgumentNullException>(() => collection.WhereNot((Func<int, int, bool>)null!));
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Filters_collection_based_on_inverse_of_predicate(IEnumerable<int> collection)
    {
        IEnumerable<int> oddNumbers = collection.WhereNot(n => n % 2 == 0);
        IEnumerable<int> filteredNumbers = collection.WhereNot((n, i) => n % 2 == 0 || i == 0);

        oddNumbers.ShouldBe(new [] {1, 3, 5});
        filteredNumbers.ShouldBe(new [] {3, 5});
    }
}
