using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public interface IAbTestingService
	{
		Variation GetVariation(string experimentId, int variationNumber);
		IEnumerable<Variation> GetVariations(string experimentId);
		IEnumerable<Experiment> GetExperiments();
	}

	public class FakeAbTestingService : IAbTestingService
	{
		public Variation GetVariation(string experimentId, int variationNumber)
		{
			return new Variation(1, variationNumber, 1, true, "{Test:'test'}", experimentId, false);
		}

		public IEnumerable<Variation> GetVariations(string experimentId)
		{
			return new List<Variation>
			{
				GetVariation(experimentId, 0),
				GetVariation(experimentId, 1)
			};
		}

		public IEnumerable<Experiment> GetExperiments()
		{
			return new List<Experiment>
			{
				new Experiment("A1234", "AB1", "Desc 1", 1, true, DateTime.Now, null, GetVariations("AB1").ToArray()),
				new Experiment("A2345", "AB2", "Desc 2", 1, true, DateTime.Now, null, GetVariations("AB2").ToArray())
			};
		}
	}
}
