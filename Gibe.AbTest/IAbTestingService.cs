using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public interface IAbTestingService
	{
		Variation GetVariation(string experimentKey, int variationNumber);
		IEnumerable<Variation> GetVariations(string experimentKey);
		IEnumerable<Experiment> GetExperiments();
	}

	public class FakeAbTestingService : IAbTestingService
	{
		public Variation GetVariation(string experimentKey, int variationNumber)
		{
			return new Variation(1, variationNumber, 1, true, "{Test:'test'}", experimentKey);
		}

		public IEnumerable<Variation> GetVariations(string experimentKey)
		{
			return new List<Variation>
			{
				GetVariation(experimentKey, 0),
				GetVariation(experimentKey, 1)
			};
		}

		public IEnumerable<Experiment> GetExperiments()
		{
			return new List<Experiment>
			{
				new Experiment("A1234", "AB1", "Desc 1", 1, true, DateTime.Now, null, GetVariations("AB1")),
				new Experiment("A2345", "AB2", "Desc 2", 1, true, DateTime.Now, null, GetVariations("AB2"))
			};
		}
	}
}
