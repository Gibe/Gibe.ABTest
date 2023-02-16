using System;
using System.Runtime.Caching;
using Gibe.AbTest.Dates;

namespace Gibe.AbTest.Caching
{
	public class MemoryCacheWrapper : ICache
	{
		private readonly ObjectCache _cache;
		private ITimeProvider _timeProvider;

		public MemoryCacheWrapper(ITimeProvider timeProvider)
		{
			_cache = MemoryCache.Default;
			_timeProvider = timeProvider;
		}

		public void Add<T>(string key, T value, TimeSpan duration) where T : class
		{
			_cache.Add(key, value, new DateTimeOffset(_timeProvider.Now.Add(duration)));
		}

		public T Get<T>(string key) where T : class
		{
			return (T)_cache[key];
		}

		public bool Exists(string key)
		{
			return _cache.Contains(key);
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}
	}
}
