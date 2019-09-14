using MemoryCache.Testing.Common.Extensions;
using MemoryCache.Testing.Moq.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MemoryCache.Testing.Moq {
    /// <inheritdoc />
    public class CacheEntryFake : Common.CacheEntryFake {
        private object _value;

        public CacheEntryFake(object key) : base(key) { }

        public CacheEntryFake(object key, IMemoryCache mockedMemoryCache) : base(key, mockedMemoryCache) { }

        public override object Value {
            get => _value;
            set {
                Logger.LogDebug($"Setting _value to {value}");
                _value = value;
                MockedMemoryCache.SetUpCacheEntryGet(Key, _value);
                MockedMemoryCache.SetUpCacheEntryRemove(Key, _value?.GetType().GetDefaultValue());
            }
        }
    }
}