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
    public sealed class CollectionExtensions_ToArray_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToArray<int, string>(n => n.ToString()));
            Should.Throw<ArgumentNullException>(() => collection.ToArray<int>(n => n % 2 == 0));
            Should.Throw<ArgumentNullException>(() => collection.ToArray(n => n % 2 == 0, n => n * 2));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToArray<int>(null));
            Should.Throw<ArgumentNullException>(() => collection.ToArray(null, n => n.ToString()));
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_converter_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToArray<int, string>((Func<int, string>) null));
            Should.Throw<ArgumentNullException>(() => collection.ToArray(n => n % 2 == 0, (Func<int, string>) null));
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Converts_int_array_to_string_array(IEnumerable<int> collection)
        {
            string[] converted = collection.ToArray(n => n.ToString());

            converted.Length.ShouldBe(6);
            converted.ShouldBe(new[] {"1", "2", "3", "4", "5", "6"});
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_array_of_even_numbers(IEnumerable<int> collection)
        {
            int[] result = collection.ToArray(n => n % 2 == 0);

            result.Length.ShouldBe(3);
            result.ShouldBe(new[] { 2, 4, 6 });
        }

        [Theory, DataAttributes.Collection(CollectionType.NumbersOneToSix)]
        public void Returns_array_of_even_numbers_strings(IEnumerable<int> collection)
        {
            string[] result = collection.ToArray(n => n % 2 == 0, n => n.ToString());

            result.Length.ShouldBe(3);
            result.ShouldBe(new[] { "2", "4", "6" });
        }
    }
}