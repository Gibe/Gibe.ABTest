﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

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
				.Select(x => new Experiment(x, GetVariations(x.Id).ToArray()));

			if (experiments.Any())
			{
				return experiments;
			}

			return new[] { EmptyExperiment() };
		}

		public IEnumerable<Variation> GetVariations(string experimentId)
		{
			var variations = _abTestRepository.GetVariations(experimentId)
				.Select(v => new Variation(v));

			if (variations.Any())
			{
				return variations;
			}

			return new[] { EmptyVariation() };
		}

		public Variation GetVariation(string experimentId, int variationNumber)
		{
			var dto = _abTestRepository.GetVariations(experimentId)
				.First(v => v.VariationNumber == variationNumber);

			if (dto != null)
			{
				return new Variation(dto);
			}

			return EmptyVariation();
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
