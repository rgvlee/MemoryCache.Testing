using MemoryCache.Testing.Common.Extensions;
using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace MemoryCache.Testing.Moq.Extensions {
    public static class MockExtensions {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(MockExtensions));

        public static Mock<IMemoryCache> SetUpCacheEntry<T>(this Mock<IMemoryCache> memoryCacheMock, object cacheEntryKey, T cacheEntryValue) {
            Logger.LogDebug($"Setting up cache entry for '{cacheEntryKey}' (type: {cacheEntryValue.GetType().Name}; value: '{cacheEntryValue.ToString()}')");

            memoryCacheMock.SetUpCacheEntryCreate(cacheEntryKey, cacheEntryValue);

            memoryCacheMock.SetUpCacheEntryGet<T>(cacheEntryKey, cacheEntryValue);

            memoryCacheMock.SetUpCacheEntryRemove<T>(cacheEntryKey);

            return memoryCacheMock;
        }

        public static Mock<IMemoryCache> SetUpCacheEntryCreate<T>(this Mock<IMemoryCache> memoryCacheMock, object cacheEntryKey, T cacheEntryValue) {
            Logger.LogDebug($"Setting up cache entry Create for '{cacheEntryKey}' (type: {cacheEntryValue.GetType().Name}; value: '{cacheEntryValue.ToString()}')");

            var cacheEntryFake = new CacheEntryFake(cacheEntryKey, memoryCacheMock);

            memoryCacheMock.Setup(m => m.CreateEntry(It.Is<object>(k => k.Equals(cacheEntryKey))))
                .Callback(() => Logger.LogDebug("Cache CreateEntry invoked"))
                .Returns(() => cacheEntryFake);

            return memoryCacheMock;
        }

        public static Mock<IMemoryCache> SetUpCacheEntryGet<T>(this Mock<IMemoryCache> memoryCacheMock, object cacheEntryKey, object cacheEntryValue) {
            Logger.LogDebug($"Setting up cache entry Get for '{cacheEntryKey}'");

            memoryCacheMock.Setup(m => m.TryGetValue(It.Is<object>(k => k.Equals(cacheEntryKey)), out cacheEntryValue))
                .Callback(() => Logger.LogDebug("Cache TryGetValue invoked"))
                .Returns(true);

            return memoryCacheMock;
        }

        public static Mock<IMemoryCache> SetUpCacheEntryRemove<T>(this Mock<IMemoryCache> memoryCacheMock, object cacheEntryKey) {
            Logger.LogDebug($"Setting up cache entry Remove for '{cacheEntryKey}'");

            memoryCacheMock.Setup(m => m.Remove(It.Is<object>(k => k.Equals(cacheEntryKey))))
                .Callback(() => {
                    Logger.LogDebug("Cache Remove invoked");
                    memoryCacheMock.SetUpCacheEntryGet<T>(cacheEntryKey, typeof(T).GetDefaultValue());
                });

            return memoryCacheMock;
        }
    }
}
