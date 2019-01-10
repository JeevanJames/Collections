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

namespace Collection.Tests.DataAttributes
{
    public sealed class DictionaryAttribute : BaseCollectionAttribute
    {
        public DictionaryAttribute(CollectionType collectionType) : base(collectionType)
        {
        }

        protected override object CreateCollection()
        {
            switch (CollectionType)
            {
                case CollectionType.Null:
                    return null;
                case CollectionType.Empty:
                    return new Dictionary<string, int>(0);
                case CollectionType.NonEmpty:
                case CollectionType.NumbersOneToSix:
                    return new Dictionary<string, int>
                    {
                        ["One"] = 1,
                        ["Two"] = 2,
                        ["Three"] = 3,
                        ["Four"] = 4,
                        ["Five"] = 5,
                        ["Six"] = 6,
                    };
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}