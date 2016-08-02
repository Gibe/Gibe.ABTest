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

		public Variation AssignVariation()
		{
			var experiments = _abTestingService.GetExperiments().Where(x => x.Enabled);
			var selectedExperiment = RandomlySelectOption(experiments);
			return RandomlySelectOption(selectedExperiment.Variations);
		}

		public Variation GetAssignedVariation(string experimentKey, int variationNumber)
		{
			return _abTestingService.GetVariation(experimentKey, variationNumber);
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
