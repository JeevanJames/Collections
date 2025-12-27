// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ByteCollectionExtensions
{
    public sealed class ToString_Tests
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
        public void Does_not_throw_if_delimiter_is_null_or_empty(IList<byte> bytes)
        {
            Should.NotThrow(() => bytes.ToString(null));
            Should.NotThrow(() => bytes.ToString(string.Empty));
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_without_delimiters(IList<byte> bytes)
        {
            bytes.ToString(null).ShouldBe("123456");
            bytes.ToString(string.Empty).ShouldBe("123456");
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_single_character_delimiter(IList<byte> bytes)
        {
            bytes.ToString(",").ShouldBe("1,2,3,4,5,6");
        }

        [Theory, ByteArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_Multi_character_delimiter(IList<byte> bytes)
        {
            bytes.ToString("#;%").ShouldBe("1#;%2#;%3#;%4#;%5#;%6");
        }
    }
}
