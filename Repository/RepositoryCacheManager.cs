using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Constants;
using Entities.Models;
using System.Runtime.Caching;

namespace Repository
{
    public class RepositoryCacheManager: ICacheManager<RepositoryWrapper>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public RepositoryCacheManager(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public RepositoryWrapper GetCachedItem()
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(ApplicationConstants.CACHE_REPOSITORY_KEY))
            {
                return (RepositoryWrapper)cache.Get(ApplicationConstants.CACHE_REPOSITORY_KEY);
            }
            else
            {
                CacheItemPolicy cachePolicy = new CacheItemPolicy();
                cachePolicy.SlidingExpiration = new TimeSpan(0, 20, 0);

                cache.Add(ApplicationConstants.CACHE_REPOSITORY_KEY, _repositoryWrapper, cachePolicy);

                return (RepositoryWrapper)_repositoryWrapper;
            }
        }
    }
}
