﻿#region --- License & Copyright Notice ---
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
    public sealed class CollectionExtensions_LastIndexOf_Tests
    {
        [Theory, SpecialCollection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.LastIndexOf(n => n % 2 == 0));
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.LastIndexOf(null));
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Finds_index_of_existing_element(IList<int> collection)
        {
            int index = collection.LastIndexOf(n => n % 2 == 0);

            index.ShouldBe(5);
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Returns_negative_number_for_nonexistent_element(IList<int> collection)
        {
            int index = collection.LastIndexOf(n => n == 100);

            index.ShouldBeLessThan(0);
        }
    }
}
