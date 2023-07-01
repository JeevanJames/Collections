// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Enumerable;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class None_Tests
    {
        private static readonly int[] OddNumbers = { 1, 3, 5, 7, 9, 11 };
        private static readonly int[] MixedNumbers = { 1, 2, 3, 5, 7, 8, 9, 11 };

        [Fact]
        public void Throws_if_source_is_null()
        {
            IEnumerable<int> numbers = null;

            Should.Throw<ArgumentNullException>(() => numbers.None(n => n % 2 == 0));
        }

        [Fact]
        public void Does_not_throw_if_source_is_empty()
        {
            IEnumerable<int> numbers = new int[0];

            Should.NotThrow(() => numbers.None(n => n % 2 == 0));
        }

        [Fact]
        public void Throws_if_predicate_is_null()
        {
            Should.Throw<ArgumentNullException>(() => OddNumbers.None(null));
        }

        [Fact]
        public void Returns_true_if_no_item_matches()
        {
            OddNumbers.None(n => n % 2 == 0).ShouldBeTrue();
        }

        [Fact]
        public void Returns_false_if_any_item_does_not_match()
        {
            MixedNumbers.None(n => n % 2 == 0).ShouldBeFalse();
        }
    }
}
