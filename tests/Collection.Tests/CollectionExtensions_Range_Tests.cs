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

namespace Collection.Tests
{
    public sealed class CollectionExtensions_Range_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(ICollection<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.Range(0, 1));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_start_is_out_of_range(ICollection<int> collection)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Range(-1, null));
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Range(collection.Count, null));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_end_is_less_than_start_or_out_of_range(ICollection<int> collection)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Range(2, 1));
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Range(2, collection.Count));
        }

        [Theory]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 2, 4, new[] {3, 4, 5})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, 3, new[] {1, 2, 3, 4})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, null, new[] {4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, null, null, new[] {1, 2, 3, 4, 5, 6})]
        [InlineData(new[] {1, 2, 3, 4, 5, 6}, 3, 3, new[] {4})]
        public void Returns_iterator_for_specified_range(ICollection<int> collection, int? start, int? end, IList<int> expectedResult)
        {
            IEnumerable<int> result = collection.Range(start, end);

            result.ShouldBe(expectedResult);
        }
    }
}