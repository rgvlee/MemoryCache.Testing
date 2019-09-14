using MemoryCache.Testing.Common.Tests;
using MemoryCache.Testing.Moq.Extensions;
using NUnit.Framework;

namespace MemoryCache.Testing.Moq.Tests {
    [TestFixture]
    public class Tests : TestBase {
        [SetUp]
        public override void SetUp() {
            base.SetUp();

            MockedCache = Create.MockedMemoryCache();
        }

        protected override void SetUpCacheEntry<T>(string cacheEntryKey, T expectedResult) {
            MockedCache.SetUpCacheEntry(cacheEntryKey, expectedResult);
        }
    }
}