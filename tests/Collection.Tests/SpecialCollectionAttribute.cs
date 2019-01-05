using System;
using System.Collections.Generic;
using System.Reflection;

using Xunit.Sdk;

namespace Collection.Tests
{
    public sealed class SpecialCollectionAttribute : DataAttribute
    {
        private readonly CollectionType _collectionType;

        public SpecialCollectionAttribute(CollectionType collectionType)
        {
            _collectionType = collectionType;
        }

        public override sealed IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] {CreateCollection()};
        }

        private object CreateCollection()
        {
            switch (_collectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new int[0];
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new List<int> {1, 2, 3, 4, 5, 6};
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public enum CollectionType
    {
        Null,
        Empty,
        NonEmpty,
        NumbersOneToSix
    }
}