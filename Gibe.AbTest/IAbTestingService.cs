using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public interface IAbTestingService
	{
		Variation GetVariation(string key);
		IEnumerable<Variation> GetVariations(int experimentId);
		IEnumerable<Experiment> GetExperiments();
	}

	public class FakeAbTestingService : IAbTestingService
	{
		public Variation GetVariation(string key)
		{
			return new Variation(1, key, 1, true, "{Test:'test'}");
		}

		public IEnumerable<Variation> GetVariations(int experimentId)
		{
			return new List<Variation>
			{
				GetVariation("A"),
				GetVariation("B")
			};
		}

		public IEnumerable<Experiment> GetExperiments()
		{
			return new List<Experiment>
			{
				new Experiment(1, "AB1", 1, true, DateTime.Now, null, GetVariations(1)),
				new Experiment(2, "AB2", 1, true, DateTime.Now, null, GetVariations(2))
			};
		}
	}
}
