using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gibe.NPoco;
using NUnit.Framework;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestRepositoryTests
	{
		[Test]
		public void Test()
		{
			var repo = new AbTestRepository(new DefaultDatabaseProvider("GibeCommerce"));
			var experiments = repo.GetExperiments().ToArray();
		}
	}
}
