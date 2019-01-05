using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_Fill_Tests
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
        public void Fills_collection_with_generated_values()
        {
            IList<int> collection = new int[6];

            collection.Fill(n => n * 2);

            collection.Count.ShouldBe(6);
            collection.ShouldBe(new[] {0, 2, 4, 6, 8, 10});
        }
    }
}