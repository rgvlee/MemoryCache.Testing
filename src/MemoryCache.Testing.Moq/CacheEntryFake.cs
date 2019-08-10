using MemoryCache.Testing.Common.Extensions;
using MemoryCache.Testing.Common.Helpers;
using MemoryCache.Testing.Moq.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace MemoryCache.Testing.Moq {
    /// <summary>
    /// A fake cache entry.
    /// </summary>
    public class CacheEntryFake : MemoryCache.Testing.Common.CacheEntryFake {
        private static readonly ILogger Logger = LoggerHelper.CreateLogger(typeof(CacheEntryFake));

        private readonly Mock<IMemoryCache> _memoryCacheMock;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">The cache entry key.</param>
        /// <param name="memoryCacheMock">The memory cache mock instance.</param>
        public CacheEntryFake(object key, Mock<IMemoryCache> memoryCacheMock) : base(key) {
            _memoryCacheMock = memoryCacheMock;
        }

        private object _value;

        /// <inheritdoc />
        public override object Value {
            get => _value;
            set {
                Logger.LogDebug($"Setting _value to {value}");
                _value = value;
                _memoryCacheMock.SetUpCacheEntryGet(Key, _value);
                _memoryCacheMock.SetUpCacheEntryRemove(Key, _value?.GetType().GetDefaultValue());
            }
        }
    }
}
