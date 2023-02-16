using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest
{
	public class AbTestingService : IAbTestingService
	{
		private readonly IAbTestRepository _abTestRepository;

		public AbTestingService(IAbTestRepository abTestRepository)
		{
			_abTestRepository = abTestRepository;
		}

		public IEnumerable<Experiment> GetExperiments()
		{
			var experiments = _abTestRepository.GetExperiments()
				.Where(e => e.Enabled)
				.Select(e => new Experiment(e, GetAvailableVariations(e.Id).ToArray()))
				.Where(e => e.Variations.Any())
				.ToArray();

			if (experiments.Any())
			{
				return experiments;
			}

			return new[] { EmptyExperiment() };
		}

		public IEnumerable<Variation> GetVariations(string experimentId)
		{
			var experiment = GetExperiments()
				.FirstOrDefault(v => v.Id == experimentId);

			if (experiment != null && experiment.Variations.Any())
			{
				return experiment.Variations;
			}

			return new[] { EmptyVariation() };
		}

		public Variation GetVariation(string experimentId, int variationNumber)
		{
			var experiment = GetExperiments()
				.FirstOrDefault(v => v.Id == experimentId);

			if (experiment != null && experiment.Variations.Any(v => v.VariationNumber == variationNumber))
			{
				return experiment.Variations.First(v => v.VariationNumber == variationNumber);
			}

			return EmptyVariation();
		}

		private IEnumerable<Variation> GetAvailableVariations(string experimentId)
		{
			var variations = _abTestRepository.GetVariations(experimentId)
				.Where(v => v.Enabled)
				.Select(v => new Variation(v))
				.ToArray();

			return variations.Any() ? variations : Enumerable.Empty<Variation>();
		}

		private Experiment EmptyExperiment()
		{
			return new Experiment(new Dto.ExperimentDto
			{
				Id = "",
				Key = "",
				Description = "",
				StartDate = new System.DateTime(2019, 01, 01),
				EndDate = null,
				Weight = 1,
				Enabled = true
			}, new[] { EmptyVariation() });
		}

		private Variation EmptyVariation()
		{
			return new Variation(0, 1, 1, true, "", "", false);
		}
	}
}
