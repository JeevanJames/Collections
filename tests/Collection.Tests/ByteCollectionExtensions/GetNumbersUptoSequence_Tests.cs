// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ByteCollectionExtensions
{
    public sealed class GetNumbersUptoSequence_Tests
    {
        [Theory]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {4, 5, 6}, 0, new byte[] {1, 2, 3})]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {4, 5, 6}, 1, new byte[] {2, 3})]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {1, 2, 3}, 0, new byte[0])]
        public void Returns_bytes_upto_sequence(byte[] bytes, byte[] sequence, int start, byte[] expectedResult)
        {
            IList<byte>? result = bytes.GetNumbersUptoSequence(start, sequence);

            result.ShouldNotBeNull().ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {7, 8}, 0)]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {7, 8}, 2)]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {1, 2, 3}, 1)]
        public void Returns_null_if_sequence_not_found(byte[] bytes, byte[] sequence, int start)
        {
            IList<byte>? result = bytes.GetNumbersUptoSequence(start, sequence);

            result.ShouldBeNull();
        }
    }
}
