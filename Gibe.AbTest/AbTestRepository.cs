using System;
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

		public IEnumerable<ExperimentDto> GetEnabledExperiments()
		{
			using (var db = _databaseProvider.GetDatabase())
			{
				var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				return db.Fetch<ExperimentDto>($"FROM AbExperiment " +
					$"WHERE [Enabled] = 1 " +
					$"AND '{now}' >= [StartDate] OR StartDate IS NULL " +
					$"AND '{now}' < [EndDate] OR StartDate IS NULL ");
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
