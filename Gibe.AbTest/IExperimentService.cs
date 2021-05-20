using System.Collections.Generic;

namespace Gibe.AbTest
{
	public interface IExperimentService
	{
		bool IsCurrentUserInExperiment();
		IEnumerable<Variation> CurrentUserVariations();
		Variation CurrentUserVariation(string experimentId);
		IEnumerable<Variation> AssignUserVariations();
		IEnumerable<Variation> AssignUserVariations(string value);
	}
}
