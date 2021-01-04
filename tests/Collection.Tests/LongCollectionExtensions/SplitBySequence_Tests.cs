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

namespace Collection.Tests.LongCollectionExtensions
{
    public sealed class SplitBySequence_Tests
    {
        [Theory, LongArray(CollectionType.Null)]
        public void Throws_if_longs_is_null(long[] longs)
        {
            Should.Throw<ArgumentNullException>(() => longs.SplitBySequence(1, 2));
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Throws_if_start_is_negative(long[] longs)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => longs.SplitBySequence(-1, 100, 1, 2));
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Throws_if_count_is_negative(long[] longs)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => longs.SplitBySequence(0, -1, 1, 2));
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Throws_if_sequence_is_null_or_empty(long[] longs)
        {
            Should.Throw<ArgumentNullException>(() => longs.SplitBySequence(null));
            Should.Throw<ArgumentException>(() => longs.SplitBySequence());
        }

        [Theory, MemberData(nameof(Splits_longs_on_existing_sequence_Data))]
        public void Splits_longs_on_existing_sequence(long[] longs, long[] sequence, long[][] expectedResult)
        {
            long[][] result = longs.SplitBySequence(sequence);

            result.ShouldBe(expectedResult);
        }

        public static IEnumerable<object[]> Splits_longs_on_existing_sequence_Data()
        {
            yield return new object[]
            {
                new long[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new long[] {4, 1},
                new long[][]
                {
                    new long[] {1, 2, 3},
                    new long[] {2, 3},
                    new long[] {2, 3, 4},
                }
            };

            yield return new object[]
            {
                new long[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new long[] {4},
                new long[][]
                {
                    new long[] {1, 2, 3},
                    new long[] {1, 2, 3},
                    new long[] {1, 2, 3},
                    new long[0], 
                }
            };

            yield return new object[]
            {
                new long[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new long[] {1},
                new long[][]
                {
                    new long[0], 
                    new long[] {2, 3, 4},
                    new long[] {2, 3, 4},
                    new long[] {2, 3, 4},
                }
            };
        }

        [Theory]
        [InlineData(new long[] {1, 2, 3, 4, 1, 2, 3, 4}, new long[] {9})]
        [InlineData(new long[] {1, 2, 3, 4, 1, 2, 3, 4}, new long[] {8, 9})]
        public void Splits_longs_on_non_existing_sequence(long[] longs, long[] sequence)
        {
            long[][] result = longs.SplitBySequence(sequence);

            result.Length.ShouldBe(1);
            result[0].ShouldBeSameAs(longs);
            result.ShouldBe(new long[][] {longs});
        }

        [Fact]
        public void Does_not_error_if_count_is_too_large()
        {
            long[] longs = {1, 2, 3, 4, 1, 2, 3, 4};
            long[] sequence = {3};

            Should.NotThrow(() => longs.SplitBySequence(0, 100, sequence));
        }

        //TODO: Add tests for non-standard start and count values.
    }
}
