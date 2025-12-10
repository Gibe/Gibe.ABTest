using Gibe.AbTest.Dto;
using System.Collections.Generic;
using GibeCommerce.Persistence;
using NPoco;

namespace Gibe.AbTest
{
	public class AbTestRepository : IAbTestRepository
	{
		private readonly IDatabaseProvider<IDatabase> _databaseProvider;

		public AbTestRepository(IDatabaseProvider<IDatabase> databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}

		public ExperimentDto GetExperiment(string id)
		{
			using (var db = _databaseProvider.Database())
			{
				return db.Single<ExperimentDto>("WHERE Id = @0", id);
			}
		}

		public IEnumerable<ExperimentDto> GetExperiments()
		{
			using (var db = _databaseProvider.Database())
			{
				return db.Query<ExperimentDto>("FROM AbExperiment");
			}
		}

		public VariationDto GetVariation(int id)
		{
			using (var db = _databaseProvider.Database())
			{
				return db.Single<VariationDto>("WHERE Id = @0", id);
			}
		}

		public IEnumerable<VariationDto> GetVariations(string experimentId)
		{
			using (var db = _databaseProvider.Database())
			{
				return db.Query<VariationDto>("WHERE ExperimentId = @0", experimentId);
			}
		}
	}
}
