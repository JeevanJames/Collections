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

namespace Collection.Tests
{
    public sealed class ByteCollectionExtensions_SplitBySequence_Tests
    {
        [Theory, ByteArray(CollectionType.Null)]
        public void Throws_if_bytes_is_null(byte[] bytes)
        {
            Should.Throw<ArgumentNullException>(() => bytes.SplitBySequence(1, 2));
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Throws_if_start_is_negative(byte[] bytes)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => bytes.SplitBySequence(-1, 100, 1, 2));
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Throws_if_count_is_negative(byte[] bytes)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => bytes.SplitBySequence(0, -1, 1, 2));
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Throws_if_sequence_is_null_or_empty(byte[] bytes)
        {
            Should.Throw<ArgumentNullException>(() => bytes.SplitBySequence(null));
            Should.Throw<ArgumentException>(() => bytes.SplitBySequence());
        }

        [Theory, MemberData(nameof(Splits_bytes_on_existing_sequence_Data))]
        public void Splits_bytes_on_existing_sequence(byte[] bytes, byte[] sequence, byte[][] expectedResult)
        {
            byte[][] result = bytes.SplitBySequence(sequence);

            result.ShouldBe(expectedResult);
        }

        public static IEnumerable<object[]> Splits_bytes_on_existing_sequence_Data()
        {
            yield return new object[]
            {
                new byte[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new byte[] {4, 1},
                new byte[][]
                {
                    new byte[] {1, 2, 3},
                    new byte[] {2, 3},
                    new byte[] {2, 3, 4},
                }
            };

            yield return new object[]
            {
                new byte[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new byte[] {4},
                new byte[][]
                {
                    new byte[] {1, 2, 3},
                    new byte[] {1, 2, 3},
                    new byte[] {1, 2, 3},
                    new byte[0], 
                }
            };

            yield return new object[]
            {
                new byte[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4},
                new byte[] {1},
                new byte[][]
                {
                    new byte[0], 
                    new byte[] {2, 3, 4},
                    new byte[] {2, 3, 4},
                    new byte[] {2, 3, 4},
                }
            };
        }

        [Theory]
        [InlineData(new byte[] {1, 2, 3, 4, 1, 2, 3, 4}, new byte[] {9})]
        [InlineData(new byte[] {1, 2, 3, 4, 1, 2, 3, 4}, new byte[] {8, 9})]
        public void Splits_bytes_on_non_existing_sequence(byte[] bytes, byte[] sequence)
        {
            byte[][] result = bytes.SplitBySequence(sequence);

            result.Length.ShouldBe(1);
            result[0].ShouldBeSameAs(bytes);
            result.ShouldBe(new byte[][] {bytes});
        }

        [Fact]
        public void Does_not_error_if_count_is_too_large()
        {
            byte[] bytes = {1, 2, 3, 4, 1, 2, 3, 4};
            byte[] sequence = {3};

            Should.NotThrow(() => bytes.SplitBySequence(0, 100, sequence));
        }

        //TODO: Add tests for non-standard start and count values.
    }
}