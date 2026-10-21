namespace Application.Pipelines.Interfaces
{
    public interface ICacheable
    {
        public string CacheKey { get; set; } //Key for acees data.
        public bool BypassCache { get; set; } //Instead get the data stored from cache, you prefere get from the database.
        public TimeSpan? SlidingExpiration { get; set; } //After a certain period, if the data stored in cache is not being used, It must be deleted.
    }
}
