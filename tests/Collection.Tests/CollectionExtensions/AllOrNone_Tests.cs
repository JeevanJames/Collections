// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

#if EXPLICIT
using Collections.Net.EnumerablesEx;
#endif

using Shouldly;

using Xunit;

namespace Collection.Tests.CollectionExtensions;

public sealed class AllOrNoneTests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Throws_if_sequence_is_null(IEnumerable<int> sequence)
    {
        Should.Throw<ArgumentNullException>(() => sequence.AllOrNone(n => n % 2 == 0));
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_predicate_is_null(IEnumerable<int> sequence)
    {
        Should.Throw<ArgumentNullException>(() => sequence.AllOrNone(null!));
    }

    [Theory, DataAttributes.Collection(CollectionType.Empty)]
    public void Returns_true_for_empty_sequence(IEnumerable<int> sequence)
    {
        sequence.AllOrNone(n => n % 2 == 0).ShouldBeTrue();
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Returns_true_if_all_items_match(IEnumerable<int> sequence)
    {
        sequence.AllOrNone(n => n < 7).ShouldBeTrue();
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Returns_true_if_no_items_match(IEnumerable<int> sequence)
    {
        sequence.AllOrNone(n => n > 7).ShouldBeTrue();
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Returns_false_if_some_items_match_and_some_dont(IEnumerable<int> sequence)
    {
        sequence.AllOrNone(n => n % 2 == 0).ShouldBeFalse();
    }
}
