using System.Collections.Generic;
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
			return _abTestRepository.GetExperiments()
				.Select(x => new Experiment(x, GetVariations(x.Key)));
		}

		public IEnumerable<Variation> GetVariations(string experimentId)
		{
			return _abTestRepository.GetVariations(experimentId)
				.Select(v => new Variation(v));
		}

		public Variation GetVariation(string experimentId, int variationNumber)
		{
			var dto = _abTestRepository.GetVariations(experimentId)
				.First(v => v.VariationNumber == variationNumber);
			return new Variation(dto);
		}
	}
}
