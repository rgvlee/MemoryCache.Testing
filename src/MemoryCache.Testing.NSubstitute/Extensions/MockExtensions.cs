using MemoryCache.Testing.Common.Extensions;
using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace MemoryCache.Testing.NSubstitute.Extensions {
    /// <summary>
    /// Extensions for mocks.
    /// </summary>
    public static class MockExtensions {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(MockExtensions));

        /// <summary>
        /// Sets up a cache entry.
        /// </summary>
        /// <param name="memoryCacheMock">The memory cache mock instance.</param>
        /// <param name="cacheEntryKey">The cache entry key.</param>
        /// <param name="cacheEntryValue">The cache entry value.</param>
        /// <returns>The memory cache mock.</returns>
        public static IMemoryCache SetUpCacheEntry(this IMemoryCache memoryCacheMock, object cacheEntryKey, object cacheEntryValue) {
            Logger.LogDebug($"Setting up cache entry for '{cacheEntryKey}' (type: {cacheEntryValue.GetType().Name}; value: '{cacheEntryValue}')");

            memoryCacheMock.SetUpCacheEntryGet(cacheEntryKey, cacheEntryValue);

            memoryCacheMock.SetUpCacheEntryRemove(cacheEntryKey, cacheEntryValue.GetType().GetDefaultValue());

            return memoryCacheMock;
        }
        
        /// <summary>
        /// Sets up the TryGetValue method for a cache entry.
        /// </summary>
        /// <param name="memoryCacheMock">The memory cache mock instance.</param>
        /// <param name="cacheEntryKey">The cache entry key.</param>
        /// <param name="cacheEntryValue">The cache entry value.</param>
        /// <returns>The memory cache mock.</returns>
        /// <remarks>I've left this accessible for advanced usage. In most cases you should just use <see cref="SetUpCacheEntry"/>.</remarks>
        public static IMemoryCache SetUpCacheEntryGet(this IMemoryCache memoryCacheMock, object cacheEntryKey, object cacheEntryValue) {
            Logger.LogDebug($"Setting up cache entry Get for '{cacheEntryKey}'");
            
            memoryCacheMock.TryGetValue(Arg.Is<object>(k => k.Equals(cacheEntryKey)), out Arg.Any<object>())
                .Returns(x => {
                    x[1] = cacheEntryValue;
                    return true;
                })
                .AndDoes(x => Logger.LogDebug("Cache TryGetValue invoked"));

            return memoryCacheMock;
        }

        /// <summary>
        /// Sets up the Remove method for a cache entry.
        /// </summary>
        /// <param name="memoryCacheMock">The memory cache mock instance.</param>
        /// <param name="cacheEntryKey">The cache entry key.</param>
        /// <param name="defaultValue">The default value (e.g., default(T)) for the cache entry value.</param>
        /// <returns>The memory cache mock.</returns>
        /// <remarks>I've left this accessible for advanced usage. In most cases you should just use <see cref="SetUpCacheEntry"/>.</remarks>
        public static IMemoryCache SetUpCacheEntryRemove(this IMemoryCache memoryCacheMock, object cacheEntryKey, object defaultValue) {
            Logger.LogDebug($"Setting up cache entry Remove for '{cacheEntryKey}' (default value: {defaultValue})");

            memoryCacheMock.When(x => x.Remove(Arg.Is<object>(k => k.Equals(cacheEntryKey))))
                .Do(x => {
                    Logger.LogDebug("Cache Remove invoked");
                    memoryCacheMock.SetUpCacheEntryGet(cacheEntryKey, defaultValue);
                });

            return memoryCacheMock;
        }
    }
}
