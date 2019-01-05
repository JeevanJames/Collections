using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace Collection.Tests
{
    public sealed class ByteCollectionExtensions_IsNullOrZeroed_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(new byte[0])]
        [InlineData(new byte[] {0, 0, 0, 0})]
        public void Returns_true_if_collection_is_null_or_empty_or_zeroed(IList<byte> bytes)
        {
            bytes.IsNullOrZeroed().ShouldBeTrue();
        }

        [Fact]
        public void Returns_false_if_any_element_is_non_zero()
        {
            byte[] bytes = {0, 1, 0, 0};
            bytes.IsNullOrZeroed().ShouldBeFalse();
        }
    }
}