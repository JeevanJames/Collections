// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

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
}
