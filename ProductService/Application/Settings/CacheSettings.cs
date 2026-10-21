namespace Application.Settings
{
    public class CacheSettings
    {
        public string DestinationUrl { get; set; }
        public string ApplicationName { get; set; }
        public int SlidingExpirationInMinutes { get; set; }
        public bool BypassCache { get; set; }
    }
}
