using MemoryCache.Testing.Common.Helpers;
using MemoryCache.Testing.Moq.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;

namespace MemoryCache.Testing.Moq.PackageVerification.Tests {
    [TestFixture]
    public class Tests {
        [SetUp]
        public virtual void SetUp() {
            LoggerHelper.LoggerFactory.AddConsole(LogLevel.Debug);
        }

        [Test]
        public void MinimumViableInterface_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid().ToString();

            var mockedCache = MockFactory.CreateMockedMemoryCache();

            var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetOrCreateWithNoSetUp_TestObject_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = new TestObject();

            var cacheMock = MockFactory.CreateMemoryCacheMock();
            var mockedCache = cacheMock.Object;

            var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual void GetOrCreateWithSetUp_Guid_ReturnsExpectedResult() {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid();

            var cacheMock = MockFactory.CreateMemoryCacheMock();
            cacheMock.SetUpCacheEntry(cacheEntryKey, expectedResult);
            var mockedCache = cacheMock.Object;

            var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}