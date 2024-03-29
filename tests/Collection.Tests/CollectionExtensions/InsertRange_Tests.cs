﻿// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.ListExtensions;
#endif

namespace Collection.Tests.CollectionExtensions;

public sealed class InsertRangeTests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Throws_if_list_is_null(IList<int> list)
    {
        Should.Throw<ArgumentNullException>(() => list.InsertRange(1, 7, 8, 9));
        Should.Throw<ArgumentNullException>(() => list.InsertRange(1, new[] {7, 8, 9}));
        Should.Throw<ArgumentNullException>(() => list.InsertRange(1, new[] {7, 8, 9}, n => n % 2 == 0));
        Should.Throw<ArgumentNullException>(() => list.InsertRange(1, new[] {"7", "8", "9"}, int.Parse));
        Should.Throw<ArgumentNullException>(() =>
            list.InsertRange(1, new[] {"7", "8", "9"}, s => s == "8", int.Parse));
        Should.Throw<ArgumentNullException>(() =>
            list.InsertRange(1, new[] {"7", "8", "9"}, s => s == "8" || s == "9", int.Parse, n => n % 2 == 0));
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Adds_range_if_index_is_greater_or_equal_to_list_count(IList<int> list)
    {
        list.InsertRange(100, new [] {7, 8, 9});
        list.ShouldBe(new [] {1, 2, 3, 4, 5, 6, 7, 8, 9});

        list.InsertRange(list.Count, new [] {10, 11, 12});
        list.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12});

        // Empty list
        var emptyList = new List<int>();
        emptyList.InsertRange(0, 1, 2, 3);
        emptyList.ShouldBe(new[] {1, 2, 3});
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_index_if_negative(IList<int> list)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => list.InsertRange(-1, 7, 8, 9));
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Does_nothing_if_items_is_null(IList<int> list)
    {
        list.InsertRange(1, null!);

        list.ShouldBe(new[] {1, 2, 3, 4, 5, 6});
    }

    [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
    public void Inserts_items_at_correct_index(IList<int> list)
    {
        list.InsertRange(5, 7, 8, 9);
        list.ShouldBe(new[] {1, 2, 3, 4, 5, 7, 8, 9, 6});

        list.InsertRange(0, 10, 11, 12);
        list.ShouldBe(new[] { 10, 11, 12, 1, 2, 3, 4, 5, 7, 8, 9, 6 });

        list.InsertRange(3, 13, 14, 15);
        list.ShouldBe(new[] { 10, 11, 12, 13, 14, 15, 1, 2, 3, 4, 5, 7, 8, 9, 6 });
    }

    [Fact]
    public void Inserts_items_at_correct_index_for_custom_collection()
    {
        var list = new NoDuplicateCollection {1, 2, 3};
        list.InsertRange(0, 2, 3, 4, 5);
        list.ShouldBe(new[] {4, 5, 1, 2, 3});
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_predicate_is_null(IList<int> list)
    {
        Should.Throw<ArgumentNullException>(() => list.InsertRange(2, new[] {7, 8, 9}, predicate: null!));
        Should.Throw<ArgumentNullException>(() =>
            list.InsertRange(2, new[] {"7", "8", "9"}, beforeConvertPredicate: null!, converter: int.Parse));
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_converter_is_null(IList<int> list)
    {
        Should.Throw<ArgumentNullException>(() => list.InsertRange(2, new[] { 7, 8, 9 }, converter: null!));
        Should.Throw<ArgumentNullException>(() =>
            list.InsertRange(2, new[] { "7", "8", "9" }, beforeConvertPredicate: n => n == "8", converter: null!));
    }
}

public sealed class NoDuplicateCollection : Collection<int>
{
    protected override void InsertItem(int index, int item)
    {
        if (this.All(e => e != item))
            base.InsertItem(index, item);
    }
}
