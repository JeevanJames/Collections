using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace Collection.Tests
{
    public sealed class EnumIterator_Tests
    {
        [Fact]
        public void Returns_collection_of_enum_values()
        {
            var values = EnumIterator.For<DayOfWeek>().ToList();

            values.Count.ShouldBe(7);
            values.ShouldBe(new []
            {
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
            });
        }

        [Fact]
        public void Throws_if_enum_type_is_null()
        {
            Should.Throw<ArgumentNullException>(() => EnumIterator.For(null));
        }

        [Fact]
        public void Throws_if_specified_type_is_not_enum()
        {
            Should.Throw<ArgumentException>(() => EnumIterator.For(typeof(EnumIterator_Tests)));
        }
    }
}