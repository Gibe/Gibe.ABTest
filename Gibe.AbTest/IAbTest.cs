﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public interface IAbTest
	{
		Variation AssignVariation();
		Variation GetAssignedVariation(string experimentId, int variationNumber);
	}

	public class FakeAbTest : IAbTest
	{
		public Variation AssignVariation()
		{
			return new Variation(1, 0, 1,true,"{Test:'test'}", "ABC1");
		}

		public Variation GetAssignedVariation(string experimentId, int variationNumber)
		{
			return new Variation(1, variationNumber, 1, true, "{Test:'test'}", experimentId);
		}
	}
}
