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
    public sealed class CollectionExtensions_ShuffleInplace_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ShuffleInplace());
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Throws_if_iterations_is_less_than_one(IList<int> collection)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => collection.ShuffleInplace(0));
            Should.Throw<ArgumentOutOfRangeException>(() => collection.ShuffleInplace(-1));
        }

        [Fact]
        public void Shuffles_collection_in_place()
        {
            int[] collection = {1, 2, 3, 4, 5, 6, 7, 8};
            
            collection.ShuffleInplace();

            collection.ShouldNotBe(new [] {1, 2, 3, 4, 5, 6, 7, 8});
        }
    }
}