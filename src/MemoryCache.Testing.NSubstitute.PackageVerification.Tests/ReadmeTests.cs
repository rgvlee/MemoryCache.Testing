using System;
using MemoryCache.Testing.Common.Helpers;
using MemoryCache.Testing.NSubstitute.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace MemoryCache.Testing.NSubstitute.PackageVerification.Tests
{
    [TestFixture]
    public class ReadmeTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            //LoggerHelper.LoggerFactory.AddConsole(LogLevel.Debug);
        }

        [Test]
        public virtual void GetOrCreateWithSetUp_Guid_ReturnsExpectedResult()
        {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid().ToString();

            var mockedCache = Create.MockedMemoryCache();
            mockedCache.SetUpCacheEntry(cacheEntryKey, expectedResult);

            var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public virtual void MinimumViableInterface_Guid_ReturnsExpectedResult()
        {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid().ToString();

            var mockedCache = Create.MockedMemoryCache();

            var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}