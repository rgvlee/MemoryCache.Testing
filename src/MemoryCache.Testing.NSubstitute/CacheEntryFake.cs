using MemoryCache.Testing.NSubstitute.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using rgvlee.Core.Common.Extensions;

namespace MemoryCache.Testing.NSubstitute
{
    /// <inheritdoc />
    public class CacheEntryFake : Common.CacheEntryFake
    {
        private object _value;

        /// <inheritdoc />
        public CacheEntryFake(object key) : base(key) { }

        /// <inheritdoc />
        public CacheEntryFake(object key, IMemoryCache mockedMemoryCache) : base(key, mockedMemoryCache) { }

        /// <inheritdoc />
        public override object Value {
            get => _value;
            set
            {
                Logger.LogDebug($"Setting _value to {value}");
                _value = value;
                MockedMemoryCache.SetUpCacheEntryGet(Key, _value);
                MockedMemoryCache.SetUpCacheEntryRemove(Key, _value?.GetType().GetDefaultValue());
            }
        }
    }
}