// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System.Collections.Generic;

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Enumerable;
#endif

namespace Collection.Tests.CollectionExtensions
{
    public sealed class IsNotNullOrEmpty_Tests
    {
        [Theory, DataAttributes.Collection(CollectionType.Null)]
        public void Returns_false_if_sequence_is_null(IEnumerable<int> sequence)
        {
            sequence.IsNotNullOrEmpty().ShouldBeFalse();
        }

        [Theory, DataAttributes.Collection(CollectionType.Empty)]
        public void Returns_false_if_sequence_is_empty(IEnumerable<int> sequence)
        {
            sequence.IsNotNullOrEmpty().ShouldBeFalse();
        }

        [Theory, DataAttributes.Collection(CollectionType.NonEmpty)]
        public void Returns_true_if_sequence_is_not_empty(IEnumerable<int> sequence)
        {
            sequence.IsNotNullOrEmpty().ShouldBeTrue();
        }
    }
}
