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

namespace Collection.Tests.IntCollectionExtensions
{
    public sealed class GetNumbersUptoSequence_Tests
    {
        [Theory]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {4, 5, 6}, 0, new int[] {1, 2, 3})]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {4, 5, 6}, 1, new int[] {2, 3})]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {1, 2, 3}, 0, new int[0])]
        public void Returns_ints_upto_sequence(int[] ints, int[] sequence, int start, int[] expectedResult)
        {
            IList<int> result = ints.GetNumbersUptoSequence(start, sequence);

            result.ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {7, 8}, 0)]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {7, 8}, 2)]
        [InlineData(new int[] {1, 2, 3, 4, 5, 6}, new int[] {1, 2, 3}, 1)]
        public void Returns_null_if_sequence_not_found(int[] ints, int[] sequence, int start)
        {
            IList<int> result = ints.GetNumbersUptoSequence(start, sequence);

            result.ShouldBeNull();
        }
    }
}
