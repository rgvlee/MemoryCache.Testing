using System;
using MemoryCache.Testing.Common;
using MemoryCache.Testing.Common.Extensions;
using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace MemoryCache.Testing.NSubstitute.Extensions
{
    /// <summary>
    ///     Extensions for mocks.
    /// </summary>
    public static class MockExtensions
    {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(MockExtensions));

        /// <summary>
        ///     Sets up a cache entry.
        /// </summary>
        /// <param name="mockedMemoryCache">The mocked memory cache.</param>
        /// <param name="cacheEntryKey">The cache entry key.</param>
        /// <param name="cacheEntryValue">The cache entry value.</param>
        /// <returns>The mocked memory cache.</returns>
        [Obsolete("Access to this method will be removed in a future version. Use the mocked memory cache to maintain cache entries.")]
        public static IMemoryCache SetUpCacheEntry(this IMemoryCache mockedMemoryCache, object cacheEntryKey, object cacheEntryValue)
        {
            EnsureArgument.IsNotNull(mockedMemoryCache, nameof(mockedMemoryCache));
            EnsureArgument.IsNotNull(cacheEntryKey, nameof(cacheEntryKey));

            Logger.LogDebug($"Setting up cache entry for '{cacheEntryKey}' (type: {cacheEntryValue.GetType().Name}; value: '{cacheEntryValue}')");

            mockedMemoryCache.SetUpCacheEntryGet(cacheEntryKey, cacheEntryValue);

            mockedMemoryCache.SetUpCacheEntryRemove(cacheEntryKey, cacheEntryValue.GetType().GetDefaultValue());

            return mockedMemoryCache;
        }

        /// <summary>
        ///     Sets up the TryGetValue method for a cache entry.
        /// </summary>
        /// <param name="mockedMemoryCache">The mocked memory cache.</param>
        /// <param name="cacheEntryKey">The cache entry key.</param>
        /// <param name="cacheEntryValue">The cache entry value.</param>
        /// <returns>The mocked memory cache.</returns>
        /// <remarks>
        ///     I've left this accessible for advanced usage. In most cases you should just use <see cref="SetUpCacheEntry" />.
        /// </remarks>
        [Obsolete("Access to this method will be removed in a future version. Use the mocked memory cache to maintain cache entries.")]
        public static IMemoryCache SetUpCacheEntryGet(this IMemoryCache mockedMemoryCache, object cacheEntryKey, object cacheEntryValue)
        {
            EnsureArgument.IsNotNull(mockedMemoryCache, nameof(mockedMemoryCache));
            EnsureArgument.IsNotNull(cacheEntryKey, nameof(cacheEntryKey));

            Logger.LogDebug($"Setting up cache entry Get for '{cacheEntryKey}'");

            mockedMemoryCache.TryGetValue(Arg.Is<object>(k => k.Equals(cacheEntryKey)), out Arg.Any<object>())
                .Returns(x =>
                {
                    x[1] = cacheEntryValue;
                    return true;
                })
                .AndDoes(x => Logger.LogDebug("Cache TryGetValue invoked"));

            return mockedMemoryCache;
        }

        /// <summary>
        ///     Sets up the Remove method for a cache entry.
        /// </summary>
        /// <param name="mockedMemoryCache">The mocked memory cache.</param>
        /// <param name="cacheEntryKey">The cache entry key.</param>
        /// <param name="defaultValue">The default value (e.g., default(T)) for the cache entry value.</param>
        /// <returns>The mocked memory cache.</returns>
        /// <remarks>
        ///     I've left this accessible for advanced usage. In most cases you should just use <see cref="SetUpCacheEntry" />.
        /// </remarks>
        [Obsolete("Access to this method will be removed in a future version. Use the mocked memory cache to maintain cache entries.")]
        public static IMemoryCache SetUpCacheEntryRemove(this IMemoryCache mockedMemoryCache, object cacheEntryKey, object defaultValue)
        {
            EnsureArgument.IsNotNull(mockedMemoryCache, nameof(mockedMemoryCache));
            EnsureArgument.IsNotNull(cacheEntryKey, nameof(cacheEntryKey));

            Logger.LogDebug($"Setting up cache entry Remove for '{cacheEntryKey}' (default value: {defaultValue})");

            mockedMemoryCache.When(x => x.Remove(Arg.Is<object>(k => k.Equals(cacheEntryKey))))
                .Do(x =>
                {
                    Logger.LogDebug("Cache Remove invoked");
                    mockedMemoryCache.SetUpCacheEntryGet(cacheEntryKey, defaultValue);
                });

            return mockedMemoryCache;
        }
    }
}