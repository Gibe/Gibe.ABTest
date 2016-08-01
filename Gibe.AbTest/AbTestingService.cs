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
				.Select(x => new Experiment(x, GetVariations(x.Id)));
		}

		public IEnumerable<Variation> GetVariations(int experimentId)
		{
			return _abTestRepository.GetVariations(experimentId)
				.Select(v => new Variation(v));
		}

		public Variation GetVariation(string key)
		{
			var dto = _abTestRepository.GetVariation(key);
			return new Variation(dto);
		}
	}
}
