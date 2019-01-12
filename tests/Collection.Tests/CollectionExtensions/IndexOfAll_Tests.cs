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
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

namespace Collection.Tests.CollectionExtensions
{
    public sealed class IndexOfAll_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            // ToList is needed because the returned collection is lazy
            Should.Throw<ArgumentNullException>(() => collection.IndexOfAll(n => n % 2 == 0).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IList<int> collection)
        {
            // ToList is needed because the returned collection is lazy
            Should.Throw<ArgumentNullException>(() => collection.IndexOfAll(null).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Finds_indices_of_all_matching_elements(IList<int> collection)
        {
            IEnumerable<int> indices = collection.IndexOfAll(n => n % 2 == 0);

            indices.ShouldBe(new[] {1, 3, 5});
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_empty_enumeration_if_matching_elements_not_found(IList<int> collection)
        {
            IEnumerable<int> indices = collection.IndexOfAll(n => n > 100);

            indices.ShouldBeEmpty();
        }
    }
}