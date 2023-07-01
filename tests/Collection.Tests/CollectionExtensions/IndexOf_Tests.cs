﻿// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.List;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class IndexOf_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.IndexOf(n => n % 2 == 0));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.IndexOf(null));
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Finds_index_of_existing_element(IList<int> collection)
        {
            int index = collection.IndexOf(n => n % 2 == 0);

            index.ShouldBe(1);
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_negative_number_for_nonexistent_element(IList<int> collection)
        {
            int index = collection.IndexOf(n => n == 100);

            index.ShouldBeLessThan(0);
        }
    }
}
