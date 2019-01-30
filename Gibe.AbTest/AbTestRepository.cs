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

		public ExperimentDto GetExperiment(string id)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.SingleOrDefault<ExperimentDto>("WHERE Id = @0", id);
			}
		}

		public IEnumerable<ExperimentDto> GetExperiments()
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Fetch<ExperimentDto>("FROM AbExperiment");
			}
		}
		
		public IEnumerable<VariationDto> GetVariations(string experimentId)
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				return db.Fetch<VariationDto>("WHERE ExperimentId = @0", experimentId);
			}
		}
	}
}
