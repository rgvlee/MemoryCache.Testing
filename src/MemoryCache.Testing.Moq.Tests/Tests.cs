using MemoryCache.Testing.Common.Tests;
using MemoryCache.Testing.Moq.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;

namespace MemoryCache.Testing.Moq.Tests {
    [TestFixture]
    public class Tests : TestBase {
        protected Mock<IMemoryCache> CacheMock;

        protected override void SetUpCacheEntry<T>(string cacheEntryKey, T expectedResult) {
            CacheMock.SetUpCacheEntry(cacheEntryKey, expectedResult);
        }

        [SetUp]
        public override void SetUp() {
            base.SetUp();

            CacheMock = MockFactory.CreateCachingServiceMock();
            MockedCache = CacheMock.Object;
        }
    }
}