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

#if EXPLICIT
using Collections.Net.List
#else
namespace System.Collections.Generic
#endif
{
    public static class ListExtensions
    {
        /// <summary>
        ///     Populates each item in a byte collection with a specific value.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the collection</typeparam>
        /// <param name="collection"> The byte array to be populated. </param>
        /// <param name="value"> The value to populate the byte array with. </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Fill<T>(this IList<T> collection, T value)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (collection.Count == 0)
                return;
            for (int i = 0; i < collection.Count; i++)
                collection[i] = value;
        }

        public static void Fill<T>(this IList<T> collection, Func<int, T> generator)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (collection.Count == 0)
                return;
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));
            for (int i = 0; i < collection.Count; i++)
                collection[i] = generator(i);
        }

        /// <summary>
        ///     Returns the index of the first element in a <paramref name="collection"/> that matches the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
        /// <returns>The index of the element, if found; otherwise -1.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is <c>null</c>.</exception>
        public static int IndexOf<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = 0; i < collection.Count; i++)
            {
                if (predicate(collection[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        ///     Returns all indices of elements in a <paramref name="collection"/> that match the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
        /// <returns>
        ///     A sequence of indices of the matches elements in the collection, if any are found; otherwise -1.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is <c>null</c>.</exception>
        public static IEnumerable<int> IndexOfAll<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = 0; i < collection.Count; i++)
            {
                if (predicate(collection[i]))
                    yield return i;
            }
        }

        /// <summary>
        ///     Returns the index of the last element in a <paramref name="collection"/> that matches the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
        /// <returns>The index of the element, if found; otherwise -1.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is <c>null</c>.</exception>
        public static int LastIndexOf<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (predicate(collection[i]))
                    return i;
            }
            return -1;
        }

        public static IEnumerable<T> Random<T>(this IList<T> collection, int count)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (collection.Count == 0)
                yield break;
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            var rng = new Rng(collection.Count);

            for (int i = 0; i < count; i++)
            {
                int index = rng.Next();
                yield return collection[index];
            }
        }

        /// <summary>
        ///     Removes all elements from the <paramref name="collection"/> that satisfy the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The predicate delegate to check against.</param>
        /// <returns>The number of elements removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="predicate"/> is <c>null</c>.</exception>
        public static int RemoveAll<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            int count = 0;
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (predicate(collection[i]))
                {
                    collection.RemoveAt(i);
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        ///     Removes the first element from the <paramref name="collection"/> that satisfies the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The predicate delegate to check against.</param>
        /// <returns><c>true</c> if an element was found and removed; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="predicate"/> is <c>null</c>.</exception>
        public static bool RemoveFirst<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = 0; i < collection.Count; i++)
            {
                if (predicate(collection[i]))
                {
                    collection.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Removes the last element from the <paramref name="collection"/> that satisfies the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The predicate delegate to check against.</param>
        /// <returns><c>true</c> if an element was found and removed; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="predicate"/> is <c>null</c>.</exception>
        public static bool RemoveLast<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (predicate(collection[i]))
                {
                    collection.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Shuffles the elements of the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="iterations">The number of times to repeat the shuffle operation.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="iterations"/> is less than one.</exception>
        public static void ShuffleInplace<T>(this IList<T> collection, int iterations = 1)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (iterations < 1)
                throw new ArgumentOutOfRangeException(nameof(iterations));

            var rng = new Rng(collection.Count);

            for (int iteration = 0; iteration < iterations; iteration++)
            {
                T temp;
                for (int i = 0; i < collection.Count; i++)
                {
                    int index1 = rng.Next();
                    int index2 = rng.Next();
                    temp = collection[index1];
                    collection[index1] = collection[index2];
                    collection[index2] = temp;
                }
            }
        }
    }
}