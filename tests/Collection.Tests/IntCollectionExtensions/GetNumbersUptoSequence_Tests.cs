// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions
{
    public sealed class GetNumbersUptoSequence_Tests
    {
        [Theory]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {4, 5, 6}, 0, new int[] {1, 2, 3})]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {4, 5, 6}, 1, new int[] {2, 3})]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {1, 2, 3}, 0, new int[0])]
        public void Returns_ints_upto_sequence(int[] ints, int[] sequence, int start, int[] expectedResult)
        {
            IList<int> result = ints.GetNumbersUptoSequence(start, sequence);

            result.ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {7, 8}, 0)]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {7, 8}, 2)]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {1, 2, 3}, 1)]
        public void Returns_null_if_sequence_not_found(int[] ints, int[] sequence, int start)
        {
            IList<int> result = ints.GetNumbersUptoSequence(start, sequence);

            result.ShouldBeNull();
        }
    }
}
