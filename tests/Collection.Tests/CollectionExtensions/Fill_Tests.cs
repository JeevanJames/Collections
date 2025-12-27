// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.CollectionExtensions;

public sealed class FillTests
{
    [Fact]
    public void Throws_if_collection_is_null()
    {
        IList<int> collection = null!;

        Should.Throw<ArgumentNullException>(() => collection.Fill(0));
        Should.Throw<ArgumentNullException>(() => collection.Fill(n => n * 2));
    }

    [Fact]
    public void Does_not_do_anything_if_collection_is_empty()
    {
        IList<int> collection = new List<int>(0);

        Should.NotThrow(() => collection.Fill(0));
        Should.NotThrow(() => collection.Fill(n => n * 2));
    }

    [Fact]
    public void Fills_collection_with_value()
    {
        IList<int> collection = new int[6];

        collection.Fill(5);

        collection.Count.ShouldBe(6);
        collection.ShouldBe(new[] {5, 5, 5, 5, 5, 5});
    }

    [Fact]
    public void Throws_if_generator_is_null()
    {
        IList<int> collection = new int[6];

        Should.Throw<ArgumentNullException>(() => collection.Fill(generator: null!));
    }

    [Fact]
    public void Fills_collection_with_generated_values()
    {
        IList<int> collection = new int[6];

        collection.Fill(n => n * 2);

        collection.Count.ShouldBe(6);
        collection.ShouldBe(new[] {0, 2, 4, 6, 8, 10});
    }
}
