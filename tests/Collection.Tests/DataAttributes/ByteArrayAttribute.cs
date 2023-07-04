// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

namespace Collection.Tests.DataAttributes;

public sealed class ByteArrayAttribute : BaseCollectionAttribute
{
    public ByteArrayAttribute(CollectionType collectionType) : base(collectionType)
    {
    }

    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<byte>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => new byte[] { 1, 2, 3, 4, 5, 6 },
        _ => throw new InvalidOperationException()
    };
}

public sealed class IntArrayAttribute : BaseCollectionAttribute
{
    public IntArrayAttribute(CollectionType collectionType) : base(collectionType)
    {
    }

    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<int>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix=> new[] { 1, 2, 3, 4, 5, 6 },
        _ => throw new InvalidOperationException()
    };
}

public sealed class LongArrayAttribute : BaseCollectionAttribute
{
    public LongArrayAttribute(CollectionType collectionType) : base(collectionType)
    {
    }

    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<long>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => new[] { 1L, 2, 3, 4, 5, 6 },
        _ => throw new InvalidOperationException(),
    };
}

public sealed class CharArrayAttribute : BaseCollectionAttribute
{
    public CharArrayAttribute(CollectionType collectionType) : base(collectionType)
    {
    }

    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<char>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => new[] { '1', '2', '3', '4', '5', '6' },
        _ => throw new InvalidOperationException()
    };
}

public sealed class ShortArrayAttribute : BaseCollectionAttribute
{
    public ShortArrayAttribute(CollectionType collectionType) : base(collectionType)
    {
    }

    protected override object CreateCollection() => CollectionType switch
    {
        CollectionType.Null => null!,
        CollectionType.Empty => Array.Empty<short>(),
        CollectionType.NonEmpty or CollectionType.NumbersOneToSix => new short[] { 1, 2, 3, 4, 5, 6 },
        _ => throw new InvalidOperationException()
    };
}
