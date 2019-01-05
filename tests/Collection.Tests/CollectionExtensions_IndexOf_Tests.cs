using System;
using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_IndexOf_Tests
    {
        [Theory, SpecialCollection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.IndexOf(n => n % 2 == 0));
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IList<int> collection)
        {
            Should.Throw<ArgumentNullException>(() => collection.IndexOf(null));
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Finds_index_of_existing_element(IList<int> collection)
        {
            int index = collection.IndexOf(n => n % 2 == 0);

            index.ShouldBe(1);
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Returns_negative_number_for_nonexistent_element(IList<int> collection)
        {
            int index = collection.IndexOf(n => n == 100);

            index.ShouldBeLessThan(0);
        }
    }
}
