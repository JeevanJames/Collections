// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.List;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class Random_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.Random(10).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.Empty)]
        public void Returns_empty_sequence_if_collection_is_empty(IList<int> collection)
        {
            IEnumerable<int> random = collection.Random(10).ToList();

            random.ShouldBeEmpty();
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_count_is_less_than_one(IList<int> collection)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Random(0).ToList());
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Random(-1).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Generates_random_sequence(IList<int> collection)
        {
            var random = collection.Random(10).ToList();

            random.Count.ShouldBe(10);
            random.ShouldAllBe(n => collection.Any(element => element == n));
        }
    }
}
