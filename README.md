# Collections.NET [![Build status](https://img.shields.io/appveyor/ci/JeevanJames/collections.svg)](https://ci.appveyor.com/project/JeevanJames/collections/branch/master) [![Test status](https://img.shields.io/appveyor/tests/JeevanJames/collections.svg)](https://ci.appveyor.com/project/JeevanJames/collections/branch/master) [![codecov](https://codecov.io/gh/JeevanJames/Collections/branch/master/graph/badge.svg)](https://codecov.io/gh/JeevanJames/Collections) [![NuGet Version](http://img.shields.io/nuget/v/Collections.NET.svg?style=flat)](https://www.nuget.org/packages/Collections.NET/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Collections.NET.svg)](https://www.nuget.org/packages/Collections.NET/)

Collections.NET is a set of collection extensions and custom collections.

All code in this package is under the `System.Collections.Generic` namespace, so you automatically have access to the extension methods simply by using the namespace.

## Nuget installation
Install using package manager:
```ps
PM> Install-Package Collections.NET
```

Install using `dotnet` CLI:
```sh
> dotnet add package Collections.NET
```

## Collection extensions
Extension methods for common collection interfaces like `IEnumerable<T>`, `ICollection<T>` and `IList<T>`.

|Method name|Interface|Description|
|-----------|---------|-----------|
|`AddRange`|`ICollection<T>`|Adds one or more items to a collection in a single operation. Inspired by the `List<T>.AddRange` method.|
|`Chunk`|`IEnumerable<T>`|Splits a collection into chunks of a specified size.|
|`Fill`|`IList<T>`|Sets all elements in a collection to a specific value.|
|`ForEach`|`IEnumerable<T>`|Iterates over each element in a collection and calls the specified delegate for each item. Inspired by the `List<T>.ForEach` method.|
|`IndexOf`|`IList<T>`|Finds the index of the first matching element in a collection based on the specified predicate.|
|`IndexOfAll`|`IList<T>`|Finds all indices of the matching elements in a collection based on the specified predicate.|
|`IsEmpty`|`IEnumerable<T>`|Indicates whether a collection is empty. A better way of doing `if !collection.Any()`|
|`IsNullOrEmpty`|`IEnumerable<T>`|Indicates whether a collection is `null` or empty.|
|`LastIndexOf`|`IList<T>`|Finds the index of the last matching element in a collection based on the specified predicate.|
|`None`|`IEnumerable<T>`|Determines whether none of the elements in a collection satisfies the specified predicate. Opposite of the LINQ `All` extension method.|
|`Range`|`ICollection<T>`|Returns an iterator for the elements in the collection from the specified start index to the end index.|
|`RemoveAll`|`IList<T>`|Removes all elements from a collection that satisfies the specified predicate.|
|`RemoveFirst`|`IList<T>`|Removes the first element from a collection that satisfies the specified predicate.|
|`RemoveLast`|`IList<T>`|Removes the last elements from a collection that satisfies the specified predicate.|
|`Repeat`|`IEnumerable<T>`|Creates a collection that contains the specified collection repeated a specified number of times.|
|`ShuffleInplace`|`IList<T>`|Shuffles the elements of a collection.|
|`ToArray`|`IEnumerable<T>`|Creates an array from a collection. Overloads available to select the elements from the collection to be included in the array based on a predicate, and to convert the elements to a different type.|
|`ToList`|`IEnumerable<T>`|Creates a `List<T>` from a collection. Overloads available to select the elements from the collection to be included in the array based on a predicate, and to convert the elements to a different type.|
|`WhereNot`|`IEnumerable<T>`|Filters a collection on the values that do not match the specified predicate. Inverse of the LINQ `Where` method.|

## Byte array extensions
Extension methods that provide commonly-used operations on `byte` arrays.

> Since .NET arrays implement `IList<T>`, most of the extension methods operate on `IList<byte>` rather than just `byte[]`. The documentation will continue to refer to them as byte arrays.

|Method name|Description|
|-----------|-----------|
|`IsEqualTo`|Checks whether two byte collections contain the same data.|
|`IsNullOrZeroed`|Indicates whether a byte collection is `null`, empty or contains only zeroes.|
|`IsZeroed`|Indicates whether a byte collection is empty or contains only zeroes.|
|`ToString`|Joins all values in a byte collection using a specified delimiter.|

### Byte sequence operations
The byte array extensions also provide a number of methods to deal with sequences of bytes:

|Method name|Description|
|-----------|-----------|
|`GetBytesUptoSequence`|Gets all the bytes in a byte array up to the specified byte sequence.|
|`IndexOfSequence`|Gets the index of the first occurence of the specified byte sequence in a byte array.|
|`IndexOfSequences`|Gets all indices of the occurences of the specified byte sequence in a byte array.|
|`SplitBySequence`|Splits a byte array by the specified byte sequence.|

## `EnumIterator` class
The `EnumIterator` class provides two static methods to create an iterator for the values of an `enum`.

```cs
// First overload - takes a generic type parameter for the enum type
foreach (DayOfWeek day in EnumIterator.For<DayOfWeek>())
{
    Console.WriteLine(day);
}

// Second overload - non-generic method that accepts Type parameter.
List<DayOfWeek> days = EnumIterator.For(typeof(DayOfWeek)).ToList();
days.ForEach(Console.WriteLine);
```
