// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions
{
    public sealed class IsNullOrZeroed_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(new int[0])]
        [InlineData(new int[] {0, 0, 0, 0})]
        public void Returns_true_if_collection_is_null_or_empty_or_zeroed(IList<int> ints)
        {
            ints.IsNullOrZeroed().ShouldBeTrue();
        }

        [Fact]
        public void Returns_false_if_any_element_is_non_zero()
        {
            int[] ints = {0, 1, 0, 0};
            ints.IsNullOrZeroed().ShouldBeFalse();
        }
    }
}
