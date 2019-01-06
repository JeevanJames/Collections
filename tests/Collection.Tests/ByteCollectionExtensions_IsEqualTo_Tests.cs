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

using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class ByteCollectionExtensions_IsEqualTo_Tests
    {
        [Theory]
        [InlineData(new byte[0])]
        [InlineData(new byte[] {1, 2, 3})]
        public void Returns_true_for_the_same_reference(IList<byte> bytes)
        {
            bytes.IsEqualTo(bytes).ShouldBeTrue();
        }

        [Theory]
        [InlineData(new byte[0], null)]
        [InlineData(new byte[] {1, 2}, null)]
        [InlineData(null, new byte[0])]
        [InlineData(null, new byte[] {1, 2})]
        public void Returns_false_if_any_collection_is_null(IList<byte> bytes1, IList<byte> bytes2)
        {
            bytes1.IsEqualTo(bytes2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new byte[0], new byte[] {1, 2})]
        [InlineData(new byte[] {1, 2}, new byte[0])]
        public void Returns_false_for_collections_of_different_lengths(IList<byte> bytes1, IList<byte> bytes2)
        {
            bytes1.IsEqualTo(bytes2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new byte[] {2, 1}, new byte[] {1, 2})]
        [InlineData(new byte[] {1, 2, 3, 4}, new byte[] {1, 2, 4, 3})]
        public void Returns_false_for_collections_of_different_content(IList<byte> bytes1, IList<byte> bytes2)
        {
            bytes1.IsEqualTo(bytes2).ShouldBeFalse();
        }

        [Theory]
        [InlineData(new byte[0], new byte[0])]
        [InlineData(new byte[] {1, 2}, new byte[] {1, 2})]
        [InlineData(new byte[] {1, 2, 3, 4}, new byte[] {1, 2, 3, 4})]
        public void Returns_true_for_collections_of_same_content(IList<byte> bytes1, IList<byte> bytes2)
        {
            bytes1.IsEqualTo(bytes2).ShouldBeTrue();
        }

        [Theory, SpecialCollection(CollectionType.NonEmptyByte)]
        public void Returns_true_for_params_array(IList<byte> bytes)
        {
            bytes.IsEqualTo(1, 2, 3, 4, 5, 6).ShouldBeTrue();
        }
    }
}