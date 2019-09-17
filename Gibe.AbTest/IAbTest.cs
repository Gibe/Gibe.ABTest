using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest
{
	public interface IAbTest
	{
		IEnumerable<Experiment> AllExperiments();
		Variation AssignRandomVariation();
		Variation AssignVariation(string userAgent);
		Variation AssignVariationByExperimentKey(string experimentKey);
		IEnumerable<Variation> AllCurrentVariations();
		Variation Variation(string experimentId, int variationNumber);
	}

	public class FakeAbTest : IAbTest
	{
		private readonly IEnumerable<Variation> _variations;

		public FakeAbTest(IEnumerable<Variation> variations)
		{
			_variations = variations;
		}

		public IEnumerable<Experiment> AllExperiments()
		{
			return new List<Experiment>();//TODO: chance constructor to take experiments instead of variations
		}

		public Variation AssignRandomVariation()
		{
			return _variations.First();
		}

		public Variation AssignVariation(string userAgent)
		{
			return _variations.First();
		}

		public Variation AssignVariationByExperimentKey(string experimentKey)
		{
			return _variations.First(v => v.ExperimentId == experimentKey);
		}

		public IEnumerable<Variation> AllCurrentVariations()
		{
			return _variations.GroupBy(v => v.ExperimentId).Select(group => group.First());
		}

		public Variation Variation(string experimentId, int variationNumber)
		{
			return _variations.FirstOrDefault(v => v.ExperimentId == experimentId && v.VariationNumber == variationNumber);
		}
	}
}
