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

namespace Collection.Tests.ShortCollectionExtensions
{
    public sealed class ToString_Tests
    {
        [Theory, ShortArray(CollectionType.Null)]
        public void Returns_null_if_shorts_are_null(IList<short> shorts)
        {
            shorts.ToString(",").ShouldBeNull();
        }

        [Theory, ShortArray(CollectionType.Empty)]
        public void Returns_empty_string_if_shorts_is_empty(IList<short> shorts)
        {
            shorts.ToString(",").ShouldBeEmpty();
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Does_not_throw_if_delimiter_is_null_or_empty(IList<short> shorts)
        {
            Should.NotThrow(() => shorts.ToString(null));
            Should.NotThrow(() => shorts.ToString(string.Empty));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_without_delimiters(IList<short> shorts)
        {
            shorts.ToString(null).ShouldBe("123456");
            shorts.ToString(string.Empty).ShouldBe("123456");
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_single_character_delimiter(IList<short> shorts)
        {
            shorts.ToString(",").ShouldBe("1,2,3,4,5,6");
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_Multi_character_delimiter(IList<short> shorts)
        {
            shorts.ToString("#;%").ShouldBe("1#;%2#;%3#;%4#;%5#;%6");
        }
    }
}
