// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.LongCollectionExtensions
{
    public sealed class ToString_Tests
    {
        [Theory, LongArray(CollectionType.Null)]
        public void Returns_null_if_longs_are_null(IList<long> longs)
        {
            longs.ToString(",").ShouldBeNull();
        }

        [Theory, LongArray(CollectionType.Empty)]
        public void Returns_empty_string_if_longs_is_empty(IList<long> longs)
        {
            longs.ToString(",").ShouldBeEmpty();
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Does_not_throw_if_delimiter_is_null_or_empty(IList<long> longs)
        {
            Should.NotThrow(() => longs.ToString(null));
            Should.NotThrow(() => longs.ToString(string.Empty));
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_without_delimiters(IList<long> longs)
        {
            longs.ToString(null).ShouldBe("123456");
            longs.ToString(string.Empty).ShouldBe("123456");
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_single_character_delimiter(IList<long> longs)
        {
            longs.ToString(",").ShouldBe("1,2,3,4,5,6");
        }

        [Theory, LongArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_Multi_character_delimiter(IList<long> longs)
        {
            longs.ToString("#;%").ShouldBe("1#;%2#;%3#;%4#;%5#;%6");
        }
    }
}
