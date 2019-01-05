using System.Linq;
using System.Reflection;

namespace System.Collections.Generic
{
    /// <summary>
    ///     Provides iterators for enum types. Can be used in a LINQ expression.
    /// </summary>
    public static class EnumIterator
    {
        /// <summary>
        ///     Generates an iterator for the enum type specified by the TEnum generic parameter.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to generate the iterator for</typeparam>
        /// <returns>An generic iterator that can iterate over the values of TEnum</returns>
        public static IEnumerable<TEnum> For<TEnum>()
            where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        /// <summary>
        ///     Generates an iterator for the enum type specified by the TEnum generic parameter.
        /// </summary>
        /// <param name="enumType">The enum type to generate the iterator for</param>
        /// <returns>A non-generic iterator that can iterate over the values of the enum</returns>
        /// <exception cref="ArgumentNullException">Thrown when the specified type is null</exception>
        /// <exception cref="ArgumentException">Thrown when the specified type is not an enum</exception>
        public static IEnumerable For(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));
#if NETSTANDARD1_3
            if (!enumType.GetTypeInfo().IsEnum)
#else
            if (!enumType.IsEnum)
#endif
                throw new ArgumentException("enumType must be an enum", nameof(enumType));
            return Enum.GetValues(enumType);
        }
    }
}