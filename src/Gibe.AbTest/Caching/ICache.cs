using System;

namespace Gibe.AbTest.Caching
{
	public interface ICache
	{
		void Add<T>(string key, T value, TimeSpan duration) where T : class;
		T Get<T>(string key) where T : class;
		bool Exists(string key);
		void Remove(string key);
	}
}
