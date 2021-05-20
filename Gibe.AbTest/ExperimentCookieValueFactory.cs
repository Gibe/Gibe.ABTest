using System.Collections.Generic;

namespace Gibe.AbTest
{
	public class ExperimentCookieValueFactory : IExperimentCookieValueFactory
	{
		private readonly IAbTest _abTest;

		public ExperimentCookieValueFactory(IAbTest abTest)
		{
			_abTest = abTest;
		}

		public ExperimentCookieValue ExperimentCookieValue(string rawValue)
		{
			return new ExperimentCookieValue(_abTest, rawValue);
		}

		public ExperimentCookieValue ExperimentCookieValue(IEnumerable<Variation> variations)
		{
			return new ExperimentCookieValue(_abTest, variations);
		}
	}
}
