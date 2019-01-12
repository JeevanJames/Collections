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
    public sealed class CollectionExtensions_RemoveLast_Tests
    {
        [Fact]
        public void Throws_if_collection_is_null()
        {
            IList<int> collection = null;

            Should.Throw<ArgumentNullException>(() => collection.RemoveLast(n => n % 2 == 0));
        }

        [Fact]
        public void Throws_if_predicate_is_null()
        {
            IList<int> collection = new[] {1, 2};
            Should.Throw<ArgumentNullException>(() => collection.RemoveLast(null));
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new [] {1, 3, 5, 7, 9})]
        public void Returns_false_if_matching_element_not_found(IList<int> collection)
        {
            collection.RemoveLast(n => n % 2 == 0).ShouldBeFalse();
        }

        [Fact]
        public void Removes_last_matching_element()
        {
            IList<int> collection = new List<int> {1, 2, 3, 4, 5, 6};

            bool removed = collection.RemoveLast(n => n % 2 == 0);

            removed.ShouldBeTrue();
            collection.Count.ShouldBe(5);
            collection.ShouldContain(n => n == 6, 0);
        }
    }
}