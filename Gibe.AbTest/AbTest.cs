using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest
{
	public class AbTest : IAbTest
	{
		private readonly IAbTestingService _abTestingService;
		private readonly IRandomNumber _randomNumber;

		public AbTest(IAbTestingService abTestingService,
			IRandomNumber randomNumber)
		{
			_abTestingService = abTestingService;
			_randomNumber = randomNumber;
		}

		public Variation AssignRandomVariation()
		{
			var experiments = _abTestingService.GetExperiments().Where(x => x.Enabled);
			var selectedExperiment = RandomlySelectOption(experiments);
			return RandomlySelectOption(selectedExperiment.Variations);
		}

		public Variation AssignVariation(string experimentKey)
		{
			var experiment = _abTestingService.GetExperiments()
				.Where(x => x.Enabled)
				.First(x => x.Key == experimentKey);
			return RandomlySelectOption(experiment.Variations);
		}

		public IEnumerable<Variation> AssignVariations()
		{
			var experiments = _abTestingService.GetExperiments().Where(x => x.Enabled);
			foreach (var experiment in experiments)
			{
				yield return RandomlySelectOption(experiment.Variations);
			}
		}

		public Variation AssignedVariation(string experimentId, int variationNumber)
		{
			return _abTestingService.GetVariation(experimentId, variationNumber);
		}


		private T RandomlySelectOption<T>(IEnumerable<T> options) where T : IWeighted
		{
			var opts = options.ToArray();
			var totalWeights = opts.Sum(o => o.Weight);
			var selectedNumber = _randomNumber.Number(totalWeights);

			var currentWeight = 0;
			foreach (var o in opts)
			{
				currentWeight += o.Weight;
				if (currentWeight > selectedNumber)
					return o;
			}
			return opts.Last();
		}
	}
}
