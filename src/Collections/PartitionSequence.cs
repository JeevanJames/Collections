#region --- License & Copyright Notice ---
/*
Custom collections and collection extensions for .NET
Copyright (c) 2018-2020 Jeevan James
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
#if EXPLICIT
using Collections.Net
#else
namespace System.Collections.Generic
#endif
{
    /// <summary>
    ///     Represents the results of a predicate check on all items of a collection.
    /// </summary>
    /// <typeparam name="T">The type of element in the collection.</typeparam>
    public sealed class PartitionSequence<T>
    {
        /// <summary>
        ///     Initializes an instance of the <see cref="PartitionSequence{T}"/> class.
        /// </summary>
        /// <param name="matches">The matching elements.</param>
        /// <param name="mismatches">The elements that do not match.</param>
        internal PartitionSequence(IEnumerable<T> matches, IEnumerable<T> mismatches)
        {
            Matches = matches;
            Mismatches = mismatches;
        }

        /// <summary>
        ///     Collection of all items in the collection that satisfy the predicate.
        /// </summary>
        public IEnumerable<T> Matches { get; }

        /// <summary>
        ///     Collection of all items in the collection that do not satisfy the predicate.
        /// </summary>
        public IEnumerable<T> Mismatches { get; }
    }
}
#endif
