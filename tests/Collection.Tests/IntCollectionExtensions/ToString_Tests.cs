// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Numeric;
#endif

namespace Collection.Tests.IntCollectionExtensions
{
    public sealed class ToString_Tests
    {
        [Theory, IntArray(CollectionType.Null)]
        public void Returns_null_if_ints_are_null(IList<int> ints)
        {
            ints.ToString(",").ShouldBeNull();
        }

        [Theory, IntArray(CollectionType.Empty)]
        public void Returns_empty_string_if_ints_is_empty(IList<int> ints)
        {
            ints.ToString(",").ShouldBeEmpty();
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Does_not_throw_if_delimiter_is_null_or_empty(IList<int> ints)
        {
            Should.NotThrow(() => ints.ToString(null));
            Should.NotThrow(() => ints.ToString(string.Empty));
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_without_delimiters(IList<int> ints)
        {
            ints.ToString(null).ShouldBe("123456");
            ints.ToString(string.Empty).ShouldBe("123456");
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_single_character_delimiter(IList<int> ints)
        {
            ints.ToString(",").ShouldBe("1,2,3,4,5,6");
        }

        [Theory, IntArray(CollectionType.NonEmpty)]
        public void Returns_joined_string_with_Multi_character_delimiter(IList<int> ints)
        {
            ints.ToString("#;%").ShouldBe("1#;%2#;%3#;%4#;%5#;%6");
        }
    }
}
