using System;
using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MemoryCache.Testing.NSubstitute {
    /// <summary>
    ///     Factory for creating mock/mocked instances.
    /// </summary>
    [Obsolete("Use Create.MockedMemoryCache() to create a mocked memory cache. This class will removed in a future release.")]
    public class MockFactory {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(MockFactory));

        /// <summary>
        ///     Creates a memory cache mock.
        /// </summary>
        /// <returns>A memory cache mock.</returns>
        public static IMemoryCache CreateMemoryCacheMock() {
            return Create.MockedMemoryCache();
        }
    }
}