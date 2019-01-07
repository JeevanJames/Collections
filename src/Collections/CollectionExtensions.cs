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

using System.Linq;

namespace System.Collections.Generic
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

            collection.AddRange(items.Select(item => converter(item)));
        }

        public static void AddRange<TDest, TSource>(this ICollection<TDest> collection, IEnumerable<TSource> items,
            Func<TSource, bool> predicate, Func<TSource, TDest> converter)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));
            if (items == null)
                return;

            collection.AddRange(items.Where(predicate).Select(item => converter(item)));
        }

        public static IEnumerable<IList<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Populates each item in a byte collection with a specific value.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the collection</typeparam>
        /// <param name="source"> The byte array to be populated. </param>
        /// <param name="value"> The value to populate the byte array with. </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Fill<T>(this IList<T> source, T value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source.Count == 0)
                return;
            for (int i = 0; i < source.Count; i++)
                source[i] = value;
        }

        public static void Fill<T>(this IList<T> source, Func<int, T> generator)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source.Count == 0)
                return;
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));
            for (int i = 0; i < source.Count; i++)
                source[i] = generator(i);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                return;
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (T item in source)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source == null)
                return;
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var index = 0;
            foreach (T item in source)
                action(item, index++);
        }

        public static int IndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                    return i;
            }
            return -1;
        }

        public static IEnumerable<int> IndexOfAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                    yield return i;
            }
        }

        /// <summary>
        ///     Indicates whether the specified collection is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the collection</typeparam>
        /// <param name="source">The collection to test.</param>
        /// <returns><c>true</c> if the collection is empty; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source collection is <c>null</c>.</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return source is ICollection<T> collection ? collection.Count == 0 : !source.Any();
        }

        /// <summary>
        ///     Indicates whether the specified collection is null or does not contain any elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the collection</typeparam>
        /// <param name="source"> The collection to test. </param>
        /// <returns> <c>true</c> if the collection is either null or empty; otherwise <c>false</c>. </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return true;
            return source is ICollection<T> collection ? collection.Count == 0 : !source.Any();
        }

        public static int LastIndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        ///     Determines whether none of the elements of a sequence satisfies the specified condition
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> whose elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns><c>true</c> if any elements in the source sequence pass the test in the specified predicate; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <c>source</c> or <c>predicate</c> is <c>null</c>.</exception>
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            return !source.Any(predicate);
        }

        public static int RemoveAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            int count = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    count++;
                }
            }
            return count;
        }

        public static bool RemoveFirst<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public static bool RemoveLast<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            for (var i = 0; i < count; i++)
            {
                foreach (T item in source)
                    yield return item;
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            throw new NotImplementedException();
        }

        public static void ShuffleInplace<T>(this IList<T> source)
        {
            throw new NotImplementedException();
        }

        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> source,
            Func<TInput, TOutput> converter)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return source.Select(converter).ToArray();
        }

        public static T[] ToArray<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return source.Where(predicate).ToArray();
        }

        /// <summary>
        ///     Converts selected elements of the specified collection to an array of another type.
        /// </summary>
        /// <typeparam name="TInput">The type of elements in the source collection.</typeparam>
        /// <typeparam name="TOutput">The type of elements in the target array.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="predicate">A delegate that controls the elements that are included in the target array.</param>
        /// <param name="converter">A delegate that converts elements from the source collection type to the target array element type.</param>
        /// <returns>An array of the target element type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source collection, <c>predicate</c> delegate or <c>converter</c> delegate are null.</exception>
        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> source, Func<TInput, bool> predicate,
            Func<TInput, TOutput> converter)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return source.Where(predicate).Select(converter).ToArray();
        }

        public static List<TOutput> ToList<TInput, TOutput>(this IEnumerable<TInput> source,
            Func<TInput, TOutput> converter)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return source.Select(converter).ToList();
        }

        public static List<T> ToList<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return source.Where(predicate).ToList();
        }

        public static List<TOutput> ToList<TInput, TOutput>(this IEnumerable<TInput> source,
            Func<TInput, bool> predicate, Func<TInput, TOutput> converter)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return source.Where(predicate).Select(converter).ToList();
        }

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
