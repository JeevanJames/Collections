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

#if EXPLICIT
using Collections.Net.Dictionary;
#endif

namespace Collection.Tests.DictionaryExtensions
{
    public sealed class AddOrUpdate_Tests
    {
        [Theory, Dictionary(CollectionType.Null)]
        public void Throws_if_dictionary_is_null(IDictionary<string, int> dictionary)
        {
            Should.Throw<ArgumentNullException>(() => dictionary.AddOrUpdate("Seven", 7));
        }

        [Theory, Dictionary(CollectionType.NumbersOneToSix)]
        public void Adds_new_entry(IDictionary<string, int> dictionary)
        {
            dictionary.AddOrUpdate("Seven", 7);

            dictionary.Count.ShouldBe(7);
            dictionary["Seven"].ShouldBe(7);
        }

        [Theory, Dictionary(CollectionType.NumbersOneToSix)]
        public void Updates_existing_entry(IDictionary<string, int> dictionary)
        {
            dictionary.AddOrUpdate("Two", 10);

            dictionary.Count.ShouldBe(6);
            dictionary["Two"].ShouldBe(10);
        }
    }
}
