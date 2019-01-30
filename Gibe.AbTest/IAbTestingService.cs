using System.Collections.Generic;

namespace Gibe.AbTest
{
	public interface IAbTestingService
	{
		Variation GetVariation(string experimentId, int variationNumber);
		IEnumerable<Variation> GetVariations(string experimentId);
		IEnumerable<Experiment> GetExperiments();
	}

	public class FakeAbTestingService : IAbTestingService
	{
		public IEnumerable<Experiment> Experiments;
		public FakeAbTestingService(IEnumerable<Experiment> experiments)
		{
			Experiments = experiments;
		}

		public Variation GetVariation(string experimentId, int variationNumber)
		{
			return new Variation(1, variationNumber, 1, true, "{Test:'test'}", experimentId);
		}

		public IEnumerable<Variation> GetVariations(string experimentId)
		{
			return new List<Variation>
			{
				GetVariation(experimentId, 0),
				GetVariation(experimentId, 1)
			};
		}

		public IEnumerable<Experiment> GetExperiments()
		{
			return Experiments;
		}
	}
}
