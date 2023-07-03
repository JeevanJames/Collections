// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

#if EXPLICIT
using Collections.Net.Extensions.EnumerableExtensions;
#endif

using Shouldly;

using Xunit;

namespace Collection.Tests.CollectionExtensions;

public sealed class PartitionTests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Throws_if_collection_is_null(IList<int> collection)
    {
        Should.Throw<ArgumentNullException>(() => collection.Partition(n => n % 2 == 0));
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_predicate_is_null(IList<int> collection)
    {
        Should.Throw<ArgumentNullException>(() => collection.Partition(null!));
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Returns_matches_and_mismatches(IList<int> collection)
    {
        (IEnumerable<int> matches, IEnumerable<int> mismatches) = collection.Partition(n => n % 2 == 0);

        matches.ShouldBe(new[] {2, 4, 6});
        mismatches.ShouldBe(new[] {1, 3, 5});
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Returns_matches_where_all_match(IList<int> collection)
    {
        (IEnumerable<int> matches, IEnumerable<int> mismatches) = collection.Partition(n => n >= 1 && n <= 6);

        matches.ShouldBe(new[] {1, 2, 3, 4, 5, 6});
        mismatches.Any().ShouldBeFalse();
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Returns_matches_where_none_match(IList<int> collection)
    {
        (IEnumerable<int> matches, IEnumerable<int> mismatches) = collection.Partition(n => n == 10);

        matches.Any().ShouldBeFalse();
        mismatches.ShouldBe(new[] { 1, 2, 3, 4, 5, 6 });
    }
}
