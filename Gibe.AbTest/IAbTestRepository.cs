using System.Collections.Generic;
using Gibe.AbTest.Dto;

namespace Gibe.AbTest
{
	public interface IAbTestRepository
	{
		Experiment GetExperiment(int id);
		Experiment GetExperiment(string key);
		IEnumerable<Experiment> GetExperiments();
		Variation GetVariation(int id);
		Variation GetVariation(string key);
		IEnumerable<Variation> GetVariations(int experimentId);
	}
}
