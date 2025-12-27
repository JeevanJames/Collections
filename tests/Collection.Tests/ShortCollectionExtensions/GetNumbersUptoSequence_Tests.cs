// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ShortCollectionExtensions;

public sealed class GetNumbersUptoSequenceTests
{
    [Theory]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {4, 5, 6}, 0, new short[] {1, 2, 3})]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {4, 5, 6}, 1, new short[] {2, 3})]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {1, 2, 3}, 0, new short[0])]
    public void Returns_shorts_upto_sequence(short[] shorts, short[] sequence, int start, short[] expectedResult)
    {
        IList<short>? result = shorts.GetNumbersUptoSequence(start, sequence);

        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {7, 8}, 0)]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {7, 8}, 2)]
    [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {1, 2, 3}, 1)]
    public void Returns_null_if_sequence_not_found(short[] shorts, short[] sequence, int start)
    {
        IList<short>? result = shorts.GetNumbersUptoSequence(start, sequence);

        result.ShouldBeNull();
    }
}
