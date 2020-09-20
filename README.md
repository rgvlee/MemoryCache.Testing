# MemoryCache.Testing

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/c9692f6a601d4dc0b485224de539c441)](https://www.codacy.com/manual/rgvlee/MemoryCache.Testing?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=rgvlee/MemoryCache.Testing&amp;utm_campaign=Badge_Grade) [![Codacy Badge](https://api.codacy.com/project/badge/Coverage/c9692f6a601d4dc0b485224de539c441)](https://www.codacy.com/manual/rgvlee/MemoryCache.Testing?utm_source=github.com&utm_medium=referral&utm_content=rgvlee/MemoryCache.Testing&utm_campaign=Badge_Coverage)

## Overview

MemoryCache.Testing is a mocking library that creates Microsoft.Extensions.Caching.Memory IMemoryCache system mocks. It's easy to use (usually just a single line of code) with implementations for both Moq and NSubstitute.

## Resources

-   [Source repository](https://github.com/rgvlee/MemoryCache.Testing)
-   [MemoryCache.Testing.Moq - NuGet](https://www.nuget.org/packages/MemoryCache.Testing.Moq)
-   [MemoryCache.Testing.NSubstitute - NuGet](https://www.nuget.org/packages/MemoryCache.Testing.NSubstitute)

## Usage

Start by creating a mocked memory cache using `Create.MockedMemoryCache()`:

```c#
var cacheEntryKey = "SomethingInTheCache";
var expectedResult = Guid.NewGuid().ToString();

var mockedCache = Create.MockedMemoryCache();

var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

Assert.AreEqual(expectedResult, actualResult);
```

This creates a mocked `IMemoryCache`. If your SUT populates the cache you're done. If it doesn't, or you like your arrange to be verbose, populate it as if you were using the real thing:

```c#
var cacheEntryKey = "SomethingInTheCache";
var expectedResult = Guid.NewGuid().ToString();

var mockedCache = Create.MockedMemoryCache();
mockedCache.Set(cacheEntryKey, expectedResult);

var actualResult = mockedCache.Get(cacheEntryKey);

Assert.AreEqual(expectedResult, actualResult);
```

The Moq implementation of `Create.MockedMemoryCache()` returns the mocked memory cache. If you need the mock itself (e.g., to verify an invocation) use `Mock.Get(mockedCache)`:

```c#
var cacheEntryKey = "SomethingInTheCache";
var expectedResult = Guid.NewGuid().ToString();

var mockedCache = Create.MockedMemoryCache();

var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

var cacheMock = Mock.Get(mockedCache);
cacheMock.Verify(x => x.CreateEntry(cacheEntryKey), Times.Once);
object cacheEntryValue;
cacheMock.Verify(x => x.TryGetValue(cacheEntryKey, out cacheEntryValue), Times.Once);
```

With regard to verifying invocations, all members of the `IMemoryCache` interface are mocked.
