#region --- License & Copyright Notice ---
/*
Custom collections and collection extensions for .NET
Copyright (c) 2018-2019 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System;
using System.Collections.Generic;
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

namespace Collection.Tests.LongCollectionExtensions
{
    public sealed class IndexOfSequence_Tests
    {
        [Theory, LongArray(CollectionType.Null)]
        public void Throws_if_longs_are_null(IList<long> longs)
        {
            Should.Throw<ArgumentNullException>(() => longs.IndexOfSequence(1));
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Throws_if_start_is_negative(IList<long> longs)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => longs.IndexOfSequence(-1, 10, 1));
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Throws_if_count_is_negative(IList<long> longs)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => longs.IndexOfSequence(0, -1, 1));
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Throws_if_sequence_is_null(IList<long> longs)
        {
            Should.Throw<ArgumentNullException>(() => longs.IndexOfSequence(0, 100, null));
        }

        [Theory]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {1, 2}, 0)]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {4, 5}, 3)]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {6}, 5)]
        public void Returns_index_of_existing_sequence(IList<long> longs, long[] sequence, int expectedIndex)
        {
            longs.IndexOfSequence(sequence).ShouldBe(expectedIndex);
        }

        [Theory]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {2, 1})]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {9})]
        [InlineData(new long[] {1, 2, 3, 4, 5, 6}, new long[] {1, 2, 4})]
        public void Returns_minus_one_if_sequence_not_found(IList<long> longs, long[] sequence)
        {
            longs.IndexOfSequence(sequence).ShouldBeLessThan(0);
        }

        [Theory]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 2, 3 }, 1, 5, 1)]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 4, 5 }, 2, 9, 3)]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 6 }, 1, 100, 5)]
        public void Returns_index_of_existing_sequence_for_start_and_count(IList<long> longs, long[] sequence, int start, int count, int expectedIndex)
        {
            longs.IndexOfSequence(start, count, sequence).ShouldBe(expectedIndex);
        }

        [Theory]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 3, 2 }, 1, 5)]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 1, 2 }, 2, 9)]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, new long[] { 7 }, 1, 100)]
        public void Returns_minus_one_for_start_and_count_if_sequence_not_found(IList<long> longs, long[] sequence, int start, int count)
        {
            longs.IndexOfSequence(start, count, sequence).ShouldBeLessThan(0);
        }
    }
}
