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
using System.Linq;
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Enumerable;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class Chunk_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_empty(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.Chunk(2).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_chunksize_is_zero_or_negative(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Chunk(0).ToList());
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Chunk(-1).ToList());
        }

        [Theory, MemberData(nameof(Returns_chunks_of_the_specified_size_Data))]
        public void Returns_chunks_of_the_specified_size(IEnumerable<int> collection, int chunkSize,
            IEnumerable<int[]> expectedResult)
        {
            IEnumerable<int[]> result = collection.Chunk(chunkSize);

            result.ShouldBe(expectedResult);
        }

        public static IEnumerable<object[]> Returns_chunks_of_the_specified_size_Data()
        {
            yield return new object[]
            {
                new[] {1, 2, 3, 4, 5, 6, 7, 8},
                1,
                new int[][]
                {
                    new[] {1},
                    new[] {2},
                    new[] {3},
                    new[] {4},
                    new[] {5},
                    new[] {6},
                    new[] {7},
                    new[] {8},
                }
            };

            yield return new object[]
            {
                new[] {1, 2, 3, 4, 5, 6, 7, 8},
                2,
                new int[][]
                {
                    new[] {1, 2},
                    new[] {3, 4},
                    new[] {5, 6},
                    new[] {7, 8},
                }
            };

            yield return new object[]
            {
                new[] {1, 2, 3, 4, 5, 6, 7, 8},
                3,
                new int[][]
                {
                    new[] {1, 2, 3},
                    new[] {4, 5, 6},
                    new[] {7, 8},
                }
            };

            yield return new object[]
            {
                new[] {1, 2, 3, 4, 5, 6, 7, 8},
                7,
                new int[][]
                {
                    new[] {1, 2, 3, 4, 5, 6, 7},
                    new[] {8},
                }
            };

            yield return new object[]
            {
                new[] {1, 2, 3, 4, 5, 6, 7, 8},
                8,
                new int[][]
                {
                    new[] {1, 2, 3, 4, 5, 6, 7, 8},
                }
            };

            yield return new object[]
            {
                new[] {1, 2, 3, 4, 5, 6, 7, 8},
                10,
                new int[][]
                {
                    new[] {1, 2, 3, 4, 5, 6, 7, 8},
                }
            };
        }
    }
}
