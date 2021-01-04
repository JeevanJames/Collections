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

#if EXPLICIT
using Collections.Net.Numeric;
#endif

namespace Collection.Tests.ShortCollectionExtensions
{
    public sealed class IndexOfSequence_Tests
    {
        [Theory, ShortArray(CollectionType.Null)]
        public void Throws_if_shorts_are_null(IList<short> shorts)
        {
            Should.Throw<ArgumentNullException>(() => shorts.IndexOfSequence(1));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_start_is_negative(IList<short> shorts)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => shorts.IndexOfSequence(-1, 10, 1));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_count_is_negative(IList<short> shorts)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => shorts.IndexOfSequence(0, -1, 1));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_sequence_is_null(IList<short> shorts)
        {
            Should.Throw<ArgumentNullException>(() => shorts.IndexOfSequence(0, 100, null));
        }

        [Theory]
        [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {1, 2}, 0)]
        [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {4, 5}, 3)]
        [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {6}, 5)]
        public void Returns_index_of_existing_sequence(IList<short> shorts, short[] sequence, int expectedIndex)
        {
            shorts.IndexOfSequence(sequence).ShouldBe(expectedIndex);
        }

        [Theory]
        [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {2, 1})]
        [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {9})]
        [InlineData(new short[] {1, 2, 3, 4, 5, 6}, new short[] {1, 2, 4})]
        public void Returns_minus_one_if_sequence_not_found(IList<short> shorts, short[] sequence)
        {
            shorts.IndexOfSequence(sequence).ShouldBeLessThan(0);
        }

        [Theory]
        [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 2, 3 }, 1, 5, 1)]
        [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 4, 5 }, 2, 9, 3)]
        [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 6 }, 1, 100, 5)]
        public void Returns_index_of_existing_sequence_for_start_and_count(IList<short> shorts, short[] sequence, int start, int count, int expectedIndex)
        {
            shorts.IndexOfSequence(start, count, sequence).ShouldBe(expectedIndex);
        }

        [Theory]
        [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 3, 2 }, 1, 5)]
        [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 1, 2 }, 2, 9)]
        [InlineData(new short[] { 1, 2, 3, 4, 5, 6 }, new short[] { 7 }, 1, 100)]
        public void Returns_minus_one_for_start_and_count_if_sequence_not_found(IList<short> shorts, short[] sequence, int start, int count)
        {
            shorts.IndexOfSequence(start, count, sequence).ShouldBeLessThan(0);
        }
    }
}
