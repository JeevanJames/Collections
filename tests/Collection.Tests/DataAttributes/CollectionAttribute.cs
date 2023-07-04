// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

namespace Collection.Tests.DataAttributes;

public sealed class CollectionAttribute : BaseCollectionAttribute
{
    public CollectionAttribute(CollectionType collectionType) : base(collectionType)
    {
    }

    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => new List<int>(0),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => new List<int> { 1, 2, 3, 4, 5, 6 },
        _ => throw new InvalidOperationException(),
    };
}
