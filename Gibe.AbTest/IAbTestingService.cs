using System.Collections.Generic;

namespace Gibe.AbTest
{
	public interface IAbTestingService
	{
		Variation GetVariation(string experimentId, int variationNumber);
		IEnumerable<Variation> GetVariations(string experimentId);
		IEnumerable<Experiment> GetEnabledExperiments();
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
			return new Variation(1, variationNumber, 1, true, "{Test:'test'}", experimentId, false);
		}

		private Variation GetDesktopOnlyVariation(string experimentId, int variationNumber, bool desktopOnly = false)
		{
			var variation = GetVariation(experimentId, variationNumber);
			variation.DesktopOnly = desktopOnly;
			return variation;
		}

		public IEnumerable<Variation> GetVariations(string experimentId)
		{
			return new List<Variation>
			{
				GetDesktopOnlyVariation(experimentId, 0, false),
				GetDesktopOnlyVariation(experimentId, 1, true)
			};
		}

		public IEnumerable<Experiment> GetEnabledExperiments()
		{
			return Experiments;
		}
	}
}
