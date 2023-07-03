// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0.  See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
#if EXPLICIT
using Collections.Net.Collection;

namespace Collections.Net.List
#else
namespace System.Collections.Generic
#endif
{
    public static class ListExtensions
    {
        /// <summary>
        ///     Populates each item in a <paramref name="list"/> with a specific <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list</typeparam>
        /// <param name="list">The collection to be populated.</param>
        /// <param name="value">The value with which to populate the collection.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is <c>null</c>.</exception>
        public static void Fill<T>(this IList<T> list, T value)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                return;
            for (int i = 0; i < list.Count; i++)
                list[i] = value;
        }

        /// <summary>
        ///     Populates each item in a <paramref name="list"/> with the values from a <paramref name="generator"/>
        ///     delegate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list</typeparam>
        /// <param name="list">The collection to be populated.</param>
        /// <param name="generator">The delegate to generate the values for each item.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is <c>null</c>.</exception>
        public static void Fill<T>(this IList<T> list, Func<int, T> generator)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                return;
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));
            for (int i = 0; i < list.Count; i++)
                list[i] = generator(i);
        }

        /// <summary>
        ///     Returns the index of the first element in a <paramref name="list"/> that matches the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
        /// <returns>The index of the element, if found; otherwise -1.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is <c>null</c>.</exception>
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

        /// <summary>
        ///     Returns all indices of elements in a <paramref name="list"/> that match the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
        /// <returns>
        ///     A sequence of indices of the matches elements in the list, if any are found; otherwise -1.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is <c>null</c>.</exception>
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

        public static void InsertRange<T>(this IList<T> list, int index, params T[] items)
        {
            InsertRange(list, index, (IEnumerable<T>)items);
        }

        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T>? items)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (items == null)
                return;

            if (index >= list.Count)
                list.AddRange(items);
            else
            {
                int originalCount = list.Count;
                foreach (T item in items)
                    list.Insert(index + (list.Count - originalCount), item);
            }
        }

        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            InsertRange(list, index, items.Where(predicate));
        }

        public static void InsertRange<TDest, TSource>(this IList<TDest> list, int index, IEnumerable<TSource> items,
            Func<TSource, TDest> converter)
        {
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));
            InsertRange(list, index, items.Select(converter));
        }

        public static void InsertRange<TDest, TSource>(this IList<TDest> list, int index, IEnumerable<TSource> items,
            Func<TSource, bool> predicate, Func<TSource, TDest> converter,
            Func<TDest, bool>? afterConvertPredicate = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            IEnumerable<TDest> convertedItems = items.Where(predicate).Select(converter);
            if (afterConvertPredicate != null)
                convertedItems = convertedItems.Where(afterConvertPredicate);
            InsertRange(list, index, convertedItems);
        }

        /// <summary>
        ///     Returns the index of the last element in a <paramref name="list"/> that matches the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The <paramref name="predicate"/> to check against.</param>
        /// <returns>The index of the element, if found; otherwise -1.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is <c>null</c>.</exception>
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

        public static IEnumerable<T> Random<T>(this IList<T> list, int count)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                yield break;
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            var rng = new Rng(list.Count);

            for (int i = 0; i < count; i++)
            {
                int index = rng.Next();
                yield return list[index];
            }
        }

        /// <summary>
        ///     Removes all elements from the <paramref name="list"/> that satisfy the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The predicate delegate to check against.</param>
        /// <returns>The number of elements removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="list"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="predicate"/> is <c>null</c>.</exception>
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

        /// <summary>
        ///     Removes the first element from the <paramref name="list"/> that satisfies the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The predicate delegate to check against.</param>
        /// <returns><c>true</c> if an element was found and removed; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="list"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="predicate"/> is <c>null</c>.</exception>
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

        /// <summary>
        ///     Removes the last element from the <paramref name="list"/> that satisfies the specified
        ///     <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The predicate delegate to check against.</param>
        /// <returns><c>true</c> if an element was found and removed; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="list"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="predicate"/> is <c>null</c>.</exception>
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

        /// <summary>
        ///     Shuffles the elements of the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="iterations">The number of times to repeat the shuffle operation.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="list"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="iterations"/> is less than one.</exception>
        public static void ShuffleInplace<T>(this IList<T> list, int iterations = 1)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (iterations < 1)
                throw new ArgumentOutOfRangeException(nameof(iterations));

            var rng = new Rng(list.Count);

            for (int iteration = 0; iteration < iterations; iteration++)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int index1 = rng.Next();
                    int index2 = rng.Next();
                    T temp = list[index1];
                    list[index1] = list[index2];
                    list[index2] = temp;
                }
            }
        }

        /// <summary>
        ///     Returns overlapping chunks of the specified <paramref name="chunkSize"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="chunkSize">The size of the chunks.</param>
        /// <returns>Collection of overlapping chunks.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="list"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="chunkSize"/> is less than one.
        /// </exception>
        public static IEnumerable<IList<T>> SlidingChunk<T>(this IList<T> list, int chunkSize)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (chunkSize < 1)
                throw new ArgumentOutOfRangeException(nameof(chunkSize));

            if (chunkSize > list.Count)
                chunkSize = list.Count;

            int upperLimit = Math.Max(0, list.Count - chunkSize);
            for (int i = 0; i <= upperLimit; i++)
            {
                var chunk = new T[chunkSize];
                for (int j = i; j < i + chunkSize; j++)
                    chunk[j - i] = list[j];
                yield return chunk;
            }
        }

    }
}
