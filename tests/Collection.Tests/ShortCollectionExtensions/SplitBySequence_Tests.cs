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

namespace Collection.Tests.ShortCollectionExtensions
{
    public sealed class SplitBySequence_Tests
    {
        [Theory, ShortArray(CollectionType.Null)]
        public void Throws_if_shorts_is_null(short[] shorts)
        {
            Should.Throw<ArgumentNullException>(() => shorts.SplitBySequence(1, 2));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_start_is_negative(short[] shorts)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => shorts.SplitBySequence(-1, 100, 1, 2));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_count_is_negative(short[] shorts)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => shorts.SplitBySequence(0, -1, 1, 2));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_sequence_is_null_or_empty(short[] shorts)
        {
            Should.Throw<ArgumentNullException>(() => shorts.SplitBySequence(null));
            Should.Throw<ArgumentException>(() => shorts.SplitBySequence());
        }

        [Theory, MemberData(nameof(Splits_shorts_on_existing_sequence_Data))]
        public void Splits_shorts_on_existing_sequence(short[] shorts, short[] sequence, short[][] expectedResult)
        {
            short[][] result = shorts.SplitBySequence(sequence);

            result.ShouldBe(expectedResult);
        }

        public static IEnumerable<object[]> Splits_shorts_on_existing_sequence_Data()
        {
            yield return new object[]
            {
                new short[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new short[] {4, 1},
                new short[][]
                {
                    new short[] {1, 2, 3},
                    new short[] {2, 3},
                    new short[] {2, 3, 4},
                }
            };

            yield return new object[]
            {
                new short[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new short[] {4},
                new short[][]
                {
                    new short[] {1, 2, 3},
                    new short[] {1, 2, 3},
                    new short[] {1, 2, 3},
                    new short[0], 
                }
            };

            yield return new object[]
            {
                new short[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new short[] {1},
                new short[][]
                {
                    new short[0], 
                    new short[] {2, 3, 4},
                    new short[] {2, 3, 4},
                    new short[] {2, 3, 4},
                }
            };
        }

        [Theory]
        [InlineData(new short[] {1, 2, 3, 4, 1, 2, 3, 4}, new short[] {9})]
        [InlineData(new short[] {1, 2, 3, 4, 1, 2, 3, 4}, new short[] {8, 9})]
        public void Splits_shorts_on_non_existing_sequence(short[] shorts, short[] sequence)
        {
            short[][] result = shorts.SplitBySequence(sequence);

            result.Length.ShouldBe(1);
            result[0].ShouldBeSameAs(shorts);
            result.ShouldBe(new short[][] {shorts});
        }

        [Fact]
        public void Does_not_error_if_count_is_too_large()
        {
            short[] shorts = {1, 2, 3, 4, 1, 2, 3, 4};
            short[] sequence = {3};

            Should.NotThrow(() => shorts.SplitBySequence(0, 100, sequence));
        }

        //TODO: Add tests for non-standard start and count values.
    }
}
