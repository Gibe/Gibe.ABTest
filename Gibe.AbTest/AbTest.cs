using System;
using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest
{
	public class AbTest : IAbTest
	{
		private readonly IAbTestingService _abTestingService;
		private readonly IRandomNumber _randomNumber;

		public AbTest(IAbTestingService abTestingService, IRandomNumber randomNumber)
		{
			_abTestingService = abTestingService;
			_randomNumber = randomNumber;
		}

		public Variation AssignVariation(string userAgent)
		{
			var experiments = _abTestingService.GetExperiments().Where(x => x.Enabled);
			var selectedExperiment = RandomlySelectOption(experiments);
			return RandomlySelectOption(FilterVariations(selectedExperiment.Variations, userAgent));
		}

		public Variation GetAssignedVariation(string experimentId, int variationNumber)
		{
			return _abTestingService.GetVariation(experimentId, variationNumber);
		}

		private IEnumerable<Variation> FilterVariations(IEnumerable<Variation> variations, string userAgent)
		{
			var filtered = variations.Where(v => v.DesktopOnly && !userAgent.Contains("Mobi") || !v.DesktopOnly);
			if (!filtered.Any())
			{
				return variations.Take(1);
			}
			return filtered;
		}

		private T RandomlySelectOption<T>(IEnumerable<T> options) where T : IWeighted
		{
			var opts = options.ToArray();
			var totalWeights = opts.Sum(o => o.Weight);
			var selectedNumber = _randomNumber.Number(totalWeights);

			var currrentWeight = 0;
			foreach (var t in opts)
			{
				currrentWeight += t.Weight;
				if (currrentWeight > selectedNumber)
					return t;
			}
			return opts.Last();
		}
	}
}
