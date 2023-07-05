// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using Collection.Tests.DataAttributes;

using Shouldly;

using Xunit;

#if EXPLICIT
using Collections.Net.Extensions.Numeric;
#endif

namespace Collection.Tests.ShortCollectionExtensions;

public sealed class IsEqualToTests
{
    [Theory]
    [InlineData(new short[0])]
    [InlineData(new short[] {1, 2, 3})]
    public void Returns_true_for_the_same_reference(IList<short> shorts)
    {
        shorts.IsEqualTo(shorts).ShouldBeTrue();
    }

    [Theory]
    [InlineData(new short[0], null)]
    [InlineData(new short[] {1, 2}, null)]
    [InlineData(null, new short[0])]
    [InlineData(null, new short[] {1, 2})]
    public void Returns_false_if_any_collection_is_null(IList<short> shorts1, IList<short> shorts2)
    {
        shorts1.IsEqualTo(shorts2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new short[0], new short[] {1, 2})]
    [InlineData(new short[] {1, 2}, new short[0])]
    public void Returns_false_for_collections_of_different_lengths(IList<short> shorts1, IList<short> shorts2)
    {
        shorts1.IsEqualTo(shorts2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new short[] {2, 1}, new short[] {1, 2})]
    [InlineData(new short[] {1, 2, 3, 4}, new short[] {1, 2, 4, 3})]
    public void Returns_false_for_collections_of_different_content(IList<short> shorts1, IList<short> shorts2)
    {
        shorts1.IsEqualTo(shorts2).ShouldBeFalse();
    }

    [Theory]
    [InlineData(new short[0], new short[0])]
    [InlineData(new short[] {1, 2}, new short[] {1, 2})]
    [InlineData(new short[] {1, 2, 3, 4}, new short[] {1, 2, 3, 4})]
    public void Returns_true_for_collections_of_same_content(IList<short> shorts1, IList<short> shorts2)
    {
        shorts1.IsEqualTo(shorts2).ShouldBeTrue();
    }

    [Theory, ShortArray(CollectionType.NonEmpty)]
    public void Returns_true_for_params_array(IList<short> shorts)
    {
        shorts.IsEqualTo<short>(1, 2, 3, 4, 5, 6).ShouldBeTrue();
    }
}
