#region --- License & Copyright Notice ---
/*
Custom collections and collection extensions for .NET
Copyright (c) 2018-2019 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

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