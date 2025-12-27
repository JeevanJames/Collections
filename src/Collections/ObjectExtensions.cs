// Copyright (c) 2018-2023 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Collections.Net.Extensions.Objects;

public static class ObjectExtensions
{
    /// <summary>
    ///     Given a self-referencing object, this method traverses up the parent chain until the root
    ///     parent is reached or the specified <paramref name="stopCondition"/> is met.
    /// </summary>
    /// <typeparam name="T">Type of the self-referencing object.</typeparam>
    /// <param name="start">The object to start traversing from.</param>
    /// <param name="parentSelector">
    ///     Delegate that accepts an instance of <typeparamref name="T"/> and returns its parent instance.
    /// </param>
    /// <param name="stopCondition">Optional predicate to stop the traversal.</param>
    /// <param name="skipStart">
    ///     If <c>true</c>, skips the <paramref name="start"/> instance during traversal.
    /// </param>
    /// <returns>The sequence of instances traversed.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="parentSelector"/> is <c>null</c>.
    /// </exception>
    public static IEnumerable<T> EnumerateParentChain<T>(this T? start,
        Func<T, T?> parentSelector,
        Func<T, bool>? stopCondition = null,
        bool skipStart = false)
        where T : class
    {
        if (parentSelector is null)
            throw new ArgumentNullException(nameof(parentSelector));

        if (start is null)
            yield break;

        T? current = skipStart ? parentSelector(start) : start;
        while (current is not null)
        {
            if (stopCondition is not null && stopCondition(current))
                yield break;

            yield return current;
            current = parentSelector(current);
        }
    }

    /// <summary>
    ///     Given a self-referencing object, this method traverses from the root object till the current
    ///     object or until the specified <paramref name="stopCondition"/> is met.
    /// </summary>
    /// <typeparam name="T">Type of the self-referencing object.</typeparam>
    /// <param name="start">The object to traverse up to.</param>
    /// <param name="parentSelector">
    ///     Delegate that accepts an instance of <typeparamref name="T"/> and returns its parent instance.
    /// </param>
    /// <param name="stopCondition">Optional predicate to stop the traversal.</param>
    /// <param name="skipStart">
    ///     If <c>true</c>, skips the <paramref name="start"/> instance during traversal.
    /// </param>
    /// <returns>The sequence of instances traversed.</returns>
    public static IEnumerable<T> EnumerateParentChainReverse<T>(this T? start,
        Func<T, T?> parentSelector,
        Func<T, bool>? stopCondition = null,
        bool skipStart = false)
        where T : class
    {
        IEnumerable<T> chain = EnumerateParentChain(start, parentSelector, skipStart: skipStart).Reverse();
        if (stopCondition is not null)
            chain = chain.SkipWhile(item => !stopCondition(item));
        return chain;
    }

    /// <summary>
    ///     Given a self-referencing object (<paramref name="start"/>), traverse up the parent chain
    ///     until the first object satisfying the specified <paramref name="predicate"/> is found.
    /// </summary>
    /// <typeparam name="T">Type of the self-referencing object.</typeparam>
    /// <param name="start">The object to start traversing from.</param>
    /// <param name="parentSelector">
    ///     Delegate that accepts an instance of <typeparamref name="T"/> and returns it's parent
    ///     instance.
    /// </param>
    /// <param name="predicate">
    ///     Delegate that specifies the condition for the object to meet.
    /// </param>
    /// <returns>The first matching object, if found; otherwise <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="start"/>, <paramref name="parentSelector"/> or
    ///     <paramref name="predicate"/> is <c>null</c>.
    /// </exception> 
    public static T? FindParent<T>(this T start, Func<T, T?> parentSelector, Func<T, bool> predicate)
        where T : class
    {
        if (start is null)
            throw new ArgumentNullException(nameof(start));
        if (parentSelector is null)
            throw new ArgumentNullException(nameof(parentSelector));
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        T? current = parentSelector(start);
        while (current is not null)
        {
            if (predicate(current))
                return current;
            current = parentSelector(current);
        }

        return null;
    }

    /// <summary>
    ///     Given a self-referencing object (<paramref name="start"/>), traverses up the parent chain
    ///     and returns the root object (whose parent is <c>null</c>).
    /// </summary>
    /// <typeparam name="T">Type of the self-referencing object.</typeparam>
    /// <param name="start">The object to start traversing from.</param>
    /// <param name="parentSelector">
    ///     Delegate that accepts an instance of <typeparamref name="T"/> and returns it's parent
    ///     instance.
    /// </param>
    /// <returns>The root object.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="start"/> or <paramref name="parentSelector"/> is <c>null</c>.
    /// </exception>
    public static T FindRootParent<T>(this T start, Func<T, T?> parentSelector)
        where T : class
    {
        if (start is null)
            throw new ArgumentNullException(nameof(start));
        if (parentSelector is null)
            throw new ArgumentNullException(nameof(parentSelector));

        T current = start;
        T? parent = parentSelector(current);
        while (parent is not null)
        {
            current = parent;
            parent = parentSelector(current);
        }

        return current;
    }

    /// <summary>
    ///     Returns an <see cref="IEnumerable{T}"/> from a single object.
    /// </summary>
    /// <typeparam name="T">The type of the single object.</typeparam>
    /// <param name="instance">The single object.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains the single object.</returns>
    public static IEnumerable<T> AsEnumerable<T>(this T instance)
    {
        yield return instance;
    }

    /// <summary>
    ///     Returns an <see cref="IList{T}"/> that contains the single <paramref name="instance"/>.
    /// </summary>
    /// <typeparam name="T">The type of the single object.</typeparam>
    /// <param name="instance">The single object.</param>
    /// <returns>A <see cref="IList{T}"/> that contains the single <paramref name="instance"/>.</returns>
    public static IList<T> AsListCollection<T>(this T instance) => new List<T>(1) { instance };

    /// <summary>
    ///     Returns a new <see cref="List{T}"/> that contains the single <paramref name="instance"/>.
    /// </summary>
    /// <typeparam name="T">The type of the single object.</typeparam>
    /// <param name="instance">The single object.</param>
    /// <returns>A new <see cref="List{T}"/> that contains the single <paramref name="instance"/>.</returns>
    public static List<T> AsConcreteList<T>(this T instance) => new(1) { instance };

    /// <summary>
    ///     Returns a new <see cref="IReadOnlyList{T}"/> that contains the single <paramref name="instance"/>.
    /// </summary>
    /// <typeparam name="T">The type of the single object.</typeparam>
    /// <param name="instance">The single object.</param>
    /// <returns>
    ///     A new <see cref="IReadOnlyList{T}"/> that contains the single <paramref name="instance"/>.
    /// </returns>
    public static IReadOnlyList<T> AsReadOnlyListCollection<T>(this T instance) =>
        new ReadOnlyCollection<T>(new[] { instance });

    /// <summary>
    ///     Returns a new array that contains the single <paramref name="instance"/>.
    /// </summary>
    /// <typeparam name="T">The type of the single object.</typeparam>
    /// <param name="instance">The single object.</param>
    /// <returns>A new <see cref="List{T}"/> that contains the single <paramref name="instance"/>.</returns>
    public static T[] AsArrayCollection<T>(this T instance) => new[] { instance };
}
