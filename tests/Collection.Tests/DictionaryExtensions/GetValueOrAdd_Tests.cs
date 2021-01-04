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

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Dictionary;
#endif

namespace Collection.Tests.DictionaryExtensions
{
    public sealed class GetValueOrAdd_Tests
    {
        [Theory, Dictionary(CollectionType.Null)]
        public void Throws_if_dictionary_is_null(IDictionary<string, int> dictionary)
        {
            Should.Throw<ArgumentNullException>(() => dictionary.GetValueOrAdd("One", 2));
            Should.Throw<ArgumentNullException>(() => dictionary.GetValueOrAdd("One", key => 2));
            Should.Throw<ArgumentNullException>(() => dictionary.GetValueOrAdd("One", (key, dict) => 2));
        }

        [Theory, Dictionary(CollectionType.NonEmpty)]
        public void Throws_if_value_getter_is_null(IDictionary<string, int> dictionary)
        {
            Should.Throw<ArgumentNullException>(() => dictionary.GetValueOrAdd("One", (Func<string, int>) null));
            Should.Throw<ArgumentNullException>(() => dictionary.GetValueOrAdd("One",
                (Func<string, IDictionary<string, int>, int>) null));
        }

        [Theory, Dictionary(CollectionType.NumbersOneToSix)]
        public void Gets_value_for_existing_key(IDictionary<string, int> dictionary)
        {
            int value = dictionary.GetValueOrAdd("Two", 3);

            value.ShouldBe(2);
            dictionary.Count.ShouldBe(6);

            value = dictionary.GetValueOrAdd("Two", key => key.Length);

            value.ShouldBe(2);
            dictionary.Count.ShouldBe(6);

            value = dictionary.GetValueOrAdd("Two", (key, dict) => key.Length * dict.Count);

            value.ShouldBe(2);
            dictionary.Count.ShouldBe(6);
        }

        [Theory, Dictionary(CollectionType.NumbersOneToSix)]
        public void Adds_value_for_nonexisting_key(IDictionary<string, int> dictionary)
        {
            int value = dictionary.GetValueOrAdd("Seven", 10);

            value.ShouldBe(10);
            dictionary.Count.ShouldBe(7);
        }

        [Theory, Dictionary(CollectionType.NumbersOneToSix)]
        public void Adds_value_for_nonexisting_value_using_func1(IDictionary<string, int> dictionary)
        {
            int value = dictionary.GetValueOrAdd("Seven", key => key.Length * 2);

            value.ShouldBe(10);
            dictionary.Count.ShouldBe(7);
        }

        [Theory, Dictionary(CollectionType.NumbersOneToSix)]
        public void Adds_value_for_nonexisting_value_using_func2(IDictionary<string, int> dictionary)
        {
            int value = dictionary.GetValueOrAdd("Seven", (key, dict) => key.Length * dict.Count);

            value.ShouldBe(30);
            dictionary.Count.ShouldBe(7);
        }
    }
}
