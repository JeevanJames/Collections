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

namespace Collection.Tests.CollectionExtensions
{
    public sealed class InsertRange_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_list_is_null(IList<int> list)
        {
            Should.Throw<ArgumentNullException>(() => list.InsertRange(1, 7, 8, 9));
            Should.Throw<ArgumentNullException>(() => list.InsertRange(1, new[] {7, 8, 9}));
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Adds_range_if_index_is_greater_or_equal_to_list_count(IList<int> list)
        {
            list.InsertRange(100, new [] {7, 8, 9});
            list.ShouldBe(new [] {1, 2, 3, 4, 5, 6, 7, 8, 9});

            list.InsertRange(list.Count, new [] {10, 11, 12});
            list.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12});
        }
    }
}