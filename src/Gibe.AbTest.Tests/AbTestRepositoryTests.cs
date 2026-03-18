using GibeCommerce.Repositories.NPoco;
using Microsoft.Extensions.Configuration;
using NPoco;
using NUnit.Framework;
using System.Linq;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestRepositoryTests
	{
		[Test]
		public void Test()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.test.json")
				.Build();
			var repo = new AbTestRepository(new FakeGibeCommerceDatabaseProvider(configuration.GetConnectionString("GibeCommerce"), DatabaseType.SqlServer2012, configuration));
			var experiments = repo.GetExperiments().ToArray();
		}

		public class FakeGibeCommerceDatabaseProvider : GibeCommerceDatabaseProvider
		{
			private readonly string _connectionString;
			private readonly DatabaseType _databaseType;

			public FakeGibeCommerceDatabaseProvider(string connectionString, DatabaseType databaseType, IConfiguration configuration) : base(configuration, [])
			{
				_connectionString = connectionString;
				_databaseType = databaseType;
			}

			public new IDatabase Database()
			{
				return new GibeCommerceDatabase(_connectionString);
			}
		}
	}
}
