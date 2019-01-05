using System;
using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class CollectionExtensions_IndexOfAll_Tests
    {
        [Theory, SpecialCollection(CollectionType.Null)]
        public void Throws_if_collection_is_null(IList<int> collection)
        {
            // ToList is needed because the returned collection is lazy
            Should.Throw<ArgumentNullException>(() => collection.IndexOfAll(n => n % 2 == 0).ToList());
        }

        [Theory, SpecialCollection(CollectionType.NonEmpty)]
        public void Throws_if_predicate_is_null(IList<int> collection)
        {
            // ToList is needed because the returned collection is lazy
            Should.Throw<ArgumentNullException>(() => collection.IndexOfAll(null).ToList());
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Finds_indices_of_all_matching_elements(IList<int> collection)
        {
            IEnumerable<int> indices = collection.IndexOfAll(n => n % 2 == 0);

            indices.ShouldBe(new[] {1, 3, 5});
        }

        [Theory, SpecialCollection(CollectionType.NumbersOneToSix)]
        public void Returns_empty_enumeration_if_matching_elements_not_found(IList<int> collection)
        {
            IEnumerable<int> indices = collection.IndexOfAll(n => n > 100);

            indices.ShouldBeEmpty();
        }
    }
}