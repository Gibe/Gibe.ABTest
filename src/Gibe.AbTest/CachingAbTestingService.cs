﻿using Gibe.AbTest.Attributes;
using Gibe.AbTest.Caching;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest
{
	public class CachingAbTestingService : IAbTestingService
	{
		private readonly IAbTestingService _abTestingService;
		private readonly ICache _cache;

		private const string CacheKey = "GibeAbCache";

		public CachingAbTestingService([NotCached] IAbTestingService abTestingService, ICache cache)
		{
			_abTestingService = abTestingService;
			_cache = cache;
		}

		public Variation GetVariation(string experimentId, int variationNumber)
		{
			return GetVariations(experimentId).First(v => v.VariationNumber == variationNumber);
		}

		public IEnumerable<Variation> GetVariations(string experimentId)
		{
			return GetExperiments().First(x => x.Id == experimentId).Variations;
		}

		public IEnumerable<Experiment> GetExperiments()
		{
			if (_cache.Exists(CacheKey))
			{
				return _cache.Get<Experiment[]>(CacheKey);
			}
			var experiments = _abTestingService.GetExperiments().ToArray();
			_cache.Add(CacheKey, experiments, TimeSpan.FromMinutes(1));
			return experiments;
		}
	}
}
