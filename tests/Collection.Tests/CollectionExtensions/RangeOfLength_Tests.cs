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

namespace Collection.Tests.CollectionExtensions
{
    public sealed class RangeOfLength_Tests
    {
        [Theory]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 2, 3, new[] {3, 4, 5})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, 4, new[] {1, 2, 3, 4})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, null, new[] {4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, null, new[] {1, 2, 3, 4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, 1, new[] {4})]
        public void Returns_iterator_for_specified_range(ICollection<int> collection, int? start, int? end, IList<int> expectedResult)
        {
            IEnumerable<int> result = collection.RangeOfLength(start, end);

            result.ShouldBe(expectedResult);
        }
    }
}