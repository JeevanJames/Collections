// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.CollectionExtensions;

public sealed class IsNotNullOrEmptyTests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Returns_false_if_sequence_is_null(IEnumerable<int> sequence)
    {
        sequence.IsNotNullOrEmpty().ShouldBeFalse();
    }

    [Theory, DataAttributes.Collection(CollectionType.Empty)]
    public void Returns_false_if_sequence_is_empty(IEnumerable<int> sequence)
    {
        sequence.IsNotNullOrEmpty().ShouldBeFalse();
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Returns_true_if_sequence_is_not_empty(IEnumerable<int> sequence)
    {
        sequence.IsNotNullOrEmpty().ShouldBeTrue();
    }
}
