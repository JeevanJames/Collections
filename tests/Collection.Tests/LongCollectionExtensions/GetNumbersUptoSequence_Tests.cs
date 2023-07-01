// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Numeric;
#endif

namespace Collection.Tests.LongCollectionExtensions
{
    public sealed class GetNumbersUptoSequence_Tests
    {
        [Theory]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {4, 5, 6}, 0, new long[] {1, 2, 3})]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {4, 5, 6}, 1, new long[] {2, 3})]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {1, 2, 3}, 0, new long[0])]
        public void Returns_longs_upto_sequence(long[] longs, long[] sequence, int start, long[] expectedResult)
        {
            IList<long> result = longs.GetNumbersUptoSequence(start, sequence);

            result.ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {7, 8}, 0)]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {7, 8}, 2)]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {1, 2, 3}, 1)]
        public void Returns_null_if_sequence_not_found(long[] longs, long[] sequence, int start)
        {
            IList<long> result = longs.GetNumbersUptoSequence(start, sequence);

            result.ShouldBeNull();
        }
    }
}
