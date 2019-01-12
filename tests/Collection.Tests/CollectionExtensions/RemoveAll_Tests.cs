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
using Shouldly;
using Xunit;

namespace Collection.Tests.CollectionExtensions
{
    public sealed class RemoveAll_Tests
    {
        [Fact]
        public void Throws_if_collection_is_null()
        {
            IList<int> collection = null;

            Should.Throw<ArgumentNullException>(() => collection.RemoveAll(n => n % 2 == 0));
        }

        [Fact]
        public void Throws_if_predicate_is_null()
        {
            IList<int> collection = new List<int> {1, 2, 3, 4, 5, 6};

            Should.Throw<ArgumentNullException>(() => collection.RemoveAll(null));
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new[] {1, 3, 5, 7, 9})]
        public void Returns_zero_if_no_matching_elements_found(IList<int> collection)
        {
            collection.RemoveAll(n => n % 2 == 0).ShouldBe(0);
        }

        [Fact]
        public void Removes_all_matching_elements()
        {
            IList<int> collection = new List<int> {1, 2, 3, 4, 5, 6};

            int removedCount = collection.RemoveAll(n => n % 2 == 0);

            removedCount.ShouldBe(3);
            collection.Count.ShouldBe(3);
            collection.ShouldAllBe(n => n % 2 != 0);
        }
    }
}