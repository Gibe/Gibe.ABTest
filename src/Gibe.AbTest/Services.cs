using Gibe.AbTest.Dates;
using GibeCommerce.Cache;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Gibe.AbTest
{
	public static class Services
	{
		public static void AddGibeAbTest(this IServiceCollection services)
		{
			services.AddSingleton<IAbTest, AbTest>();
			services.AddSingleton<IAbTestRepository, AbTestRepository>();
			services.AddSingleton<IAbTestingService, AbTestingService>();
			services.Decorate<IAbTestingService, CachingAbTestingService>();
			services.AddSingleton<IRandomNumber, RandomNumber>();
			services.AddSingleton<ITimeProvider, DateTimeUtcTimeProvider>();

			if (services.Any(x => x.ServiceType == typeof(ICache)) != true)
			{
				services.AddSingleton<ICache, MemoryCacheWrapper>();
			}
		}
	}
}

