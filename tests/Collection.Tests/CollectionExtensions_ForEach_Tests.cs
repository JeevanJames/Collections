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

namespace Collection.Tests
{
    public sealed class CollectionExtensions_ForEach_Tests
    {
        [Fact]
        public void Does_not_do_anything_is_null()
        {
            IEnumerable<int> collection = null;

            Should.NotThrow(() => collection.ForEach(n => Console.WriteLine(n)));
            Should.NotThrow(() => collection.ForEach((n, i) => Console.WriteLine(n)));
        }

        [Fact]
        public void Throws_if_action_is_null()
        {
            IEnumerable<int> collection = new[] {1, 2, 3, 4, 5, 6};

            Should.Throw<ArgumentNullException>(() => collection.ForEach((Action<int>) null));
            Should.Throw<ArgumentNullException>(() => collection.ForEach((Action<int, int>) null));
        }

        [Fact]
        public void Performs_action_on_each_element()
        {
            IEnumerable<int> collection = new[] {1, 2, 3, 4, 5, 6};

            Should.NotThrow(() => collection.ForEach(Console.WriteLine));
            Should.NotThrow(() => collection.ForEach((n, i) => Console.WriteLine(n)));
        }

        [Fact]
        public void Passes_index_of_item_in_collection()
        {
            IEnumerable<int> collection = new[] { 0, 1, 2, 3, 4, 5 };

            collection.ForEach((n, i) => n.ShouldBe(i));
        }
    }
}