// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Collection.Tests;

public sealed class EnumIteratorTests
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
        Should.Throw<ArgumentNullException>(() => EnumIterator.For(enumType: null!));
    }

    [Fact]
    public void Throws_if_specified_type_is_not_enum()
    {
        Should.Throw<ArgumentException>(() => EnumIterator.For(typeof(EnumIteratorTests)));
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
