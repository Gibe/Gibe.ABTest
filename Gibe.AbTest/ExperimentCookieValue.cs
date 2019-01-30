using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest
{
	public class ExperimentCookieValue
	{
		private readonly IAbTest _abTest;

		private readonly string _rawValue;

		private const string Seperator = "~";
		private const string ExperimentSeperator = "-";

		public ExperimentCookieValue(IAbTest abTest, string rawValue)
		{
			_abTest = abTest;
			_rawValue = rawValue;
		}

		public ExperimentCookieValue(IAbTest abTest, IEnumerable<Variation> experimentVariantPairs)
		{
			_abTest = abTest;
			_rawValue = string.Join(ExperimentSeperator, experimentVariantPairs.Select(v =>
				string.Join(Seperator, v.ExperimentId, v.VariationNumber)));
		}

		public string RawValue => _rawValue;

		public IEnumerable<Variation> Variations()
		{
			return _rawValue.Split(ExperimentSeperator.ToCharArray())
				.Select(e =>
					_abTest.Variation(e.Split(Seperator.ToCharArray())[0], int.Parse(e.Split(Seperator.ToCharArray())[1])));
		}
	}
}
