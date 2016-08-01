using System.Collections.Generic;
using Gibe.AbTest.Dto;
using Gibe.NPoco;

namespace Gibe.AbTest
{
	public class AbTestRepository : IAbTestRepository
	{
		private readonly IDatabaseProvider _databaseProvider;

		public AbTestRepository(IDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}

		public ExperimentDto GetExperiment(int id)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Single<ExperimentDto>("WHERE Id = @0", id);
			}
		}

		public ExperimentDto GetExperiment(string key)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Single<ExperimentDto>("WHERE Key = @0", key);
			}
		}

		public IEnumerable<ExperimentDto> GetExperiments()
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Query<ExperimentDto>("FROM Experiment");
			}
		}

		public VariationDto GetVariation(int id)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Single<VariationDto>("WHERE Id = @0", id);
			}
		}

		public VariationDto GetVariation(string key)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Single<VariationDto>("WHERE Key = @0", key);
			}
		}

		public IEnumerable<VariationDto> GetVariations(int experimentId)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Query<VariationDto>("WHERE ExperimentId = @0", experimentId);
			}
		}
	}
}
