// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ShortCollectionExtensions;

public sealed class IsNullOrZeroedTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(new short[0])]
    [InlineData(new short[] {0, 0, 0, 0})]
    public void Returns_true_if_collection_is_null_or_empty_or_zeroed(IList<short> shorts)
    {
        shorts.IsNullOrZeroed().ShouldBeTrue();
    }

    [Fact]
    public void Returns_false_if_any_element_is_non_zero()
    {
        short[] shorts = {0, 1, 0, 0};
        shorts.IsNullOrZeroed().ShouldBeFalse();
    }
}
