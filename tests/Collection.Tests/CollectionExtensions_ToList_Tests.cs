using System;
using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_ToList_Tests
    {
        [Theory, SpecialCollection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToList(converter: n => n.ToString()));
            Should.Throw<ArgumentNullException>(() => collection.ToList(predicate: n => n % 2 == 0));
            Should.Throw<ArgumentNullException>(() => collection.ToList(n => n % 2 == 0, n => n * 2));
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToList(predicate: null));
            Should.Throw<ArgumentNullException>(() => collection.ToList(null, n => n.ToString()));
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_converter_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToList(converter: (Func<int, string>) null));
            Should.Throw<ArgumentNullException>(() => collection.ToList(n => n % 2 == 0, (Func<int, string>) null));
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Converts_int_array_to_string_array(IEnumerable<int> collection)
        {
            List<string> converted = collection.ToList(n => n.ToString());

            converted.Count.ShouldBe(6);
            converted.ShouldBe(new[] {"1", "2", "3", "4", "5", "6"});
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Returns_array_of_even_numbers(IEnumerable<int> collection)
        {
            List<int> result = collection.ToList(n => n % 2 == 0);

            result.Count.ShouldBe(3);
            result.ShouldBe(new[] { 2, 4, 6 });
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Returns_array_of_even_numbers_strings(IEnumerable<int> collection)
        {
            List<string> result = collection.ToList(n => n % 2 == 0, n => n.ToString());

            result.Count.ShouldBe(3);
            result.ShouldBe(new[] { "2", "4", "6" });
        }
    }
}