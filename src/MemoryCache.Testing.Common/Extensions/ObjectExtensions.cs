using System;

namespace MemoryCache.Testing.Common.Extensions {
    /// <summary>
    ///     Extensions for types.
    /// </summary>
    public static class TypeExtensions {
        /// <summary>
        ///     Gets the default value for the specified type.
        /// </summary>
        /// <param name="type">The type instance.</param>
        /// <returns>The default value for the specified type.</returns>
        public static object GetDefaultValue(this Type type) {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}