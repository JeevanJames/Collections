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
    public sealed class Random_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.Random(10).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.Empty)]
        public void Returns_empty_sequence_if_collection_is_empty(IList<int> collection)
        {
            IEnumerable<int> random = collection.Random(10).ToList();

            random.ShouldBeEmpty();
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_count_is_less_than_one(IList<int> collection)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Random(0).ToList());
            Should.Throw<ArgumentOutOfRangeException>(() => collection.Random(-1).ToList());
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Generates_random_sequence(IList<int> collection)
        {
            List<int> random = collection.Random(10).ToList();

            random.Count.ShouldBe(10);
            random.ShouldAllBe(n => collection.Any(element => element == n));
        }
    }
}