// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.CollectionExtensions;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class Range_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(ICollection<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.Range(0, 1));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_start_is_out_of_range(ICollection<int> collection)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Range(-1, null));
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Range(collection.Count, null));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_end_is_less_than_start(ICollection<int> collection)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Range(2, 1));
        }

        [Theory]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 2, 4, new[] {3, 4})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, 3, new[] {1, 2, 3})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, null, new[] {4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, 200, new[] {4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, null, new[] {1, 2, 3, 4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, 100, new[] {1, 2, 3, 4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, 3, new int[0])]
        public void Returns_iterator_for_specified_range(ICollection<int> collection, int? start, int? end, IList<int> expectedResult)
        {
            IEnumerable<int> result = collection.Range(start, end);

            result.ShouldBe(expectedResult);
        }
    }
}
