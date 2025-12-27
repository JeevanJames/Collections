// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Reflection;

using Xunit.Sdk;

namespace Collection.Tests.DataAttributes
{
    public abstract class BaseCollectionAttribute : DataAttribute
    {
        protected BaseCollectionAttribute(CollectionType collectionType)
        {
            CollectionType = collectionType;
        }

        protected CollectionType CollectionType { get; }

        public override sealed IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] {CreateCollection()};
        }

        protected abstract object CreateCollection();
    }
}