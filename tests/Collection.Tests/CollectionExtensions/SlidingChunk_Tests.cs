// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.List;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class SlidingChunk_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_list_is_null(IList<int> list)
        {
            Should.Throw<ArgumentNullException>(() => list.SlidingChunk(2).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_chunksize_is_less_than_one(IList<int> list)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => list.SlidingChunk(0).ToList());
            Should.Throw<ArgumentOutOfRangeException>(() => list.SlidingChunk(-1).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Throws_returns_whole_list_if_chunksize_greater_than_list_size(IList<int> list)
        {
            IEnumerable<IList<int>> chunks = list.SlidingChunk(100);

            chunks.ShouldBe(new int[][]
            {
                new[] {1, 2, 3, 4, 5, 6}
            });
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_overlapping_chunks_based_on_specified_size(IList<int> list)
        {
            IEnumerable<IList<int>> chunks = list.SlidingChunk(2);

            chunks.ShouldBe(new int[][]
            {
                new [] {1, 2},
                new [] {2, 3},
                new [] {3, 4},
                new [] {4, 5},
                new [] {5, 6},
            });
        }
    }
}
