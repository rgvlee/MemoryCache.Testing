# MemoryCache.Testing

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/c9692f6a601d4dc0b485224de539c441)](https://www.codacy.com/manual/rgvlee/MemoryCache.Testing?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=rgvlee/MemoryCache.Testing&amp;utm_campaign=Badge_Grade) [![Codacy Badge](https://api.codacy.com/project/badge/Coverage/c9692f6a601d4dc0b485224de539c441)](https://www.codacy.com/manual/rgvlee/MemoryCache.Testing?utm_source=github.com&utm_medium=referral&utm_content=rgvlee/MemoryCache.Testing&utm_campaign=Badge_Coverage)

__*A functional system mock of the Microsoft.Extensions.Caching.Memory.IMemoryCache interface using Moq and NSubstitute*__

## Overview

The intent of this library is to provide a system mock of the Microsoft.Extensions.Caching.Memory.IMemoryCache interface. Some of the features include:

-   Easy to use - create a functional system mock with a single line of code
-   Implicit or explicit cache entry set up, it's up to you
-   Access to all of the good stuff that these great mocking libraries provide (e.g., Moq Verify)

## Resources

-   [Source repository](https://github.com/rgvlee/MemoryCache.Testing/)
-   [MemoryCache.Testing.Moq - NuGet](https://www.nuget.org/packages/MemoryCache.Testing.Moq/)
-   [MemoryCache.Testing.NSubstitute - NuGet](https://www.nuget.org/packages/MemoryCache.Testing.NSubstitute/)

## Usage

Start by creating a mocked memory cache using `Create.MockedMemoryCache()`:

``` C#
var cacheEntryKey = "SomethingInTheCache";
var expectedResult = Guid.NewGuid().ToString();

var mockedCache = Create.MockedMemoryCache();

var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

Assert.AreEqual(expectedResult, actualResult);
```

Provided your SUT populates the cache that'd be all you need to do. If it doesn't, or you like your arrange to be verbose, use `SetUpCacheEntry<T>` to set up a cache entry:

``` C#
var cacheEntryKey = "SomethingInTheCache";
var expectedResult = Guid.NewGuid().ToString();

var mockedCache = Create.MockedMemoryCache();
mockedCache.SetUpCacheEntry(cacheEntryKey, expectedResult);

var actualResult = mockedCache.Get(cacheEntryKey);

Assert.AreEqual(expectedResult, actualResult);
```

The Moq implementation of `Create.MockedMemoryCache()` returns the mocked memory cache. If you need the mock (e.g., to verify an invocation) use `Mock.Get(mockedCache)`:

``` C#
var cacheEntryKey = "SomethingInTheCache";
var expectedResult = Guid.NewGuid().ToString();

var mockedCache = Create.MockedMemoryCache();

var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

var cacheMock = Mock.Get(mockedCache);
cacheMock.Verify(x => x.CreateEntry(cacheEntryKey), Times.Once);
object cacheEntryValue;
cacheMock.Verify(x => x.TryGetValue(cacheEntryKey, out cacheEntryValue), Times.Once);
```