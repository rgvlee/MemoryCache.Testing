using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace MemoryCache.Testing.Common {
    /// <summary>
    /// A fake cache entry.
    /// </summary>
    public class CacheEntryFake : ICacheEntry {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">The cache entry key.</param>
        public CacheEntryFake(object key) {
            Key = key;
        }

        /// <inheritdoc />
        public void Dispose() {
            //throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object Key { get; }

        /// <inheritdoc />
        public virtual object Value { get; set; }

        /// <inheritdoc />
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        /// <inheritdoc />
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        /// <inheritdoc />
        public TimeSpan? SlidingExpiration { get; set; }

        /// <inheritdoc />
        public IList<IChangeToken> ExpirationTokens { get; }

        /// <inheritdoc />
        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; }

        /// <inheritdoc />
        public CacheItemPriority Priority { get; set; }

        /// <inheritdoc />
        public long? Size { get; set; }
    }
}