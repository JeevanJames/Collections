using System.Linq;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
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
                throw new ArgumentNullException("collection");
            if (items == null)
                return;

            foreach (T item in items)
                collection.Add(item);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            if (items == null)
                return;

            collection.AddRange(items.Where(predicate));
        }

        public static void AddRange<TDest, TSource>(this ICollection<TDest> collection, IEnumerable<TSource> items,
            Func<TSource, TDest> converter)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (converter == null)
                throw new ArgumentNullException("converter");
            if (items == null)
                return;

            collection.AddRange(items.Select(item => converter(item)));
        }

        public static void AddRange<TDest, TSource>(this ICollection<TDest> collection, IEnumerable<TSource> items,
            Func<TSource, bool> predicate, Func<TSource, TDest> converter)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            if (converter == null)
                throw new ArgumentNullException("converter");
            if (items == null)
                return;

            collection.AddRange(items.Where(predicate).Select(item => converter(item)));
        }

        /// <summary>
        ///     Populates each item in a byte collection with a specific value.
        /// </summary>
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

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                return;
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (T item in source)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source == null)
                return;
            if (action == null)
                throw new ArgumentNullException("action");

            var index = 0;
            foreach (T item in source)
                action(item, index++);
        }

        public static int IndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

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
                throw new ArgumentNullException("list");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                    yield return i;
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source) =>
            source is ICollection<T> collection ? collection.Count == 0 : !source.Any();

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return true;
            return source is ICollection<T> collection ? collection.Count == 0 : !source.Any();
        }

        public static int LastIndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

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
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">source or predicate is null</exception>
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            return !source.Any(predicate);
        }

        public static bool Remove<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

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

        public static int RemoveAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

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

        public static bool RemoveLast<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

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
                throw new ArgumentNullException("source");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            for (var i = 0; i < count; i++)
            {
                foreach (T item in source)
                    yield return item;
            }
        }

        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> source,
            Func<TInput, TOutput> converter)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (converter == null)
                throw new ArgumentNullException("converter");

            var array = new TOutput[source.Count()];
            var counter = 0;
            foreach (TInput item in source)
                array[counter++] = converter(item);
            return array;
        }

        public static T[] ToArray<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return source.Where(predicate).ToArray();
        }

        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> source, Func<TInput, bool> predicate,
            Func<TInput, TOutput> converter)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            if (converter == null)
                throw new ArgumentNullException("converter");

            return source.Where(predicate).Select(item => converter(item)).ToArray();
        }
    }
}
