using System.Collections.Generic;

namespace Gibe.AbTest
{
	public interface IExperimentCookieValueFactory
	{
		ExperimentCookieValue ExperimentCookieValue(IEnumerable<Variation> variations);
		ExperimentCookieValue ExperimentCookieValue(string rawValue);
	}
}