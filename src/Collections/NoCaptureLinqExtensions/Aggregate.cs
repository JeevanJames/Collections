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
    public static TSource Aggregate<TSource, TArg>(this IEnumerable<TSource> sequence,
        TArg arg,
        Func<TSource, TSource, TArg, TSource> func)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        using IEnumerator<TSource> e = sequence.GetEnumerator();

        if (!e.MoveNext())
            throw new InvalidOperationException("The sequence contains no elements to aggregate.");

        TSource result = e.Current;
        while (e.MoveNext())
            result = func(result, e.Current, arg);

        return result;
    }

    public static TSource Aggregate<TSource, TArg1, TArg2>(this IEnumerable<TSource> sequence,
        TArg1 arg1, TArg2 arg2,
        Func<TSource, TSource, TArg1, TArg2, TSource> func)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        using IEnumerator<TSource> e = sequence.GetEnumerator();

        if (!e.MoveNext())
            throw new InvalidOperationException("The sequence contains no elements to aggregate.");

        TSource result = e.Current;
        while (e.MoveNext())
            result = func(result, e.Current, arg1, arg2);

        return result;
    }

    public static TSource Aggregate<TSource, TArg1, TArg2, TArg3>(this IEnumerable<TSource> sequence,
        TArg1 arg1, TArg2 arg2, TArg3 arg3,
        Func<TSource, TSource, TArg1, TArg2, TArg3, TSource> func)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        using IEnumerator<TSource> e = sequence.GetEnumerator();

        if (!e.MoveNext())
            throw new InvalidOperationException("The sequence contains no elements to aggregate.");

        TSource result = e.Current;
        while (e.MoveNext())
            result = func(result, e.Current, arg1, arg2, arg3);

        return result;
    }

    public static TAccumulate Aggregate<TSource, TAccumulate, TArg>(this IEnumerable<TSource> sequence,
        TAccumulate seed,
        TArg arg,
        Func<TAccumulate, TSource, TArg, TAccumulate> func)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        TAccumulate result = seed;
        foreach (TSource element in sequence)
            result = func(result, element, arg);
        return result;
    }

    public static TAccumulate Aggregate<TSource, TAccumulate, TArg1, TArg2>(this IEnumerable<TSource> sequence,
        TAccumulate seed,
        TArg1 arg1, TArg2 arg2,
        Func<TAccumulate, TSource, TArg1, TArg2, TAccumulate> func)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        TAccumulate result = seed;
        foreach (TSource element in sequence)
            result = func(result, element, arg1, arg2);
        return result;
    }

    public static TAccumulate Aggregate<TSource, TAccumulate, TArg1, TArg2, TArg3>(this IEnumerable<TSource> sequence,
        TAccumulate seed,
        TArg1 arg1, TArg2 arg2, TArg3 arg3,
        Func<TAccumulate, TSource, TArg1, TArg2, TArg3, TAccumulate> func)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        TAccumulate result = seed;
        foreach (TSource element in sequence)
            result = func(result, element, arg1, arg2, arg3);
        return result;
    }

    public static TResult Aggregate<TSource, TAccumulate, TResult, TArg>(this IEnumerable<TSource> sequence,
        TAccumulate seed,
        TArg arg,
        Func<TAccumulate, TSource, TArg, TAccumulate> func,
        Func<TAccumulate, TArg, TResult> resultConverter)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));
        if (resultConverter is null)
            throw new ArgumentNullException(nameof(resultConverter));

        TAccumulate result = seed;
        foreach (TSource element in sequence)
            result = func(result, element, arg);
        return resultConverter(result, arg);
    }

    public static TResult Aggregate<TSource, TAccumulate, TResult, TArg1, TArg2>(this IEnumerable<TSource> sequence,
        TAccumulate seed,
        TArg1 arg1, TArg2 arg2,
        Func<TAccumulate, TSource, TArg1, TArg2, TAccumulate> func,
        Func<TAccumulate, TArg1, TArg2, TResult> resultConverter)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));
        if (resultConverter is null)
            throw new ArgumentNullException(nameof(resultConverter));

        TAccumulate result = seed;
        foreach (TSource element in sequence)
            result = func(result, element, arg1, arg2);
        return resultConverter(result, arg1, arg2);
    }

    public static TResult Aggregate<TSource, TAccumulate, TResult, TArg1, TArg2, TArg3>(
        this IEnumerable<TSource> sequence,
        TAccumulate seed,
        TArg1 arg1, TArg2 arg2, TArg3 arg3,
        Func<TAccumulate, TSource, TArg1, TArg2, TArg3, TAccumulate> func,
        Func<TAccumulate, TArg1, TArg2, TArg3,  TResult> resultConverter)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));
        if (func is null)
            throw new ArgumentNullException(nameof(func));
        if (resultConverter is null)
            throw new ArgumentNullException(nameof(resultConverter));

        TAccumulate result = seed;
        foreach (TSource element in sequence)
            result = func(result, element, arg1, arg2, arg3);
        return resultConverter(result, arg1, arg2, arg3);
    }
}
