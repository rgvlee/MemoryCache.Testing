using MemoryCache.Testing.Common.Tests;
using MemoryCache.Testing.NSubstitute.Extensions;
using NUnit.Framework;

namespace MemoryCache.Testing.NSubstitute.Tests {
    [TestFixture]
    public class Tests : TestBase {
        protected override void SetUpCacheEntry<T>(string cacheEntryKey, T expectedResult) {
            MockedCache.SetUpCacheEntry(cacheEntryKey, expectedResult);
        }

        [SetUp]
        public override void SetUp() {
            base.SetUp();

            MockedCache = MockFactory.CreateMemoryCacheMock();
        }
    }
}