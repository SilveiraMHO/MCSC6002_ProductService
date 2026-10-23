namespace Application.Pipelines.Interfaces
{
    public interface ICacheable
    {
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
