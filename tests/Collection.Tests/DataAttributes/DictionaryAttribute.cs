// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Collection.Tests.DataAttributes
{
    public sealed class DictionaryAttribute : BaseCollectionAttribute
    {
        public DictionaryAttribute(CollectionType collectionType) : base(collectionType)
        {
        }

        protected override object CreateCollection()
        {
            switch (CollectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new Dictionary<string, int>(0);
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new Dictionary<string, int>
                    {
                        ["One"] = 1,
                        ["Two"] = 2,
                        ["Three"] = 3,
                        ["Four"] = 4,
                        ["Five"] = 5,
                        ["Six"] = 6,
                    };
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}