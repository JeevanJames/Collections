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
using Collections.Net.Dictionary
#else
namespace System.Collections.Generic
#endif
{
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Updates the value of a key in the dictionary, if it exists. If the key does not exist,
        ///     it is added to the dictionary with the specified value.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <param name="value">The value to update or add.</param>
        /// <exception cref="ArgumentNullException">Thrown if the dictionary is <c>null</c>.</exception>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        /// <summary>
        ///     Gets the value for the specified key in a dictionary. If the key does not exist, the <paramref name="defaultValue"/> value is returned.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <param name="defaultValue">The value to return if the key does not exist.</param>
        /// <returns>The value corresponding to the specified key; otherwise the <paramref name="defaultValue"/> value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the dictionary is <c>null</c>.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue = default)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        /// <summary>
        ///     Gets the value associated with the specified key. If the key does not exist, the <paramref name="value"/>
        ///     value is added to the dictionary with the specified key and returned.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <param name="value">The value to add if the key does does exist.</param>
        /// <returns>The value assciated with the specified key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the dictionary is <c>null</c>.</exception>
        public static TValue GetValueOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (dictionary.TryGetValue(key, out TValue existingValue))
                return existingValue;
            dictionary.Add(key, value);
            return value;
        }

        /// <summary>
        ///     Gets the value associated with the specified key. If the key does not exist, a new value is generated using
        ///     the the <paramref name="valueGetter"/> delegate and is added to the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <param name="valueGetter">The delegate used to generate a new value.</param>
        /// <returns>The value assciated with the specified key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the dictionary or delegate is <c>null</c>.</exception>
        public static TValue GetValueOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TKey, TValue> valueGetter)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            if (valueGetter == null)
                throw new ArgumentNullException(nameof(valueGetter));

            if (dictionary.TryGetValue(key, out TValue existingValue))
                return existingValue;
            TValue value = valueGetter(key);
            dictionary.Add(key, value);
            return value;
        }

        /// <summary>
        ///     Gets the value associated with the specified key. If the key does not exist, a new value is generated using
        ///     the the <paramref name="valueGetter"/> delegate and is added to the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <param name="valueGetter">The delegate used to generate a new value.</param>
        /// <returns>The value assciated with the specified key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the dictionary or delegate is <c>null</c>.</exception>
        public static TValue GetValueOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TKey, IDictionary<TKey, TValue>, TValue> valueGetter)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            if (valueGetter == null)
                throw new ArgumentNullException(nameof(valueGetter));

            if (dictionary.TryGetValue(key, out TValue existingValue))
                return existingValue;
            TValue value = valueGetter(key, dictionary);
            dictionary.Add(key, value);
            return value;
        }
    }
}