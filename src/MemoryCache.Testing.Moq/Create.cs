using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using rgvlee.Core.Common.Helpers;

namespace MemoryCache.Testing.Moq
{
    /// <summary>
    ///     Factory for creating mocked instances.
    /// </summary>
    public static class Create
    {
        private static readonly ILogger Logger = LoggingHelper.CreateLogger(typeof(Create));

        /// <summary>
        ///     Creates a mocked memory cache.
        /// </summary>
        /// <returns>A mocked memory cache.</returns>
        public static IMemoryCache MockedMemoryCache()
        {
            var mock = new Mock<IMemoryCache>();

            mock.Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Callback((object key) =>
                {
                    Logger.LogDebug("Cache CreateEntry invoked");
                })
                .Returns((object key) => new CacheEntryFake(key, mock.Object));

            return mock.Object;
        }
    }
}