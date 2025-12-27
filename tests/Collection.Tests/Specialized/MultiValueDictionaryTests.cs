// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

#if EXPLICIT
using Collections.Net.Specialized;
#else
using System.Collections.Specialized;
#endif


namespace Collection.Tests.Specialized;

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

        [Fact]
        public void TripleDictionaryTest()
        {
            var students = new Dictionary<string, string, int>
            {
                { "ID001", "Ryan", 14 },
                { "ID002", "Emma", 7 }
            };

            students.ShouldNotBeNull();
            students.Count.ShouldBe(2);
        }

        [Fact]
        public void TripleDictionaryInitializerTest()
        {
            var students = new Dictionary<string, string, int>
            {
                ["ID001"] = ("Ryan", 14),
                ["ID002"] = ("Emma", 7),
            };

            students.ShouldNotBeNull();
            students.Count.ShouldBe(2);
        }
    }
}
