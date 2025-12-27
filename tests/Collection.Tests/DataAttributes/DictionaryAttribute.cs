// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.DataAttributes;

public sealed class DictionaryAttribute(CollectionType collectionType) : BaseCollectionAttribute(collectionType)
{
    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => new Dictionary<string, int>(0),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => new Dictionary<string, int>
        {
            ["One"] = 1,
            ["Two"] = 2,
            ["Three"] = 3,
            ["Four"] = 4,
            ["Five"] = 5,
            ["Six"] = 6,
        },
        _ => throw new InvalidOperationException(),
    };
}
