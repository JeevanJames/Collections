using System;
using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_AddRange_Tests
    {
        [Theory, SpecialCollection(CollectionType.Null)]
        public void Throws_if_collection_is_null(ICollection<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.AddRange(7, 8, 9));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new[] {7, 8, 9}, predicate: n => n % 2 == 0));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new[] {"7", "8", "9"}, converter: int.Parse));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new[] {"7", "8", "9"}, s => s == "8", int.Parse));
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Do_nothing_if_items_is_null(ICollection<int> collection)
        {
            Should.NotThrow(() => collection.AddRange(null));
            Should.NotThrow(() => collection.AddRange(null, predicate: n => n % 2 == 0));
            Should.NotThrow(() => collection.AddRange<int, string>(null, converter: int.Parse));
            Should.NotThrow(() => collection.AddRange<int, string>(null, s => s == "8", int.Parse));
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Adds_items_to_the_collection(ICollection<int> collection)
        {
            collection.AddRange(7, 8, 9);

            collection.Count.ShouldBe(9);
            collection.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(ICollection<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new [] {7, 8, 9}, predicate: null));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new [] {"7", "8", "9"}, null, int.Parse));
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Adds_only_even_numbers_to_collection(ICollection<int> collection)
        {
            collection.AddRange(new [] {7, 8, 9}, predicate: n => n % 2 == 0);

            collection.Count.ShouldBe(7);
            collection.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 8});
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_converter_is_null(ICollection<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new [] {7, 8, 9}, converter: null));
            Should.Throw<ArgumentNullException>(() => collection.AddRange(new [] {7, 8, 9}, n => n % 2 == 0, null));
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Adds_3_strings_to_collection_after_converting_to_ints(ICollection<int> collection)
        {
            collection.AddRange(new [] {"7", "8", "9"}, converter: int.Parse);

            collection.Count.ShouldBe(9);
            collection.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Adds_one_matching_item_after_converting_to_ints(ICollection<int> collection)
        {
            collection.AddRange(new [] {"7", "8", "9"}, n => n == "8", int.Parse);

            collection.Count.ShouldBe(7);
            collection.ShouldBe(new[] {1, 2, 3, 4, 5, 6, 8});
        }
    }
}