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
    public sealed class CollectionExtensions_Fill_Tests
    {
        [Fact]
        public void Throws_if_collection_is_null()
        {
            IList<int> collection = null;

            Should.Throw<ArgumentNullException>(() => collection.Fill(0));
            Should.Throw<ArgumentNullException>(() => collection.Fill(n => n * 2));
        }

        [Fact]
        public void Does_not_do_anything_if_collection_is_empty()
        {
            IList<int> collection = new List<int>(0);

            Should.NotThrow(() => collection.Fill(0));
            Should.NotThrow(() => collection.Fill(n => n * 2));
        }

        [Fact]
        public void Fills_collection_with_value()
        {
            IList<int> collection = new int[6];

            collection.Fill(5);

            collection.Count.ShouldBe(6);
            collection.ShouldBe(new[] {5, 5, 5, 5, 5, 5});
        }

        [Fact]
        public void Throws_if_generator_is_null()
        {
            IList<int> collection = new int[6];

            Should.Throw<ArgumentNullException>(() => collection.Fill(null));
        }

        [Fact]
        public void Fills_collection_with_generated_values()
        {
            IList<int> collection = new int[6];

            collection.Fill(n => n * 2);

            collection.Count.ShouldBe(6);
            collection.ShouldBe(new[] {0, 2, 4, 6, 8, 10});
        }
    }
}