// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Numeric;
#endif

namespace Collection.Tests.ByteCollectionExtensions
{
    public sealed class IsZeroed_Tests
    {
        [Theory, ByteArray(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<byte> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.IsZeroed());
        }

        [Theory]
        [InlineData(new byte[0])]
        [InlineData(new byte[] {0, 0, 0, 0})]
        public void Returns_true_if_collection_is_zeroed(IList<byte> collection)
        {
            collection.IsZeroed().ShouldBeTrue();
        }

        [Fact]
        public void Returns_false_if_collection_contains_nonzeroes()
        {
            IList<byte> collection = new List<byte> {0, 1, 0, 0};

            collection.IsZeroed().ShouldBeFalse();
        }
    }
}
