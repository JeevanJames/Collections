// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.CollectionExtensions;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class RangeOfLength_Tests
    {
        [Theory]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 2, 3, new[] {3, 4, 5})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, 4, new[] {1, 2, 3, 4})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, null, new[] {4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, null, new[] {1, 2, 3, 4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, 1, new[] {4})]
        public void Returns_iterator_for_specified_range(ICollection<int> collection, int? start, int? end, IList<int> expectedResult)
        {
            IEnumerable<int> result = collection.RangeOfLength(start, end);

            result.ShouldBe(expectedResult);
        }
    }
}
