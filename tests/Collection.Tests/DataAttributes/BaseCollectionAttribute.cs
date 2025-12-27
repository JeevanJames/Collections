// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System.Reflection;

using Xunit.Sdk;

namespace Collection.Tests.DataAttributes;

public abstract class BaseCollectionAttribute(CollectionType collectionType) : DataAttribute
{
    protected CollectionType CollectionType { get; } = collectionType;

    public override sealed IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] {CreateCollection()};
    }

    protected abstract object CreateCollection();
}
