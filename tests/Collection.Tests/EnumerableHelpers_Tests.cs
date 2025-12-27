// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

#if EXPLICIT
using Collections.Net;
using Collections.Net.Extensions.Numeric;
#endif

using Shouldly;

using Xunit;
using Xunit.Abstractions;

namespace Collection.Tests;

public sealed class EnumerableHelpersTests
{
    private readonly ITestOutputHelper _output;

    public EnumerableHelpersTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(300, byte.MinValue, byte.MaxValue)]
    [InlineData(50, byte.MinValue, 9)]
    [InlineData(40, 201, byte.MaxValue)]
    [InlineData(300, 1, 6)]
    public void Returns_random_bytes(int count, byte min, byte max)
    {
        List<byte> bytes = EnumerableHelpers.CreateRandomBytes(count, min, max).ToList();

        bytes.ShouldNotBeNull();
        bytes.Count.ShouldBe(count);
        bytes.ShouldAllBe(b => b >= min && b <= max);

        _output.WriteLine(bytes.ToString(", "));
    }

    [Theory]
    [InlineData(300, int.MinValue, int.MaxValue)]
    [InlineData(50, int.MinValue, 9)]
    [InlineData(40, 201, int.MaxValue)]
    [InlineData(40, -3, 4)]
    public void Returns_random_ints(int count, int min, int max)
    {
        List<int> bytes = EnumerableHelpers.CreateRandomInts(count, min, max).ToList();

        bytes.ShouldNotBeNull();
        bytes.Count.ShouldBe(count);
        bytes.ShouldAllBe(i => i >= min && i <= max);

        _output.WriteLine(bytes.ToString(", "));
    }

    [Theory]
    [InlineData(3, 3, 1, new[] { 3 })]
    [InlineData(3, 3, -1, new[] { 3 })]
    [InlineData(3, 8, 1, new[] { 3, 4, 5, 6, 7, 8 })]
    [InlineData(-3, 2, 1, new[] { -3, -2, -1, 0, 1, 2 })]
    [InlineData(16, 10, -1, new[] { 16, 15, 14, 13, 12, 11, 10 })]
    [InlineData(-3, -10, -1, new[] { -3, -4, -5, -6, -7, -8, -9, -10 })]
    [InlineData(2, 10, 2, new[] { 2, 4, 6, 8, 10 })]
    public void Generates_range(int start, int end, int increment, int[] expected)
    {
        int[] result = EnumerableHelpers.Range(start, end, increment).ToArray();
        _output.WriteLine(string.Join(", ", result));

        result.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(int.MaxValue - 5, int.MaxValue, 1, 6)]
    [InlineData(int.MinValue + 5, int.MinValue, -1, 6)]
    public void Generates_ranges_for_max_min_values(int start, int end, int increment, int expectedLength)
    {
        int[] result = EnumerableHelpers.Range(start, end, increment).ToArray();

        result.Length.ShouldBe(expectedLength);
        result[^1].ShouldBe(end);
    }

    [Theory]
    [InlineData(3, 8, -1)]
    [InlineData(8, 3, 1)]
    [InlineData(3, 8, 0)]
    public void Invalid_ranges(int start, int end, int increment)
    {
        Should.Throw<ArgumentException>(() => EnumerableHelpers.Range(start, end, increment).ToArray());
    }
}
