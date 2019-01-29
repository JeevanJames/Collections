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

using System.Collections.Generic;
using Shouldly;
using Xunit;

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
            IList<byte> result = bytes.GetNumbersUptoSequence(start, sequence);

            result.ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {7, 8}, 0)]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {7, 8}, 2)]
        [InlineData(new byte[] {1, 2, 3, 4, 5, 6}, new byte[] {1, 2, 3}, 1)]
        public void Returns_null_if_sequence_not_found(byte[] bytes, byte[] sequence, int start)
        {
            IList<byte> result = bytes.GetNumbersUptoSequence(start, sequence);

            result.ShouldBeNull();
        }
    }
}