// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.LongCollectionExtensions
{
    public sealed class IsZeroed_Tests
    {
        [Theory, LongArray(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<long> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.IsZeroed());
        }

        [Theory]
        [InlineData(new long[0])]
        [InlineData(new long[] {0, 0, 0, 0})]
        public void Returns_true_if_collection_is_zeroed(IList<long> collection)
        {
            collection.IsZeroed().ShouldBeTrue();
        }

        [Fact]
        public void Returns_false_if_collection_contains_nonzeroes()
        {
            IList<long> collection = new List<long> {0, 1, 0, 0};

            collection.IsZeroed().ShouldBeFalse();
        }
    }
}
