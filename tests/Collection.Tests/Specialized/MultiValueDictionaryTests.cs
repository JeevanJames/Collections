// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.Collections.Specialized;

using Shouldly;

using Xunit;

namespace Collection.Tests.Specialized
{
    public sealed class MultiValueDictionaryTests
    {
        public sealed class General_Tests
        {
            [Fact]
            public void GeneralTest()
            {
                var countries = new MultiValueDictionary<string, string>();
                countries.Add("Asia", "India");
                countries.Add("Europe", "Belgium");
                countries.Add("Europe", "France");
                countries.Add("Europe", "Ireland");

                countries.ShouldNotBeNull();
                countries.Count.ShouldBe(2);
            }
        }
    }
}
