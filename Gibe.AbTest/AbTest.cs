using System;
using System.Collections.Generic;
using System.Data;
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

		public IEnumerable<Experiment> AllExperiments()
		{
			return _abTestingService.GetEnabledExperiments();
		}

		public Variation AssignRandomVariation(string userAgent)
		{
			var experiments = _abTestingService.GetEnabledExperiments();
			var selectedExperiment = RandomlySelectOption(FilterExperiments(experiments, userAgent));
			return RandomlySelectOption(FilterVariations(selectedExperiment.Variations, userAgent));
		}

		public Variation AssignVariationByExperimentKey(string experimentKey)
		{
			var experiment = _abTestingService.GetEnabledExperiments()
				.First(x => x.Key == experimentKey);
			return RandomlySelectOption(experiment.Variations);
		}

		public IEnumerable<Variation> AllCurrentVariations()
		{
			var experiments = _abTestingService.GetEnabledExperiments().Where(x => (DateTime.Now >= x.StartDate || x.StartDate == null ) && (DateTime.Now < x.EndDate || x.EndDate == null));
			foreach (var experiment in experiments)
			{
				yield return RandomlySelectOption(experiment.Variations);
			}
		}

		public Variation Variation(string experimentId, int variationNumber)
		{
			return _abTestingService.GetVariation(experimentId, variationNumber);
		}

		private IEnumerable<Experiment> FilterExperiments(IEnumerable<Experiment> experments, string userAgent)
		{
			var filtered = experments.Where(e => e.Variations.Any(v => v.DesktopOnly) && !userAgent.Contains("Mobi") || !e.Variations.All(v => v.DesktopOnly));
			if (!filtered.Any())
			{
				return experments.Take(1); //TODO: We should not return anything if there are no correct matches, this requires a refactor to use IEnumerables everywhere
			}
			return filtered;
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
