// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.DictionaryExtensions;

public sealed class AddOrUpdate_Tests
{
    [Theory, Dictionary(CollectionType.Null)]
    public void Throws_if_dictionary_is_null(IDictionary<string, int> dictionary)
    {
        Should.Throw<ArgumentNullException>(() => dictionary.AddOrUpdate("Seven", 7));
    }

    [Theory, Dictionary(CollectionType.NumbersOneToSix)]
    public void Adds_new_entry(IDictionary<string, int> dictionary)
    {
        dictionary.AddOrUpdate("Seven", 7);

        dictionary.Count.ShouldBe(7);
        dictionary["Seven"].ShouldBe(7);
    }

    [Theory, Dictionary(CollectionType.NumbersOneToSix)]
    public void Updates_existing_entry(IDictionary<string, int> dictionary)
    {
        dictionary.AddOrUpdate("Two", 10);

        dictionary.Count.ShouldBe(6);
        dictionary["Two"].ShouldBe(10);
    }
}
