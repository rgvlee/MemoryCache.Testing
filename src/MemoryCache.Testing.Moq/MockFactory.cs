using System;
using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace MemoryCache.Testing.Moq {
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
        public static Mock<IMemoryCache> CreateMemoryCacheMock() {
            return Mock.Get(Create.MockedMemoryCache());
        }

        /// <summary>
        ///     Creates a mocked memory cache.
        /// </summary>
        /// <returns>A mocked memory cache.</returns>
        public static IMemoryCache CreateMockedMemoryCache() {
            return Create.MockedMemoryCache();
        }
    }
}