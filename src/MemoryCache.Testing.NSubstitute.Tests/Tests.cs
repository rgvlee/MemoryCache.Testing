using MemoryCache.Testing.Common.Tests;
using MemoryCache.Testing.NSubstitute.Extensions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using NUnit.Framework;

namespace MemoryCache.Testing.NSubstitute.Tests {
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

        [Test]
        public virtual void AddWithNoSetUp_TestObject_AddInvokedOnce() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();

            MockedCache.Set(cacheEntryKey, expectedResult);

            MockedCache.Received(1).CreateEntry((object)cacheEntryKey);
        }

        [Test]
        public virtual void AddThenGetWithNoSetUp_TestObject_GetInvokedOnce() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();

            MockedCache.Set(cacheEntryKey, expectedResult);

            var actualResult = MockedCache.Get<TestObject>(cacheEntryKey);

            object value;
            MockedCache.Received(1).TryGetValue((object)cacheEntryKey, out value);
        }
    }
}