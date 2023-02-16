using System.Collections.Generic;
using System.Linq;
using Gibe.AbTest.Dto;

namespace Gibe.AbTest
{
	public interface IAbTestRepository
	{
		ExperimentDto GetExperiment(string id);
		IEnumerable<ExperimentDto> GetExperiments();
		VariationDto GetVariation(int id);
		IEnumerable<VariationDto> GetVariations(string experimentId);
	}

	public class FakeAbTestRepository : IAbTestRepository
	{
		private IEnumerable<ExperimentDto> _experiments;
		private IEnumerable<VariationDto> _variations;

		public FakeAbTestRepository(IEnumerable<ExperimentDto> experiments, IEnumerable<VariationDto> variations)
		{
			_experiments = experiments;
			_variations = variations;
		}

		public ExperimentDto GetExperiment(string id)
		{
			return _experiments.First();
		}

		public IEnumerable<ExperimentDto> GetExperiments()
		{
			return _experiments;
		}

		public VariationDto GetVariation(int id)
		{
			return _variations.First();
		}

		public IEnumerable<VariationDto> GetVariations(string experimentId)
		{
			return _variations;
		}
	}
}
