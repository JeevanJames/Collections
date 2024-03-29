﻿// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

#if EXPLICIT
using Collections.Net.Extensions.EnumerableExtensions;
#endif

using Shouldly;

using Xunit;

namespace Collection.Tests.CollectionExtensions;

public sealed class IsNullOrEmptyTests
{
    [Theory]
    [InlineData((IEnumerable<int>?)null)]
    [InlineData(new int[0])]
    public void Returns_true_if_collection_is_null_or_empty(IEnumerable<int> collection)
    {
        collection.IsNullOrEmpty().ShouldBeTrue();
    }

    [Theory, MemberData(nameof(NotEmptyCollections))]
    public void Returns_false_if_collection_is_not_empty(IEnumerable<int> collection)
    {
        collection.IsNullOrEmpty().ShouldBeFalse();
    }

    public static IEnumerable<object[]> NotEmptyCollections()
    {
        yield return new object[] {Enumerable.Range(1, 5)};
        yield return new object[] {new[] {1, 2, 3, 4, 5}};
    }
}
