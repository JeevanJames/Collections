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
namespace Collections.Net.Object
#else
namespace System.Collections.Generic
#endif
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Given a self-referencing object, this method traverses up the parent chain until the
        ///     oldest parent is reached or the specified <paramref name="stopCondition"/> is met.
        /// </summary>
        /// <typeparam name="T">Type of the self-referencing object.</typeparam>
        /// <param name="start">The object to start traversing from.</param>
        /// <param name="parentSelector">
        ///     Delegate that accepts an instance of <typeparamref name="T"/> and returns it's parent
        ///     instance.
        /// </param>
        /// <param name="stopCondition">Optional predicate to stop the traversal.</param>
        /// <param name="skipStart">
        ///     If <c>true</c>, skips the <paramref name="start"/> instance during traversal.
        /// </param>
        /// <returns>The sequence of instances traversed.</returns>
        public static IEnumerable<T> ParentChain<T>(this T start,
            Func<T, T> parentSelector,
            Func<T, bool> stopCondition = null,
            bool skipStart = false)
            where T : class
        {
            if (parentSelector is null)
                throw new ArgumentNullException(nameof(parentSelector));

            if (start is null)
                yield break;

            T current = skipStart ? parentSelector(start) : start;
            while (current != null)
            {
                if (stopCondition != null && stopCondition(current))
                    yield break;

                yield return current;
                current = parentSelector(current);
            }
        }

        public static IEnumerable<T> ParentChainReverse<T>(this T start,
            Func<T, T> parentSelector,
            Func<T, bool> stopCondition = null,
            bool skipStart = false)
            where T : class
        {
            var chain = ParentChain(start, parentSelector, skipStart: skipStart).Reverse();
            if (stopCondition != null)
                chain = chain.SkipWhile(item => !stopCondition(item));
            return chain;
        }

        public static T FindParent<T>(this T start, Func<T, T> parentSelector, Func<T, bool> predicate)
            where T : class
        {
            if (start is null)
                throw new ArgumentNullException(nameof(start));
            if (parentSelector is null)
                throw new ArgumentNullException(nameof(parentSelector));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            T current = parentSelector(start);
            while (current != null)
            {
                if (predicate(current))
                    return current;
                current = parentSelector(current);
            }

            return null;
        }

        public static T FindRootParent<T>(this T start, Func<T, T> parentSelector)
            where T : class
        {
            if (start is null)
                throw new ArgumentNullException(nameof(start));
            if (parentSelector is null)
                throw new ArgumentNullException(nameof(parentSelector));

            T current = start;
            T parent = parentSelector(current);
            while (parent != null)
            {
                current = parent;
                parent = parentSelector(current);
            }

            return current;
        }
    }
}
