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

namespace Collection.Tests
{
    public sealed class ByteCollectionExtensions_ToString_Tests
    {
        [Theory, ByteArray(CollectionType.Null)]
        public void Returns_null_if_bytes_are_null(IList<byte> bytes)
        {
            bytes.ToString(",").ShouldBeNull();
        }

        [Theory, ByteArray(CollectionType.Empty)]
        public void Returns_empty_string_if_bytes_is_empty(IList<byte> bytes)
        {
            bytes.ToString(",").ShouldBeEmpty();
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Throws_if_delimiter_is_null(IList<byte> bytes)
        {
            Should.Throw<ArgumentNullException>(() => bytes.ToString(null));
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Does_not_throw_if_delimiter_is_empty(IList<byte> bytes)
        {
            Should.NotThrow(() => bytes.ToString(string.Empty));
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_without_delimiters(IList<byte> bytes)
        {
            bytes.ToString(string.Empty).ShouldBe("123456");
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_single_character_delimiter(IList<byte> bytes)
        {
            bytes.ToString(",").ShouldBe("1,2,3,4,5,6");
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_MULTI_character_delimiter(IList<byte> bytes)
        {
            bytes.ToString("#;%").ShouldBe("1#;%2#;%3#;%4#;%5#;%6");
        }
    }
}