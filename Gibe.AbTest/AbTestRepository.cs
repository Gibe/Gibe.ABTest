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

		public Experiment GetExperiment(int id)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Single<Experiment>("WHERE Id = @0", id);
			}
		}

		public Experiment GetExperiment(string key)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Single<Experiment>("WHERE Key = @0", key);
			}
		}

		public IEnumerable<Experiment> GetExperiments()
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Query<Experiment>("FROM Experiment");
			}
		}

		public Variation GetVariation(int id)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Single<Variation>("WHERE Id = @0", id);
			}
		}

		public Variation GetVariation(string key)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Single<Variation>("WHERE Key = @0", key);
			}
		}

		public IEnumerable<Variation> GetVariations(int experimentId)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Query<Variation>("WHERE ExperimentId = @0", experimentId);
			}
		}
	}
}
