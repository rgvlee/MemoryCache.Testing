using MemoryCache.Testing.Common.Extensions;
using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace MemoryCache.Testing.Moq.Extensions {
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
        public static Mock<IMemoryCache> SetUpCacheEntry(this Mock<IMemoryCache> memoryCacheMock, object cacheEntryKey, object cacheEntryValue) {
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
        public static Mock<IMemoryCache> SetUpCacheEntryGet(this Mock<IMemoryCache> memoryCacheMock, object cacheEntryKey, object cacheEntryValue) {
            Logger.LogDebug($"Setting up cache entry Get for '{cacheEntryKey}'");
            
            memoryCacheMock.Setup(m => m.TryGetValue(It.Is<object>(k => k.Equals(cacheEntryKey)), out cacheEntryValue))
                .Callback(() => Logger.LogDebug("Cache TryGetValue invoked"))
                .Returns(true);

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
        public static Mock<IMemoryCache> SetUpCacheEntryRemove(this Mock<IMemoryCache> memoryCacheMock, object cacheEntryKey, object defaultValue) {
            Logger.LogDebug($"Setting up cache entry Remove for '{cacheEntryKey}' (default value: {defaultValue})");

            memoryCacheMock.Setup(m => m.Remove(It.Is<object>(k => k.Equals(cacheEntryKey))))
                .Callback(() => {
                    Logger.LogDebug("Cache Remove invoked");
                    memoryCacheMock.SetUpCacheEntryGet(cacheEntryKey, defaultValue);
                });

            return memoryCacheMock;
        }
    }
}
