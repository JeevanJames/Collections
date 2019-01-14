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

#if EXPLICIT
namespace Collections.Net.Enumerable
#else
namespace System.Collections.Generic
#endif
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> collection, int chunkSize)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (chunkSize < 1)
                throw new ArgumentOutOfRangeException(nameof(chunkSize));

            T[] chunk = null;
            int currentIndex = 0;

            foreach (T element in collection)
            {
                if (currentIndex == 0)
                    chunk = new T[chunkSize];
                chunk[currentIndex++] = element;
                if (currentIndex >= chunkSize)
                {
                    yield return chunk;
                    currentIndex = 0;
                }
            }

            if (chunk != null && currentIndex > 0)
            {
                T[] trailingChunk = new T[currentIndex];
                Array.Copy(chunk, 0, trailingChunk, 0, currentIndex);
                yield return trailingChunk;
            }
        }

        /// <summary>
        ///     Performs an <paramref name="action"/> on all elements of a <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action to perform.</param>
        /// <exception cref="ArgumentNullException">Thrown of the <paramref name="action"/> is <c>null</c>.</exception>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
                return collection;
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (T item in collection)
                action(item);
            return collection;
        }

        /// <summary>
        ///     Performs an <paramref name="action"/> on all elements of a <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action to perform.</param>
        /// <exception cref="ArgumentNullException">Thrown of the <paramref name="action"/> is <c>null</c>.</exception>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            if (collection == null)
                return collection;
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var index = 0;
            foreach (T item in collection)
                action(item, index++);
            return collection;
        }

        /// <summary>
        ///     Indicates whether the specified collection is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the collection</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns><c>true</c> if the collection is empty; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source collection is <c>null</c>.</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            return collection is ICollection<T> coll ? coll.Count == 0 : !collection.Any();
        }

        /// <summary>
        ///     Indicates whether the specified collection is null or does not contain any elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the collection</typeparam>
        /// <param name="collection"> The collection to test. </param>
        /// <returns> <c>true</c> if the collection is either null or empty; otherwise <c>false</c>. </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return true;
            return collection is ICollection<T> coll ? coll.Count == 0 : !collection.Any();
        }

        /// <summary>
        ///     Determines whether none of the elements of the <paramref name="collection"/> satisfies the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
        /// <returns>
        ///     <c>true</c> if none of the elements in the <paramref name="collection"/> satisfy the
        ///     <paramref name="predicate"/>; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="predicate"/> is <c>null</c>.</exception>
        public static bool None<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return !collection.Any(predicate);
        }

        /// <summary>
        ///     Checks each element of the <paramref name="collection"/> against the specified <paramref name="predicate"/>
        ///     and returns the elements that match and those that do not match.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
        /// <returns>
        ///     Two collections sequences - one with the elements that match the <paramref name="predicate"/> and another
        ///     with elements that do not match.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="predicate"/> is <c>null</c>.</exception>
#if NETSTANDARD2_0
        public static (IEnumerable<T> matches, IEnumerable<T> mismatches) Partition<T>(this IEnumerable<T> collection,
            Func<T, bool> predicate)
#else
        public static PartitionSequence<T> Partition<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
#endif
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var matches = new List<T>();
            var mismatches = new List<T>();

            foreach (T element in collection)
            {
                if (predicate(element))
                    matches.Add(element);
                else
                    mismatches.Add(element);
            }

#if NETSTANDARD2_0
            return (matches, mismatches);
#else
            return new PartitionSequence<T>(matches, mismatches);
#endif
        }

        /// <summary>
        ///     Repeats the <paramref name="collection"/> <paramref name="count"/> number of times.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="count">The number of times to repeat the collection.</param>
        /// <returns>The <paramref name="collection"/> repeated <paramref name="count"/> number of times.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown of the <paramref name="count"/> is less than one.</exception>
        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (var i = 0; i < count; i++)
            {
                foreach (T item in collection)
                    yield return item;
            }
        }

        /// <summary>
        ///     Returns a shuffled version of the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="iterations">The number of times to repeat the shuffle operation.</param>
        /// <returns>A shuffled version of the <paramref name="collection"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="iterations"/> is less than one.</exception>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection, int iterations = 1)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (iterations < 1)
                throw new ArgumentOutOfRangeException(nameof(iterations));

            T[] array = collection.ToArray();
            ListExtensions.ShuffleInplace(array, iterations);
            return array;
        }

        //public static IEnumerable<IEnumerable<T>> SlidingChunk<T>(this IEnumerable<T> collection, int chunkSize)
        //{
        //    if (collection == null)
        //        throw new ArgumentNullException(nameof(collection));
        //    if (chunkSize < 1)
        //        throw new ArgumentOutOfRangeException(nameof(chunkSize));

        //    throw new NotImplementedException();
        //}

        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> collection,
            Func<TInput, TOutput> converter)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return collection.Select(converter).ToArray();
        }

        public static T[] ToArray<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return collection.Where(predicate).ToArray();
        }

        /// <summary>
        ///     Converts selected elements of the specified collection to an array of another type.
        /// </summary>
        /// <typeparam name="TInput">The type of elements in the source collection.</typeparam>
        /// <typeparam name="TOutput">The type of elements in the target array.</typeparam>
        /// <param name="collection">The source collection.</param>
        /// <param name="predicate">A delegate that controls the elements that are included in the target array.</param>
        /// <param name="converter">A delegate that converts elements from the source collection type to the target array element type.</param>
        /// <returns>An array of the target element type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source collection, <c>predicate</c> delegate or <c>converter</c> delegate are null.</exception>
        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> collection, Func<TInput, bool> predicate,
            Func<TInput, TOutput> converter)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return collection.Where(predicate).Select(converter).ToArray();
        }

        public static List<TOutput> ToList<TInput, TOutput>(this IEnumerable<TInput> collection,
            Func<TInput, TOutput> converter)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return collection.Select(converter).ToList();
        }

        public static List<T> ToList<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return collection.Where(predicate).ToList();
        }

        public static List<TOutput> ToList<TInput, TOutput>(this IEnumerable<TInput> collection,
            Func<TInput, bool> predicate, Func<TInput, TOutput> converter)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return collection.Where(predicate).Select(converter).ToList();
        }

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return collection.Where(element => !predicate(element));
        }

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> collection, Func<T, int, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return collection.Where((element, i) => !predicate(element, i));
        }
    }
}