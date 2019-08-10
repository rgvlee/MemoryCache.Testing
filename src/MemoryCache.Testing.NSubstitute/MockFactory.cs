using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace MemoryCache.Testing.NSubstitute {
    /// <summary>
    /// Factory for creating mock/mocked instances.
    /// </summary>
    public class MockFactory {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(MockFactory));

        /// <summary>
        /// Creates a memory cache mock.
        /// </summary>
        /// <returns>A memory cache mock.</returns>
        public static IMemoryCache CreateMemoryCacheMock() {
            var mock = Substitute.For<IMemoryCache>();

            mock.CreateEntry(Arg.Any<object>())
                .Returns(x => {
                    var key = x.Args()[0];
                    return new CacheEntryFake(key, mock);
                })
                .AndDoes(x => Logger.LogDebug("Cache CreateEntry invoked")); ;
                
            return mock;
        }
    }
}
