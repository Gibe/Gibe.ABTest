using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest
{
	public class ExperimentCookieValue
	{
		private readonly IAbTest _abTest;

		private const string Seperator = "~";
		private const string ExperimentSeperator = "-";

		public string RawValue;

		public ExperimentCookieValue(IAbTest abTest, string rawValue)
		{
			_abTest = abTest;
			RawValue = rawValue;
		}

		public ExperimentCookieValue(IAbTest abTest, IEnumerable<Variation> experimentVariantPairs)
		{
			_abTest = abTest;
			RawValue = string.Join(ExperimentSeperator, experimentVariantPairs.Select(v =>
				string.Join(Seperator, v.ExperimentId, v.VariationNumber)));
		}

		public IEnumerable<Variation> Variations()
		{
			return RawValue.Split(ExperimentSeperator.ToCharArray())
				.Select(e =>
					_abTest.Variation(e.Split(Seperator.ToCharArray())[0], int.Parse(e.Split(Seperator.ToCharArray())[1])));
		}
	}
}
