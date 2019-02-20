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
    public sealed class AllOrNone_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_sequence_is_null(IEnumerable<int> sequence)
        {
            Should.Throw<ArgumentNullException>(() => sequence.AllOrNone(n => n % 2 == 0));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IEnumerable<int> sequence)
        {
            Should.Throw<ArgumentNullException>(() => sequence.AllOrNone(null));
        }

        [Theory, DataAttributes.Collection(CollectionType.Empty)]
        public void Returns_true_for_empty_sequence(IEnumerable<int> sequence)
        {
            sequence.AllOrNone(n => n % 2 == 0).ShouldBeTrue();
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_true_if_all_items_match(IEnumerable<int> sequence)
        {
            sequence.AllOrNone(n => n < 7).ShouldBeTrue();
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_true_if_no_items_match(IEnumerable<int> sequence)
        {
            sequence.AllOrNone(n => n > 7).ShouldBeTrue();
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_false_if_some_items_match_and_some_dont(IEnumerable<int> sequence)
        {
            sequence.AllOrNone(n => n % 2 == 0).ShouldBeFalse();
        }
    }
}