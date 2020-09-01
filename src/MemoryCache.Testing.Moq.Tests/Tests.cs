using MemoryCache.Testing.Common.Tests;
using MemoryCache.Testing.Moq.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;

namespace MemoryCache.Testing.Moq.Tests
{
    [TestFixture]
    public class Tests : BaseForTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            MockedCache = Create.MockedMemoryCache();
        }

        protected override void SetUpCacheEntry<T>(string cacheEntryKey, T expectedResult)
        {
            MockedCache.SetUpCacheEntry(cacheEntryKey, expectedResult);
        }

        [Test]
        public virtual void AddThenGetWithNoSetUp_TestObject_GetInvokedOnce()
        {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();

            MockedCache.Set(cacheEntryKey, expectedResult);

            var actualResult = MockedCache.Get<TestObject>(cacheEntryKey);

            object value;
            Mock.Get(MockedCache).Verify(m => m.TryGetValue(cacheEntryKey, out value), Times.Once);
        }

        [Test]
        public virtual void AddWithNoSetUp_TestObject_AddInvokedOnce()
        {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();

            MockedCache.Set(cacheEntryKey, expectedResult);

            Mock.Get(MockedCache).Verify(m => m.CreateEntry(cacheEntryKey), Times.Once);
        }
    }
}