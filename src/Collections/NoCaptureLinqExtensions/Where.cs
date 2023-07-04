// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
#if EXPLICIT
namespace Collections.Net.Extensions.EnumerableExtensions.NoCapture;
#else
namespace System.Collections.Generic;
#endif

public static partial class NoCaptureLinqExtensions
{
    public static IEnumerable<T> Where<T, TArg>(this IEnumerable<T> source, TArg arg,
        Func<T, TArg, bool> predicate)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        foreach (T element in source)
        {
            if (predicate(element, arg))
                yield return element;
        }
    }

    public static IEnumerable<T> Where<T, TArg1, TArg2>(this IEnumerable<T> source, TArg1 arg1, TArg2 arg2,
        Func<T, TArg1, TArg2, bool> predicate)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        foreach (T element in source)
        {
            if (predicate(element, arg1, arg2))
                yield return element;
        }
    }
}
