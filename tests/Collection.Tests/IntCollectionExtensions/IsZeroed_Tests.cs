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
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions
{
    public sealed class IsZeroed_Tests
    {
        [Theory, IntArray(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.IsZeroed());
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new int[] {0, 0, 0, 0})]
        public void Returns_true_if_collection_is_zeroed(IList<int> collection)
        {
            collection.IsZeroed().ShouldBeTrue();
        }

        [Fact]
        public void Returns_false_if_collection_contains_nonzeroes()
        {
            IList<int> collection = new List<int> {0, 1, 0, 0};

            collection.IsZeroed().ShouldBeFalse();
        }
    }
}
