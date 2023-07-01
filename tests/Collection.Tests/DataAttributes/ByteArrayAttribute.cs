// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

using System;

namespace Collection.Tests.DataAttributes
{
    public sealed class ByteArrayAttribute : BaseCollectionAttribute
    {
        public ByteArrayAttribute(CollectionType collectionType) : base(collectionType)
        {
        }

        protected override object CreateCollection()
        {
            switch (CollectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new byte[0];
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new byte[] { 1, 2, 3, 4, 5, 6 };
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public sealed class IntArrayAttribute : BaseCollectionAttribute
    {
        public IntArrayAttribute(CollectionType collectionType) : base(collectionType)
        {
        }

        protected override object CreateCollection()
        {
            switch (CollectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new int[0];
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new int[] { 1, 2, 3, 4, 5, 6 };
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public sealed class LongArrayAttribute : BaseCollectionAttribute
    {
        public LongArrayAttribute(CollectionType collectionType) : base(collectionType)
        {
        }

        protected override object CreateCollection()
        {
            switch (CollectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new long[0];
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new long[] { 1, 2, 3, 4, 5, 6 };
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public sealed class CharArrayAttribute : BaseCollectionAttribute
    {
        public CharArrayAttribute(CollectionType collectionType) : base(collectionType)
        {
        }

        protected override object CreateCollection()
        {
            switch (CollectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new char[0];
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new char[] { '1', '2', '3', '4', '5', '6' };
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public sealed class ShortArrayAttribute : BaseCollectionAttribute
    {
        public ShortArrayAttribute(CollectionType collectionType) : base(collectionType)
        {
        }

        protected override object CreateCollection()
        {
            switch (CollectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new short[0];
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new short[] { 1, 2, 3, 4, 5, 6 };
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}