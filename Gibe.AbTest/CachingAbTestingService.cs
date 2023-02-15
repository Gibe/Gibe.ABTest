using Gibe.AbTest.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest
{
	public class CachingAbTestingService : IAbTestingService
	{
		private readonly IAbTestingService _abTestingService;

		public CachingAbTestingService([NotCached] IAbTestingService abTestingService)
		{
			_abTestingService = abTestingService;
		}

		public Variation GetVariation(string experimentId, int variationNumber)
		{
			return GetVariations(experimentId).First(v => v.VariationNumber == variationNumber);
		}

		public IEnumerable<Variation> GetVariations(string experimentId)
		{
			return GetExperiments().First(x => x.Id == experimentId).Variations;
		}

		[ResponseCache(Duration = 60)]
		public IEnumerable<Experiment> GetExperiments()
		{
			return _abTestingService.GetExperiments().ToArray();
		}
	}
}
