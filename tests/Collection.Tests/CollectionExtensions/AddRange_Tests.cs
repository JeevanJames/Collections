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
using Collections.Net.Collection;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class AddRange_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(ICollection<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.AddRange(7, 8, 9));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new[] {7, 8, 9}, predicate: n => n % 2 == 0));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new[] {"7", "8", "9"}, converter: int.Parse));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new[] {"7", "8", "9"}, s => s == "8", int.Parse));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Do_nothing_if_items_is_null(ICollection<int> collection)
        {
            Should.NotThrow(() => collection.AddRange(null));
            Should.NotThrow(() => collection.AddRange(null, predicate: n => n % 2 == 0));
            Should.NotThrow(() => collection.AddRange<int, string>(null, converter: int.Parse));
            Should.NotThrow(() => collection.AddRange<int, string>(null, s => s == "8", int.Parse));
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Adds_items_to_the_collection(ICollection<int> collection)
        {
            collection.AddRange(7, 8, 9);

            collection.Count.ShouldBe(9);
            collection.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(ICollection<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new [] {7, 8, 9}, predicate: null));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new [] {"7", "8", "9"}, null, int.Parse));
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Adds_only_even_numbers_to_collection(ICollection<int> collection)
        {
            collection.AddRange(new [] {7, 8, 9}, predicate: n => n % 2 == 0);

            collection.Count.ShouldBe(7);
            collection.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 8});
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_converter_is_null(ICollection<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new [] {7, 8, 9}, converter: null));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new [] {7, 8, 9}, n => n % 2 == 0, null));
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Adds_3_strings_to_collection_after_converting_to_ints(ICollection<int> collection)
        {
            collection.AddRange(new [] {"7", "8", "9"}, converter: int.Parse);

            collection.Count.ShouldBe(9);
            collection.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Adds_one_matching_item_after_converting_to_ints(ICollection<int> collection)
        {
            collection.AddRange(new [] {"7", "8", "9"}, n => n == "8", int.Parse);

            collection.Count.ShouldBe(7);
            collection.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 8});
        }
    }
}
