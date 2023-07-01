// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Dictionary;
#endif

namespace Collection.Tests.DictionaryExtensions
{
    public sealed class GetValueOrDefault_Tests
    {
        [Theory, Dictionary(CollectionType.Null)]
        public void Throws_if_dictionary_is_null(IDictionary<string, int> dictionary)
        {
            Should.Throw<ArgumentNullException>(() => dictionary.GetValueOrDefault("Two"));
        }

        [Theory, Dictionary(CollectionType.NumbersOneToSix)]
        public void Returns_value_for_existing_key(IDictionary<string, int> dictionary)
        {
            int value = dictionary.GetValueOrDefault("Two");

            value.ShouldBe(2);
        }

        [Theory, Dictionary(CollectionType.NumbersOneToSix)]
        public void Returns_default_value_for_nonexistent_key(IDictionary<string, int> dictionary)
        {
            dictionary.GetValueOrDefault("Hundred").ShouldBe(0);
            dictionary.GetValueOrDefault(string.Empty, 200).ShouldBe(200);
        }
    }
}
