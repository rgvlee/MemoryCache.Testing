using MemoryCache.Testing.Common.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MemoryCache.Testing.Common.Tests {
    [TestFixture]
    public abstract class TestBase {
        protected static readonly ILogger<TestBase> Logger = LoggerHelper.CreateLogger<TestBase>();

        protected IMemoryCache MockedCache;

        protected abstract void SetUpCacheEntry<T>(string cacheEntryKey, T expectedResult);
            
        [SetUp]
        public virtual void SetUp() {
            LoggerHelper.LoggerFactory.AddConsole(LogLevel.Debug);
        }

        [Test]
        public virtual void GetOrCreateWithSetUp_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid();
            SetUpCacheEntry(cacheEntryKey, expectedResult);
            
            var actualResult = MockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);
            
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual async Task GetOrCreateAsyncWithSetUp_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid();
            SetUpCacheEntry(cacheEntryKey, expectedResult);
            
            var actualResult = await MockedCache.GetOrCreateAsync(cacheEntryKey, entry => Task.FromResult(expectedResult));

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual void GetWithNoSetUp_ReturnsDefaultValue() {
            var cacheEntryKey = "SomethingInTheCache";
            
            var actualResult = MockedCache.Get<Guid>(cacheEntryKey);

            Assert.That(actualResult, Is.EqualTo(default(Guid)));
        }
        
        [Test]
        public virtual void GetOrCreateWithNoSetUp_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid();

            var actualResult = MockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.Multiple(() => {
                Assert.AreEqual(expectedResult, actualResult);
            });
        }

        [Test]
        public virtual void GetOrCreateWithNoSetUp_TestObject_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();

            var actualResult = MockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.Multiple(() => {
                Assert.AreEqual(expectedResult, actualResult);
            });
        }

        [Test]
        public virtual async Task GetOrCreateAsyncWithNoSetUp_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid();
            
            var actualResult = await MockedCache.GetOrCreateAsync(cacheEntryKey, entry => Task.FromResult(expectedResult));

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual async Task GetOrCreateAsyncWithNoSetUp_TestObject_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();
            
            var actualResult = await MockedCache.GetOrCreateAsync(cacheEntryKey, entry => Task.FromResult(expectedResult));

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual void GetThenGetOrCreateThenGetWithNoSetUp_TestObject_ReturnsExpectedResults() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult1 = default(TestObject);
            var expectedResult2 = new TestObject();
            var expectedResult3 = expectedResult2;

            var actualResult1 = MockedCache.Get<TestObject>(cacheEntryKey);
            var actualResult2 = MockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult2);
            var actualResult3 = MockedCache.Get<TestObject>(cacheEntryKey);

            Assert.Multiple(() => {
                Assert.IsNull(actualResult1);
                Assert.AreEqual(expectedResult1, actualResult1);

                Assert.AreEqual(expectedResult2, actualResult2);

                Assert.IsNotNull(actualResult3);
                Assert.AreEqual(expectedResult3, actualResult3);
            });
        }

        [Test]
        public virtual void GetThenGetOrCreateThenGetWithSetUp_TestObject_ReturnsExpectedResults() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();

            SetUpCacheEntry(cacheEntryKey, expectedResult);

            var actualResult1 = MockedCache.Get<TestObject>(cacheEntryKey);
            var actualResult2 = MockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);
            var actualResult3 = MockedCache.Get<TestObject>(cacheEntryKey);

            Assert.Multiple(() => {
                Assert.AreEqual(expectedResult, actualResult1);
                Assert.AreEqual(expectedResult, actualResult2);
                Assert.AreEqual(expectedResult, actualResult3);
            });
        }

        [Test]
        public virtual void MinimumViableInterface_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid();
            
            var actualResult = MockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual void AddThenGetWithNoSetUp_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid();
            
            Logger.LogDebug("Add invocation started");
            MockedCache.Set(cacheEntryKey, expectedResult);
            Logger.LogDebug("Add invocation finished");
            var actualResult = MockedCache.Get<Guid>(cacheEntryKey);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual void AddThenGetWithNoSetUp_TestObject_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();
            
            MockedCache.Set(cacheEntryKey, expectedResult);

            var actualResult = MockedCache.Get<TestObject>(cacheEntryKey);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual void AddThenGetWithSetUp_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult1 = Guid.NewGuid();
            var expectedResult2 = Guid.NewGuid();

            SetUpCacheEntry(cacheEntryKey, expectedResult1);
            
            var actualResult1 = MockedCache.Get<Guid>(cacheEntryKey);
            Assert.AreEqual(expectedResult1, actualResult1);

            MockedCache.Set(cacheEntryKey, expectedResult2);
            var actualResult2 = MockedCache.Get<Guid>(cacheEntryKey);

            Assert.AreEqual(expectedResult2, actualResult2);
        }

        [Test]
        public virtual void RemoveWithSetUp_Guid_ReturnsDefaultValue() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult1 = Guid.NewGuid();

            SetUpCacheEntry(cacheEntryKey, expectedResult1);
            
            var actualResult1 = MockedCache.Get<Guid>(cacheEntryKey);
            MockedCache.Remove(cacheEntryKey);
            var actualResult2 = MockedCache.Get<Guid>(cacheEntryKey);

            Assert.Multiple(() => {
                Assert.AreEqual(expectedResult1, actualResult1);
                Assert.That(actualResult2, Is.EqualTo(default(Guid)));
            });
        }

        [Test]
        public virtual void RemoveWithNoSetUp_DoesNothing() {
            var cacheEntryKey = "SomethingInTheCache";

            Assert.DoesNotThrow(() => {
                MockedCache.Remove(cacheEntryKey);
            });
        }

        [Test]
        public virtual void RemoveWithNoSetUp_Guid_ReturnsDefaultValue() {
            var cacheEntryKey = "SomethingInTheCache";
            
            var actualResult1 = MockedCache.Get<Guid>(cacheEntryKey);
            MockedCache.Remove(cacheEntryKey);
            var actualResult2 = MockedCache.Get<Guid>(cacheEntryKey);

            Assert.Multiple(() => {
                Assert.That(actualResult1, Is.EqualTo(default(Guid)));
                Assert.That(actualResult2, Is.EqualTo(default(Guid)));
            });
        }

        [Test]
        public virtual void GetOrCreateThenRemoveWithNoSetUp_Guid_ReturnsDefaultValue() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult1 = Guid.NewGuid();

            var actualResult1 = MockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult1);
            MockedCache.Remove(cacheEntryKey);
            var actualResult2 = MockedCache.Get<Guid>(cacheEntryKey);

            Assert.Multiple(() => {
                Assert.That(actualResult1, Is.EqualTo(expectedResult1));
                Assert.That(actualResult2, Is.EqualTo(default(Guid)));
            });
        }
    }
}