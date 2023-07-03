// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

#if EXPLICIT
using Collections.Net.EnumerablesEx;
#endif
using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

namespace Collection.Tests.CollectionExtensions;

public sealed class ChunkTests
{
    [Theory, DataAttributes.Collection(CollectionType.Null)]
    public void Throws_if_collection_is_empty(IEnumerable<int> collection)
    {
        Should.Throw<ArgumentNullException>(() => collection.ChunkEx(2).ToList());
    }

    [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
    public void Throws_if_chunksize_is_zero_or_negative(IEnumerable<int> collection)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => collection.ChunkEx(0).ToList());
        Should.Throw<ArgumentOutOfRangeException>(() => collection.ChunkEx(-1).ToList());
    }

    [Theory, MemberData(nameof(Returns_chunks_of_the_specified_size_Data))]
    public void Returns_chunks_of_the_specified_size(IEnumerable<int> collection, int chunkSize,
        IEnumerable<int[]> expectedResult)
    {
        IEnumerable<int[]> result = collection.ChunkEx(chunkSize);

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
