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

using System.Linq;

#if EXPLICIT
using Collections.Net.Collection
#else
namespace System.Collections.Generic
#endif
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, params T[] items) =>
            AddRange(collection, (IEnumerable<T>)items);

        /// <summary>
        ///     Adds the elements of the specified <see cref="IEnumerable{T}" /> to the collection
        /// </summary>
        /// <typeparam name="T">The type of the elements of the collection</typeparam>
        /// <param name="collection">The collection to add the items to.</param>
        /// <param name="items">
        ///     The <see cref="IEnumerable{T}" /> whose elements should be added to the end of the collection. The
        ///     <see cref="IEnumerable{T}" /> itself cannot be null, but it can contain elements that are null, if type T is a
        ///     reference type.
        /// </param>
        /// <exception cref="ArgumentNullException">collection is null</exception>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (items == null)
                return;

            foreach (T item in items)
                collection.Add(item);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (items == null)
                return;

            collection.AddRange(items.Where(predicate));
        }

        public static void AddRange<TDest, TSource>(this ICollection<TDest> collection, IEnumerable<TSource> items,
            Func<TSource, TDest> converter)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));
            if (items == null)
                return;

            collection.AddRange(items.Select(converter));
        }

        public static void AddRange<TDest, TSource>(this ICollection<TDest> collection, IEnumerable<TSource> items,
            Func<TSource, bool> predicate, Func<TSource, TDest> converter, Func<TDest, bool> afterConvertPredicate = null)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));
            if (items == null)
                return;

            IEnumerable<TDest> convertedItems = items.Where(predicate).Select(converter);
            if (afterConvertPredicate != null)
                convertedItems = convertedItems.Where(afterConvertPredicate);
            collection.AddRange(convertedItems);
        }

        /// <summary>
        ///     Returns the range of elements from the specified start and end index of a collection.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="start">
        ///     The inclusive start index of the range. If <c>null</c>, then this will default to zero.
        /// </param>
        /// <param name="end">
        ///     The exclusive end index of the range. If <c>null</c>, then this will default to the number of elements
        ///     in the collection.
        /// </param>
        /// <returns>An <see cref="IEnumerable{T}"/> with the elements from the specified range.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the collection is <c>null</c>.</exception>
        public static IEnumerable<T> Range<T>(this ICollection<T> collection, int? start = null, int? end = null)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            int startIndex = start.GetValueOrDefault();
            int endIndex = end.GetValueOrDefault(collection.Count);

            if (startIndex < 0 || startIndex >= collection.Count)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (endIndex < startIndex)
                throw new ArgumentOutOfRangeException(nameof(end), $"End index {endIndex} should be greater or equal to start index {startIndex}.");
            if (endIndex > collection.Count)
                endIndex = collection.Count;

            return collection.Skip(startIndex).Take(endIndex - startIndex);
        }

        public static IEnumerable<T> RangeOfLength<T>(this ICollection<T> collection, int? start = null,
            int? count = null) =>
            Range(collection, start, count.HasValue ? (int?) start.GetValueOrDefault() + count.Value : null);
    }
}
