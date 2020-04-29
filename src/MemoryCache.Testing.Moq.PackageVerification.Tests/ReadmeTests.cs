using System;
using MemoryCache.Testing.Common.Helpers;
using MemoryCache.Testing.Moq.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MemoryCache.Testing.Moq.PackageVerification.Tests
{
    [TestFixture]
    public class ReadmeTests
    {
        [SetUp]
        public void SetUp()
        {
            LoggerHelper.LoggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug));
        }
        
        [Test]
        public void Example1()
        {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid().ToString();

            var mockedCache = Create.MockedMemoryCache();

            var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Example2()
        {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid().ToString();

            var mockedCache = Create.MockedMemoryCache();
            mockedCache.SetUpCacheEntry(cacheEntryKey, expectedResult);

            var actualResult = mockedCache.Get(cacheEntryKey);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Example3()
        {
            var cacheEntryKey = "SomethingInTheCache";
            var expectedResult = Guid.NewGuid().ToString();

            var mockedCache = Create.MockedMemoryCache();

            var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

            var cacheMock = Mock.Get(mockedCache);
            cacheMock.Verify(x => x.CreateEntry(cacheEntryKey), Times.Once);
            object cacheEntryValue;
            cacheMock.Verify(x => x.TryGetValue(cacheEntryKey, out cacheEntryValue), Times.Once);
        }
    }
}