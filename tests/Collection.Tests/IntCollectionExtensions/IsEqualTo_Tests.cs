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
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions
{
    public sealed class IsEqualTo_Tests
    {
        [Theory]
        [InlineData(new int[0])]
        [InlineData(new int[] {1, 2, 3})]
        public void Returns_true_for_the_same_reference(IList<int> ints)
        {
            ints.IsEqualTo(ints).ShouldBeTrue();
        }

        [Theory]
        [InlineData(new int[0], null)]
        [InlineData(new int[] {1, 2}, null)]
        [InlineData(null, new int[0])]
        [InlineData(null, new int[] {1, 2})]
        public void Returns_false_if_any_collection_is_null(IList<int> ints1, IList<int> ints2)
        {
            ints1.IsEqualTo(ints2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new int[0], new int[] {1, 2})]
        [InlineData(new int[] {1, 2}, new int[0])]
        public void Returns_false_for_collections_of_different_lengths(IList<int> ints1, IList<int> ints2)
        {
            ints1.IsEqualTo(ints2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new int[] {2, 1}, new int[] {1, 2})]
        [InlineData(new int[] {1, 2, 3, 4}, new int[] {1, 2, 4, 3})]
        public void Returns_false_for_collections_of_different_content(IList<int> ints1, IList<int> ints2)
        {
            ints1.IsEqualTo(ints2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new int[0], new int[0])]
        [InlineData(new int[] {1, 2}, new int[] {1, 2})]
        [InlineData(new int[] {1, 2, 3, 4}, new int[] {1, 2, 3, 4})]
        public void Returns_true_for_collections_of_same_content(IList<int> ints1, IList<int> ints2)
        {
            ints1.IsEqualTo(ints2).ShouldBeTrue();
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Returns_true_for_params_array(IList<int> ints)
        {
            ints.IsEqualTo(1, 2, 3, 4, 5, 6).ShouldBeTrue();
        }
    }
}
