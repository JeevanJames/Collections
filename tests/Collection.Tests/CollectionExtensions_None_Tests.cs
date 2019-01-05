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
    public sealed class CollectionExtensions_None_Tests
    {
        private static readonly int[] OddNumbers = { 1, 3, 5, 7, 9, 11 };
        private static readonly int[] MixedNumbers = { 1, 2, 3, 5, 7, 8, 9, 11 };

        [Fact]
        public void Throws_if_source_is_null()
        {
            IEnumerable<int> numbers = null;

            Should.Throw<ArgumentNullException>(() => numbers.None(n => n % 2 == 0));
        }

        [Fact]
        public void Does_not_throw_if_source_is_empty()
        {
            IEnumerable<int> numbers = new int[0];

            Should.NotThrow(() => numbers.None(n => n % 2 == 0));
        }

        [Fact]
        public void Throws_if_predicate_is_null()
        {
            Should.Throw<ArgumentNullException>(() => OddNumbers.None(null));
        }

        [Fact]
        public void Returns_true_if_no_item_matches()
        {
            OddNumbers.None(n => n % 2 == 0).ShouldBeTrue();
        }

        [Fact]
        public void Returns_false_if_any_item_does_not_match()
        {
            MixedNumbers.None(n => n % 2 == 0).ShouldBeFalse();
        }
    }
}
