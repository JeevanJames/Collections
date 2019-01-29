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