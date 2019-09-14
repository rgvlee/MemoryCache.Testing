﻿using MemoryCache.Testing.Common.Extensions;
using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace MemoryCache.Testing.Moq.Extensions {
    /// <summary>
    ///     Extensions for mocks.
    /// </summary>
    public static class MockExtensions {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(MockExtensions));

        /// <summary>
        ///     Sets up a cache entry.
        /// </summary>
        /// <param name="mockedMemoryCache">The mocked memory cache.</param>
        /// <param name="cacheEntryKey">The cache entry key.</param>
        /// <param name="cacheEntryValue">The cache entry value.</param>
        /// <returns>The mocked memory cache.</returns>
        public static IMemoryCache SetUpCacheEntry(this IMemoryCache mockedMemoryCache, object cacheEntryKey, object cacheEntryValue) {
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
        public static IMemoryCache SetUpCacheEntryGet(this IMemoryCache mockedMemoryCache, object cacheEntryKey, object cacheEntryValue) {
            Logger.LogDebug($"Setting up cache entry Get for '{cacheEntryKey}'");

            Mock.Get(mockedMemoryCache).Setup(m => m.TryGetValue(It.Is<object>(k => k.Equals(cacheEntryKey)), out cacheEntryValue))
                .Callback(() => Logger.LogDebug("Cache TryGetValue invoked"))
                .Returns(true);

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
        public static IMemoryCache SetUpCacheEntryRemove(this IMemoryCache mockedMemoryCache, object cacheEntryKey, object defaultValue) {
            Logger.LogDebug($"Setting up cache entry Remove for '{cacheEntryKey}' (default value: {defaultValue})");

            Mock.Get(mockedMemoryCache).Setup(m => m.Remove(It.Is<object>(k => k.Equals(cacheEntryKey))))
                .Callback(() => {
                    Logger.LogDebug("Cache Remove invoked");
                    mockedMemoryCache.SetUpCacheEntryGet(cacheEntryKey, defaultValue);
                });

            return mockedMemoryCache;
        }
    }
}