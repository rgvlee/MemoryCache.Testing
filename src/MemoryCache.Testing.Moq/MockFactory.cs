using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace MemoryCache.Testing.Moq {
    public class MockFactory {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(MockFactory));

        public static Mock<IMemoryCache> CreateCachingServiceMock() {
            var mock = new Mock<IMemoryCache>();

            mock.Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Callback(() => Logger.LogDebug("Cache CreateEntry invoked"))
                .Returns((object key) => new CacheEntryFake(key, mock));
            
            return mock;
        }

        public static IMemoryCache CreateMockedCachingService() {
            return CreateCachingServiceMock().Object;
        }
    }
}
