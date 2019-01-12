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
    public sealed class CollectionExtensions_WhereNot_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.WhereNot(n => n % 2 == 0));
            Should.Throw<ArgumentNullException>(() => collection.WhereNot((n, i) => n % 2 == 0));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.WhereNot((Func<int, bool>)null));
            Should.Throw<ArgumentNullException>(() => collection.WhereNot((Func<int, int, bool>)null));
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Filters_collection_based_on_inverse_of_predicate(IEnumerable<int> collection)
        {
            IEnumerable<int> oddNumbers = collection.WhereNot(n => n % 2 == 0);
            IEnumerable<int> filteredNumbers = collection.WhereNot((n, i) => n % 2 == 0 || i == 0);

            oddNumbers.ShouldBe(new [] {1, 3, 5});
            filteredNumbers.ShouldBe(new [] {3, 5});
        }
    }
}