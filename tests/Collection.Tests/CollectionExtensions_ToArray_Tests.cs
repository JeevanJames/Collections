using System;
using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_ToArray_Tests
    {
        [Theory, SpecialCollection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToArray<int, string>(n => n.ToString()));
            Should.Throw<ArgumentNullException>(() => collection.ToArray<int>(n => n % 2 == 0));
            Should.Throw<ArgumentNullException>(() => collection.ToArray(n => n % 2 == 0, n => n * 2));
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToArray<int>(null));
            Should.Throw<ArgumentNullException>(() => collection.ToArray(null, n => n.ToString()));
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_converter_is_null(IEnumerable<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.ToArray<int, string>((Func<int, string>) null));
            Should.Throw<ArgumentNullException>(() => collection.ToArray(n => n % 2 == 0, (Func<int, string>) null));
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Converts_int_array_to_string_array(IEnumerable<int> collection)
        {
            string[] converted = collection.ToArray(n => n.ToString());

            converted.Length.ShouldBe(6);
            converted.ShouldBe(new[] {"1", "2", "3", "4", "5", "6"});
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Returns_array_of_even_numbers(IEnumerable<int> collection)
        {
            int[] result = collection.ToArray(n => n % 2 == 0);

            result.Length.ShouldBe(3);
            result.ShouldBe(new[] { 2, 4, 6 });
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Returns_array_of_even_numbers_strings(IEnumerable<int> collection)
        {
            string[] result = collection.ToArray(n => n % 2 == 0, n => n.ToString());

            result.Length.ShouldBe(3);
            result.ShouldBe(new[] { "2", "4", "6" });
        }
    }
}