# MemoryCache.Testing
__*Moq and NSubstitute mocking libraries for Microsoft.Extensions.Caching.Memory.IMemoryCache*__

## The cache experience
MemoryCache.Testing.Moq and MemoryCache.Testing.NSubstitute are more than just a few mock set ups. They mock the cache as a whole.

Some of the features include:
- If the SUT populates the cache using Set\<T>, GetOrCreate\<T> or GetOrCreateAsync\<T> no explicit set up is required, it just works. Create the mock and consume.
- Changes to the cache actually do something (e.g., Set will add a new cache entry or update an existing cache entry regardless of whether you have explicitly set it up or not).
- The ability to provide explicit set ups if you want (using SetUpCacheEntry\<T>).
- Access to all of the good stuff that these great mocking libraries provide such as Moq ```Verify```, NSubstitute ```Received``` etc. 

## Resources
- [Source repository](https://github.com/rgvlee/MemoryCache.Testing)
- [MemoryCache.Testing.Moq - NuGet](https://www.nuget.org/packages/MemoryCache.Testing.Moq/)
- [MemoryCache.Testing.NSubstitute - NuGet](https://www.nuget.org/packages/MemoryCache.Testing.NSubstitute/)

## The disclaimer
After my work on [LazyCache](https://github.com/rgvlee/LazyCache.Testing) I figured why not do the same thing for IMemoryCache. If you find these libraries useful and something is missing, not working as you'd expect or you need additional behaviour mocked flick me a message and I'll see what I can do.

# Moq
## Basic usage
- Get a mocked lazy cache
- Consume

```
[Test]
public virtual void MinimumViableInterface_Guid_ReturnsExpectedResult() {
    var cacheEntryKey = "SomethingInTheCache";
    var expectedResult = Guid.NewGuid().ToString();

    var mockedCache = MockFactory.CreateMockedMemoryCache();

    var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

    Assert.AreEqual(expectedResult, actualResult);
}
```

## But I want the cache mock
No problem. Use the mock helper to create the mock. At this point it's a Mock\<IMemoryCache> for you to specify additional set ups, assert verify method invocations etc.

```
[Test]
public virtual void GetOrCreateWithNoSetUp_TestObject_ReturnsExpectedResult() {
    var cacheEntryKey = "SomethingInTheCache";
    var expectedResult = new TestObject();

    var cacheMock = MockFactory.CreateMemoryCacheMock();
    var mockedCache = cacheMock.Object;

    var actualResult = mockedCache.GetOrCreate(cacheEntryKey, entry => expectedResult);

    Assert.AreEqual(expectedResult, actualResult);
}
```

## Let's get explicit
If you want to explicitly specify a cache entry set up, use the fluent extension method.

```
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
```

## I'm using Get\<T>, what do I need to do?
The mock needs to know what to return. You'll need to either:
- Populate the cache using Set\<T>, GetOrCreate\<T> or GetOrCreateAsync\<T>; or
- Use the explicit set up as described above.

## Async? Please tell me you support the async methods.
The survey says, yes the async methods are supported. You're welcome.

# NSubstitute
I'm not going to go deep into NSubstitute usage as, well, it is the same except for one thing. For Moq, the MockFactory.CreateMemoryCacheMock() method returns a Mock\<T> containing the mocked object (Mock\<T>.Object). For NSubstitute you don't need to do that.

## Basic usage
- Get a mocked lazy cache
- Consume

```
[Test]
public virtual void MinimumViableInterface_Guid_ReturnsExpectedResult() {
    var cacheEntryKey = "SomethingInTheCache";
    var expectedResult = Guid.NewGuid().ToString();

    var cacheMock = MockFactory.CreateMemoryCacheMock();

    var actualResult = cacheMock.GetOrCreate(cacheEntryKey, entry => expectedResult);

    Assert.AreEqual(expectedResult, actualResult);
}
```

## Explicit cache entry set up
Same same just with the explicit ```SetUpCacheEntry``` to set up the specified cache entry.

```
[Test]
public virtual void GetOrAddWithSetUp_Guid_ReturnsExpectedResult() {
    var cacheEntryKey = "SomethingInTheCache";
    var expectedResult = Guid.NewGuid().ToString();

    var cacheMock = MockFactory.CreateMemoryCacheMock();
    cacheMock.SetUpCacheEntry(cacheEntryKey, expectedResult);
    
    var actualResult = cacheMock.GetOrCreate(cacheEntryKey, entry => expectedResult);

    Assert.AreEqual(expectedResult, actualResult);
}
```