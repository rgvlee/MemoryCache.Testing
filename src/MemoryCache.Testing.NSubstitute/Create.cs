using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NSubstitute;
using rgvlee.Core.Common.Helpers;

namespace MemoryCache.Testing.NSubstitute
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
            var mock = Substitute.For<IMemoryCache>();

            mock.CreateEntry(Arg.Any<object>())
                .Returns(x =>
                {
                    var key = x.Args()[0];
                    return new CacheEntryFake(key, mock);
                })
                .AndDoes(x => Logger.LogDebug("Cache CreateEntry invoked"));

            return mock;
        }
    }
}