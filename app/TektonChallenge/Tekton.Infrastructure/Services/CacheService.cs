using System.Runtime.Caching;
using Tekton.Application.Interfaces.Services;
using Tekton.Domain.Entities;

namespace Tekton.Infrastructure.Services
{
	public class CacheService : ICacheService
	{
		private readonly ObjectCache _cache = MemoryCache.Default;

		public List<Status> GetOrSet(string cacheKey, Func<List<Status>> getItemCallback, int durationInMinutes)
		{
			var item = _cache[cacheKey] as List<Status>;
			if (item == null)
			{
				item = getItemCallback();
				_cache.Set(cacheKey, item, DateTimeOffset.Now.AddMinutes(durationInMinutes));
			}
			return item;
		}
	}
}
