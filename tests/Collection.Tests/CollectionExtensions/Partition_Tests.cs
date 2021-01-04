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
using System.Linq;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Enumerable;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class Partition_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.Partition(n => n % 2 == 0));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.Partition(null));
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_matches_and_mismatches(IList<int> collection)
        {
            (IEnumerable<int> matches, IEnumerable<int> mismatches) = collection.Partition(n => n % 2 == 0);

            matches.ShouldBe(new[] {2, 4, 6});
            mismatches.ShouldBe(new[] {1, 3, 5});
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_matches_where_all_match(IList<int> collection)
        {
            (IEnumerable<int> matches, IEnumerable<int> mismatches) = collection.Partition(n => n >= 1 && n <= 6);

            matches.ShouldBe(new[] {1, 2, 3, 4, 5, 6});
            mismatches.Any().ShouldBeFalse();
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_matches_where_none_match(IList<int> collection)
        {
            (IEnumerable<int> matches, IEnumerable<int> mismatches) = collection.Partition(n => n == 10);

            matches.Any().ShouldBeFalse();
            mismatches.ShouldBe(new[] { 1, 2, 3, 4, 5, 6 });
        }
    }
}
