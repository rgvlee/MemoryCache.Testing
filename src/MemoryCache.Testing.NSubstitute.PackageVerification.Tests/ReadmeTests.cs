using System;
using MemoryCache.Testing.NSubstitute.Extensions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using NUnit.Framework;

namespace MemoryCache.Testing.NSubstitute.PackageVerification.Tests
{
    [TestFixture]
    public class ReadmeTests
    {
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

            mockedCache.Received(1).CreateEntry(cacheEntryKey);
            object cacheEntryValue;
            mockedCache.Received(1).TryGetValue(cacheEntryKey, out cacheEntryValue);
        }
    }
}