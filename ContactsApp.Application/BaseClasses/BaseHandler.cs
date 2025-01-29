using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.BaseClasses
{
    public abstract class BaseHandler<T>
    {
        protected readonly IMemoryCache _memoryCache;
        protected readonly ILogger<T> _logger;
        public BaseHandler(IMemoryCache memoryCache, ILogger<T> logger)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
