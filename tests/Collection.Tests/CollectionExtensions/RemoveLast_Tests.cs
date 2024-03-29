﻿// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.ListExtensions;
#endif

namespace Collection.Tests.CollectionExtensions;

public sealed class RemoveLastTests
{
    [Fact]
    public void Throws_if_collection_is_null()
    {
        IList<int> collection = null!;

        Should.Throw<ArgumentNullException>(() => collection.RemoveLast(n => n % 2 == 0));
    }

    [Fact]
    public void Throws_if_predicate_is_null()
    {
        IList<int> collection = new[] {1, 2};
        Should.Throw<ArgumentNullException>(() => collection.RemoveLast(null!));
    }

    [Theory]
    [InlineData(new int[0])]
    [InlineData(new [] {1, 3, 5, 7, 9})]
    public void Returns_false_if_matching_element_not_found(IList<int> collection)
    {
        collection.RemoveLast(n => n % 2 == 0).ShouldBeFalse();
    }

    [Fact]
    public void Removes_last_matching_element()
    {
        IList<int> collection = new List<int> {1, 2, 3, 4, 5, 6};

        bool removed = collection.RemoveLast(n => n % 2 == 0);

        removed.ShouldBeTrue();
        collection.Count.ShouldBe(5);
        collection.ShouldContain(n => n == 6, 0);
    }
}
