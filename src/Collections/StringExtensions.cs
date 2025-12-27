// Copyright (c) 2018-2026 Jeevan James
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Collections.Net.Extensions.Strings;

public static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string AssertNotNullOrEmpty(this string? str,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(str))] string paramName = "")
#else
        string paramName)
#endif
    {
        if (str is null)
            throw new ArgumentNullException(paramName);
        if (str.Length == 0)
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);
        return str;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string AssertNotNullOrWhitespace(this string? str,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(str))] string paramName = "")
#else
        string paramName)
#endif
    {
        if (str is null)
            throw new ArgumentNullException(paramName);
        if (str.Length == 0)
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);

        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsWhiteSpace(str[i]))
                return str;
        }

        throw new ArgumentException($"{paramName} cannot be whitespace.", paramName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EmptyAsNull(this string? str) =>
        str is null || str.Length == 0 ? null : str;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EmptyOrWhitespaceAsNull(this string? str)
    {
        if (str is null || str.Length == 0)
            return null;

        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsWhiteSpace(str[i]))
                return str;
        }

        return null;
    }

    public static bool In(this string? str, IReadOnlyList<string> checkStrs,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (checkStrs is null)
            throw new ArgumentNullException(nameof(checkStrs));

        for (int i = 0; i < checkStrs.Count; i++)
        {
            if (string.Equals(str, checkStrs[i], comparison))
                return true;
        }

        return false;
    }

    public static bool In(this string? str, IEnumerable<string> checkStrs,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (checkStrs is null)
            throw new ArgumentNullException(nameof(checkStrs));

        if (checkStrs is IReadOnlyList<string> list)
            return str.In(list, comparison);

        foreach (string checkStr in checkStrs)
        {
            if (string.Equals(str, checkStr, comparison))
                return true;
        }

        return false;
    }

    public static bool StartsWith(this string str, IReadOnlyList<string> checkStrs,
        StringComparison comparison = StringComparison.Ordinal)
    {
        str.AssertNotNullOrEmpty(nameof(str));
        if (checkStrs is null)
            throw new ArgumentNullException(nameof(checkStrs));

        for (int i = 0; i < checkStrs.Count; i++)
        {
            if (str.StartsWith(checkStrs[i], comparison))
                return true;
        }

        return false;
    }

    public static bool StartsWith(this string str, IEnumerable<string> checkStrs,
        StringComparison comparison = StringComparison.Ordinal)
    {
        str.AssertNotNullOrEmpty(nameof(str));
        if (checkStrs is null)
            throw new ArgumentNullException(nameof(checkStrs));

        if (checkStrs is IReadOnlyList<string> list)
            return str.StartsWith(list, comparison);

        foreach (string checkStr in checkStrs)
        {
            if (str.StartsWith(checkStr, comparison))
                return true;
        }

        return false;
    }

    public static bool EndsWith(this string str, IReadOnlyList<string> checkStrs,
        StringComparison comparison = StringComparison.Ordinal)
    {
        str.AssertNotNullOrEmpty(nameof(str));
        if (checkStrs is null)
            throw new ArgumentNullException(nameof(checkStrs));

        for (int i = 0; i < checkStrs.Count; i++)
        {
            if (str.EndsWith(checkStrs[i], comparison))
                return true;
        }

        return false;
    }

    public static bool EndsWith(this string str, IEnumerable<string> checkStrs,
        StringComparison comparison = StringComparison.Ordinal)
    {
        str.AssertNotNullOrEmpty(nameof(str));
        if (checkStrs is null)
            throw new ArgumentNullException(nameof(checkStrs));

        if (checkStrs is IReadOnlyList<string> list)
            return str.EndsWith(list, comparison);

        foreach (string checkStr in checkStrs)
        {
            if (str.StartsWith(checkStr, comparison))
                return true;
        }

        return false;
    }
}
