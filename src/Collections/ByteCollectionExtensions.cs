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

using System.Globalization;
using System.Linq;
using System.Text;

#if EXPLICIT
using Collections.Net.Numeric
#else
namespace System.Collections.Generic
#endif
{
    /// <summary>
    ///     Set of utility extension methods for byte arrays.
    /// </summary>
    public static class ByteCollectionExtensions
    {
        /// <summary>
        ///     Checks whether two byte collections are equal based on their content.
        /// </summary>
        /// <param name="bytes">The source byte array to check.</param>
        /// <param name="other">The second byte array to check.</param>
        /// <returns><c>true</c> if the contents of the two byte arrays are equal.</returns>
        public static bool IsEqualTo(this IList<byte> bytes, IList<byte> other)
        {
            if (ReferenceEquals(bytes, other))
                return true;
            if (bytes == null || other == null)
                return false;
            if (bytes.Count != other.Count)
                return false;
            for (int i = 0; i < bytes.Count; i++)
            {
                if (bytes[i] != other[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     Checks whether two byte collections are equal based on their content.
        /// </summary>
        /// <param name="bytes">The source byte array to check.</param>
        /// <param name="other">The second byte array to check.</param>
        /// <returns><c>true</c> if the contents of the two byte arrays are equal.</returns>
        public static bool IsEqualTo(this IList<byte> bytes, params byte[] other) =>
            IsEqualTo(bytes, (IList<byte>)other);

        /// <summary>
        ///     Indicates whether the specified byte array is null, does not contain any elements or consists of only
        ///     zero value items.
        /// </summary>
        /// <param name="bytes">The byte array to test.</param>
        /// <returns>
        ///     <c>true</c> if the byte array is null, does not contain any elements or consists exclusively of zero
        ///     value items.
        /// </returns>
        public static bool IsNullOrZeroed(this IList<byte> bytes)
        {
            if (bytes == null || bytes.Count == 0)
                return true;
            return bytes.All(b => b == 0);
        }

        /// <summary>
        ///     Indicates whether the specified byte array is does not contain any elements or consists of only zero
        ///     value items.
        /// </summary>
        /// <param name="bytes">The byte array to test.</param>
        /// <returns>
        ///     <c>true</c> if the byte array is null, does not contain any elements or consists exclusively of zero
        ///     value items.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the byte array is <c>null</c>.</exception>
        public static bool IsZeroed(this IList<byte> bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            return bytes.All(b => b == 0);
        }

        /// <summary>
        ///     Creates a string from a byte array that concatenates each item in the array, separated by
        ///     the specified delimiter.
        /// </summary>
        /// <param name="source"> The byte array from which to create the string. </param>
        /// <param name="delimiter"> The optional delimiter to separate each item in the array. </param>
        /// <returns> The combined string </returns>
        public static string ToString(this IList<byte> source, string delimiter)
        {
            if (source == null)
                return null;
            if (source.Count == 0)
                return string.Empty;

            if (delimiter == null)
                throw new ArgumentNullException(nameof(delimiter));
            bool useDelimiter = delimiter.Length > 0;

            var result = new StringBuilder(source[0].ToString(CultureInfo.InvariantCulture),
                (source.Count * 3) + ((source.Count - 1) * delimiter.Length));
            for (int i = 1; i < source.Count; i++)
            {
                if (useDelimiter)
                    result.Append(delimiter);
                result.Append(source[i]);
            }
            return result.ToString();
        }

        /// <summary>
        ///     Retrieves the bytes from a byte array upto a specific sequence.
        /// </summary>
        /// <param name="source">The byte array.</param>
        /// <param name="start">The index in the array to start checking from.</param>
        /// <param name="sequence">The sequence to search for.</param>
        /// <returns>An array of bytes from the starting index to the matching sequence.</returns>
        public static byte[] GetBytesUptoSequence(this byte[] source, int start, params byte[] sequence)
        {
            int sequenceIndex = IndexOfSequence(source, start, source.Length - start + 1, sequence);
            if (sequenceIndex == -1)
                return null;

            byte[] result = new byte[sequenceIndex - start];
            Array.Copy(source, start, result, 0, result.Length);
            return result;
        }

        public static int IndexOfSequence(this IList<byte> source, params byte[] sequence) =>
            IndexOfSequence(source, 0, source != null ? source.Count : 0, (IList<byte>)sequence);

        public static int IndexOfSequence(this IList<byte> source, int start, int count, params byte[] sequence) =>
            IndexOfSequence(source, start, count, (IList<byte>)sequence);

        public static int IndexOfSequence(this IList<byte> source, int start, int count, IList<byte> sequence)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            int sequenceIndex = 0;
            int endIndex = Math.Min(source.Count, start + count);
            for (int byteIdx = start; byteIdx < endIndex; byteIdx++)
            {
                if (source[byteIdx] == sequence[sequenceIndex])
                {
                    sequenceIndex++;
                    if (sequenceIndex >= sequence.Count)
                        return byteIdx - sequence.Count + 1;
                }
                else
                    sequenceIndex = 0;
            }
            return -1;
        }

        public static int[] IndexOfSequences(this IList<byte> source, params byte[] sequence) =>
            IndexOfSequences(source, 0, source != null ? source.Count : 0, sequence);

        public static int[] IndexOfSequences(this IList<byte> source, int start, int count, params byte[] sequence) =>
            IndexOfSequences(source, start, count, (IList<byte>)sequence);

        public static int[] IndexOfSequences(this IList<byte> source, int start, int count, IList<byte> sequence)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            var locations = new List<int>();

            int sequenceLocation = IndexOfSequence(source, start, count, sequence);
            while (sequenceLocation >= 0)
            {
                locations.Add(sequenceLocation);
                count -= sequenceLocation - start + 1;
                start = sequenceLocation + sequence.Count;
                sequenceLocation = IndexOfSequence(source, start, count, sequence);
            }

            return locations.ToArray();
        }

        public static byte[][] SplitBySequence(this byte[] source, params byte[] sequence) =>
            SplitBySequence(source, 0, source.Length, sequence);

        public static byte[][] SplitBySequence(this byte[] source, int start, int count, params byte[] sequence)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            if (sequence.Length == 0)
                throw new ArgumentException("Sequence cannot be empty.", nameof(sequence));

            if (start + count > source.Length)
                count = source.Length - start;

            int[] locations = IndexOfSequences(source, start, count, sequence);
            if (locations.Length == 0)
                return new[] { source };

            var results = new List<byte[]>(locations.Length + 1);
            for (int locationIdx = 0; locationIdx < locations.Length; locationIdx++)
            {
                int startIndex = locationIdx > 0 ? locations[locationIdx - 1] + sequence.Length : start;
                int endIndex = locations[locationIdx] - 1;
                if (endIndex < startIndex)
                    results.Add(new byte[0]);
                else
                {
                    byte[] splitBytes = new byte[endIndex - startIndex + 1];
                    Array.Copy(source, startIndex, splitBytes, 0, splitBytes.Length);
                    results.Add(splitBytes);
                }
            }

            if (locations[locations.Length - 1] + sequence.Length > start + count - 1)
                results.Add(new byte[0]);
            else
            {
                byte[] splitBytes = new byte[start + count - locations[locations.Length - 1] - sequence.Length];
                Array.Copy(source, locations[locations.Length - 1] + sequence.Length, splitBytes, 0, splitBytes.Length);
                results.Add(splitBytes);
            }

            return results.ToArray();
        }
    }
}