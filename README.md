# MemoryCache.Testing
__*Moq and NSubstitute mocking libraries for Microsoft.Extensions.Caching.Memory.MemoryCache/IMemoryCache*__

## The cache experience
MemoryCache.Testing.Moq and MemoryCache.Testing.NSubstitute are more than just a few mock set ups. They mock the memory cache as a whole.

Some of the features include:
- If the SUT populates the cache using `Set<T>`, `GetOrCreate<T>` or `GetOrCreateAsync<T>` no explicit set up is required; it just works. Create the mock and consume.
- Changes to the memory cache do something (e.g., `Set` will add a new cache entry or update an existing cache entry regardless of whether you have explicitly set it up or not).
- The ability to provide explicit set ups if you want (using `SetUpCacheEntry<T>`).
- Access to all of the good stuff that these great mocking libraries provide such as Moq ```Verify```, NSubstitute ```Received``` etc. 

## Resources
- [Source repository](https://github.com/rgvlee/MemoryCache.Testing)
- [MemoryCache.Testing.Moq - NuGet](https://www.nuget.org/packages/MemoryCache.Testing.Moq/)
- [MemoryCache.Testing.NSubstitute - NuGet](https://www.nuget.org/packages/MemoryCache.Testing.NSubstitute/)

## The disclaimer
After my work on [LazyCache](https://github.com/rgvlee/LazyCache.Testing) I figured why not do the same thing for Microsoft.Extensions.Caching.Memory.MemoryCache/IMemoryCache.

If you find these libraries useful and something is missing, not working as you'd expect or you need additional behaviour mocked, flick me a message and I'll see what I can do.

# Moq
## Basic usage
- Create a mocked memory cache
- Consume

``` C#
[Test]
public virtual void MinimumViableInterface_Guid_ReturnsExpectedResult()
{
    var cacheEntryKey = "SomethingInTheCache";
    var expectedResult = Guid.NewGuid().ToString();

    var mockedCache = Create.MockedMemoryCache();

    var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

    Assert.AreEqual(expectedResult, actualResult);
}
```

## But I want the cache mock!
No problem. Use `Mock.Get(mockedCache)` to get the cache mock.

## My SUT invokes the memory cache `Get<T>` method, is there anything specific I need to do?
The mock needs to know what to return. You'll need to either:
- Populate the cache using `Set<T>`, `GetOrCreate<T>` or `GetOrCreateAsync<T>`; or
- Use the explicit set up as described below.

## Explicit memory cache cache entry set up
If you want to manually set up a cache entry use the following extension method:

``` C#
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
```

## Are the async methods supported?
The survey says, yes the async methods are supported.

# NSubstitute
It works the same/has the same interface. The only difference is if you want to cache mock. For Moq you need to invoke `Mock.Get(mockedCache)` to get it. For NSubstitute you don't need to do this.