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
using System.Linq;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_IsEmpty_Tests
    {
        [Fact]
        public void Throws_if_collection_is_null()
        {
            IEnumerable<int> collection = null;
            Should.Throw<ArgumentNullException>(() => collection.IsEmpty());
        }

        [Fact]
        public void Returns_true_if_collection_is_empty()
        {
            IEnumerable<int> collection = new int[0];
            collection.IsEmpty().ShouldBeTrue();
        }

        [Theory, MemberData(nameof(NotEmptyCollections))]
        public void Returns_false_if_collection_is_not_empty(IEnumerable<int> collection)
        {
            collection.IsEmpty().ShouldBeFalse();
        }

        public static IEnumerable<object[]> NotEmptyCollections()
        {
            yield return new object[] {Enumerable.Range(1, 5)};
            yield return new object[] {new[] {1, 2, 3, 4, 5}};
        }
    }
}