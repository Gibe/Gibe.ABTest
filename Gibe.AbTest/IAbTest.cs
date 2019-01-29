using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Gibe.AbTest
{
	public interface IAbTest
	{
		Variation AssignRandomVariation();
		Variation AssignVariation(string experiementKey);
		IEnumerable<Variation> AssignVariations();
		Variation AssignedVariation(string experimentId, int variationNumber);
	}

	public class FakeAbTest : IAbTest
	{
		private readonly IEnumerable<Variation> _variations;

		public FakeAbTest(IEnumerable<Variation> variations)
		{
			_variations = variations;
		}

		public Variation AssignRandomVariation()
		{
			return _variations.First();
		}

		public Variation AssignVariation(string experiementKey)
		{
			return _variations.First(v => v.ExperimentId == experiementKey);
		}

		public IEnumerable<Variation> AssignVariations()
		{
			return _variations.GroupBy(v => v.ExperimentId).Select(group => group.First());
		}

		public Variation AssignedVariation(string experimentId, int variationNumber)
		{
			return _variations.First(v => v.ExperimentId == experimentId && v.VariationNumber == variationNumber);
		}
	}
}
