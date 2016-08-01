using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public interface IAbTest
	{
		Variation AssignVariation();
		Variation GetAssignedVariation(string key);
	}

	public class FakeAbTest : IAbTest
	{
		public Variation AssignVariation()
		{
			return new Variation(1,"TEST",1,true,"{Test:'test'");
		}

		public Variation GetAssignedVariation(string key)
		{
			return new Variation(1, key, 1, true, "{Test:'test'");
		}
	}
}
