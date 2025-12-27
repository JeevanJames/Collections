// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests.DataAttributes;

public sealed class ByteArrayAttribute(CollectionType collectionType) : BaseCollectionAttribute(collectionType)
{
    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<byte>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => [1, 2, 3, 4, 5, 6],
        _ => throw new InvalidOperationException()
    };
}

public sealed class IntArrayAttribute(CollectionType collectionType) : BaseCollectionAttribute(collectionType)
{
    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<int>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix=> [1, 2, 3, 4, 5, 6],
        _ => throw new InvalidOperationException()
    };
}

public sealed class LongArrayAttribute(CollectionType collectionType) : BaseCollectionAttribute(collectionType)
{
    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<long>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => [1L, 2, 3, 4, 5, 6],
        _ => throw new InvalidOperationException(),
    };
}

public sealed class CharArrayAttribute(CollectionType collectionType) : BaseCollectionAttribute(collectionType)
{
    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<char>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => ['1', '2', '3', '4', '5', '6'],
        _ => throw new InvalidOperationException()
    };
}

public sealed class ShortArrayAttribute(CollectionType collectionType) : BaseCollectionAttribute(collectionType)
{
    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<short>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => [1, 2, 3, 4, 5, 6],
        _ => throw new InvalidOperationException()
    };
}
