﻿namespace ContactsApp.Domain.Global
{
    public static class CacheItemKeys
    {
        public const string allContactsCacheKey = "AllContactsCacheKey";
        public const string mainContactCacheKey = "ContactCacheKey";
        public static string actualCacheKey = string.Empty;
    }
}
