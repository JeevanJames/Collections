// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.LongCollectionExtensions;

public sealed class GetNumbersUptoSequenceTests
{
    [Theory]
    [InlineData(new[] {1L, 2, 3, 4, 5, 6}, new[] {4L, 5, 6}, 0, new[] {1L, 2, 3})]
    [InlineData(new[] {1L, 2, 3, 4, 5, 6}, new[] {4L, 5, 6}, 1, new[] {2L, 3})]
    [InlineData(new[] {1L, 2, 3, 4, 5, 6}, new[] {1L, 2, 3}, 0, new long[0])]
    public void Returns_longs_upto_sequence(long[] longs, long[] sequence, int start, long[] expectedResult)
    {
        IList<long>? result = longs.GetNumbersUptoSequence(start, sequence);

        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData(new[] {1L, 2, 3, 4, 5, 6}, new[] {7L, 8}, 0)]
    [InlineData(new[] {1L, 2, 3, 4, 5, 6}, new[] {7L, 8}, 2)]
    [InlineData(new[] {1L, 2, 3, 4, 5, 6}, new[] {1L, 2, 3}, 1)]
    public void Returns_null_if_sequence_not_found(long[] longs, long[] sequence, int start)
    {
        IList<long>? result = longs.GetNumbersUptoSequence(start, sequence);

        result.ShouldBeNull();
    }
}
