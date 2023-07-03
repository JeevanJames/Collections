// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net;
#endif

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

        [Fact]
        public void Returns_collection_of_enum_values_based_on_type()
        {
            var values = EnumIterator.For(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();

            values.Count.ShouldBe(7);
            values.ShouldBe(new[]
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
    }
}
