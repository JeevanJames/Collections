// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Collection.Tests.DataAttributes
{
    public sealed class CollectionAttribute : BaseCollectionAttribute
    {
        public CollectionAttribute(CollectionType collectionType) : base(collectionType)
        {
        }

        protected override object CreateCollection()
        {
            switch (CollectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new List<int>(0);
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new List<int> {1, 2, 3, 4, 5, 6};
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}