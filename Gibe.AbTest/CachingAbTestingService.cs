﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.Caching.Interfaces;

namespace Gibe.AbTest
{
	public class CachingAbTestingService : IAbTestingService
	{
		private readonly IAbTestingService _abTestingService;
		private readonly ICache _cache;

		private const string CacheKey = "GibeAbCache";

		public CachingAbTestingService(IAbTestingService abTestingService, ICache cache)
		{
			_abTestingService = abTestingService;
			_cache = cache;
		}

		public Variation GetVariation(string experimentKey, int variationNumber)
		{
			return GetVariations(experimentKey).First(v => v.VariationNumber == variationNumber);
		}

		public IEnumerable<Variation> GetVariations(string experimentKey)
		{
			return GetExperiments().First(x => x.Key == experimentKey).Variations;
		}

		public IEnumerable<Experiment> GetExperiments()
		{
			if (_cache.Exists(CacheKey))
			{
				return _cache.Get<Experiment[]>(CacheKey);
			}
			var experiments = _abTestingService.GetExperiments().ToArray();
			_cache.Add(CacheKey, experiments, TimeSpan.FromMinutes(15));
			return experiments;
		}
	}
}
