﻿#region --- License & Copyright Notice ---
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

#if !NETSTANDARD2_0
namespace System.Collections.Generic
{
    public sealed class PartitionSequence<T>
    {
        internal PartitionSequence(IEnumerable<T> matches, IEnumerable<T> mismatches)
        {
            Matches = matches;
            Mismatches = mismatches;
        }

        public IEnumerable<T> Matches { get; }

        public IEnumerable<T> Mismatches { get; }
    }
}
#endif
