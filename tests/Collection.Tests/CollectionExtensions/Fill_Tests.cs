// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.ListExtensions;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class Fill_Tests
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
