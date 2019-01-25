using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public interface IAbTest
	{
		Variation AssignVariation();
		Variation AssignVariation(string experiementKey);
		IEnumerable<Variation> AssignVariations();
		Variation GetAssignedVariation(string experimentId, int variationNumber);
	}

	public class FakeAbTest : IAbTest
	{
		public Variation AssignVariation()
		{
			return new Variation(1, 0, 1,true,"{Test:'test'}", "ABC1");
		}

		public Variation AssignVariation(string experiementKey)
		{
			return new Variation(1, 0, 1,true,"{Test:'test'}", experiementKey);
		}

		public IEnumerable<Variation> AssignVariations()
		{
			return new List<Variation>
			{
				new Variation(1, 0, 1,true,"{Test:'test1'}", "ABC1"),
				new Variation(2, 1, 1,true,"{Test:'test2'}", "DEF2"),
				new Variation(3, 0, 1,true,"{Test:'test3'}", "GHI3")
			};
		}

		public Variation GetAssignedVariation(string experimentId, int variationNumber)
		{
			return new Variation(1, variationNumber, 1, true, "{Test:'test'}", experimentId);
		}
	}
}
