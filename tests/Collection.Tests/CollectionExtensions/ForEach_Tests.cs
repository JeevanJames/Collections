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
    public sealed class ForEach_Tests
    {
        [Fact]
        public void Does_not_do_anything_is_null()
        {
            IEnumerable<int> collection = null;

            Should.NotThrow(() => collection.ForEach(n => Console.WriteLine(n)));
            Should.NotThrow(() => collection.ForEach((n, i) => Console.WriteLine(n)));
        }

        [Fact]
        public void Throws_if_action_is_null()
        {
            IEnumerable<int> collection = new[] {1, 2, 3, 4, 5, 6};

            Should.Throw<ArgumentNullException>(() => collection.ForEach((Action<int>) null));
            Should.Throw<ArgumentNullException>(() => collection.ForEach((Action<int, int>) null));
        }

        [Fact]
        public void Performs_action_on_each_element()
        {
            IEnumerable<int> collection = new[] {1, 2, 3, 4, 5, 6};

            Should.NotThrow(() => collection.ForEach(Console.WriteLine));
            Should.NotThrow(() => collection.ForEach((n, i) => Console.WriteLine(n)));
        }

        [Fact]
        public void Passes_index_of_item_in_collection()
        {
            IEnumerable<int> collection = new[] { 0, 1, 2, 3, 4, 5 };

            collection.ForEach((n, i) => n.ShouldBe(i));
        }
    }
}
