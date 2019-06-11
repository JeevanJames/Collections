﻿#region --- License & Copyright Notice ---
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
    public static class ObjectExtensions
    {
        public static IEnumerable<T> ParentChain<T>(this T start, Func<T, T> parentSelector, Func<T, bool> stopCondition = null, bool skipStart = false)
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

        public static IEnumerable<T> ParentChainReverse<T>(this T start, Func<T, T> parentSelector, Func<T, bool> stopCondition = null, bool skipStart = false)
            where T : class
        {
            if (stopCondition is null)
                return ParentChain(start, parentSelector, skipStart: skipStart).Reverse();

            throw new NotImplementedException();
        }
    }
}