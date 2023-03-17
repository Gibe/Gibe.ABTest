using NUnit.Framework;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestRepositoryTests
	{
		[Test]
		public void Test()
		{
#if NET45
		var repo = new AbTestRepository(new DefaultDatabaseProvider("GibeCommerce"));
		var experiments = repo.GetExperiments().ToArray();
#endif

#if NETCORE
		var repo = new AbTestRepository(new DefaultDatabaseProvider("GibeCommerce", DatabaseType.SqlServer2012));
		var experiments = repo.GetExperiments().ToArray();
#endif
		}
	}
}
