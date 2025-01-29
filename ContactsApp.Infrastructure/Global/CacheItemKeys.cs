namespace ContactsApp.Domain.Global
{
    public static class CacheItemKeys
    {
        public const string allContactsCacheKey = "AllContactsCacheKey";
        public const string mainContactCacheKey = "ContactCacheKey";
        public const string userCacheKey = "UserCacheKey";
        public static string actualCacheKey = string.Empty;
    }
}
