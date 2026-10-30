using System;
using Gibe.AbTest.Attributes;
using Gibe.AbTest.Caching;
using Gibe.AbTest.Dates;
using Microsoft.Extensions.DependencyInjection;
using Ninject;

namespace Gibe.AbTest
{
	public static class Services
	{
		public static void AddGibeAbTestBindings(this IKernel kernel)
		{
			AddGibeAbTestServices(kernel: kernel);
		}

		public static void AddGibeAbTestServices(this IServiceCollection serviceCollection)
		{
			AddGibeAbTestServices(serviceCollection: serviceCollection, kernel: null);
		}

		private static void AddGibeAbTestServices(IServiceCollection serviceCollection = null, IKernel kernel = null)
		{
			AddTransient<IAbTest, AbTest>(serviceCollection, kernel);
			AddTransient<IAbTestRepository, AbTestRepository>(serviceCollection, kernel);
			AddTransient<IAbTestingService, CachingAbTestingService>(serviceCollection, kernel);
			AddTransient<IRandomNumber, RandomNumber>(serviceCollection, kernel);
			AddTransient<ITimeProvider, DateTimeUtcTimeProvider>(serviceCollection, kernel);
			AddTransient<ICache, MemoryCacheWrapper>(serviceCollection, kernel);
		}


		private static void AddTransient<TInterface, TImplementation>(IServiceCollection serviceCollection, IKernel kernel)
			where TImplementation : TInterface
		{
			serviceCollection?.AddTransient(typeof(TInterface), typeof(TImplementation));

			kernel?.Bind<TInterface>().To<TImplementation>();
		}
	}
}

