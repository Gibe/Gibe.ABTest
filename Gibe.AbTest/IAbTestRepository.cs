using System;
using System.Collections.Generic;
using Gibe.AbTest.Dto;

namespace Gibe.AbTest
{
	public interface IAbTestRepository
	{
		ExperimentDto GetExperiment(string id);
		IEnumerable<ExperimentDto> GetExperiments();
		IEnumerable<VariationDto> GetVariations(string experimentId);
	}

	public class FakeAbTestRepository : IAbTestRepository
	{
		
		public ExperimentDto GetExperiment(string id)
		{
			return new ExperimentDto
			{
				Id = id,
				Enabled = true,
				StartDate = DateTime.Now,
				EndDate = null,
				Key = "ABC123",
				Weight = 1
			};
		}

		public IEnumerable<ExperimentDto> GetExperiments()
		{
			return new List<ExperimentDto>
			{
				GetExperiment("ABC"),
				GetExperiment("DEF"),
				GetExperiment("GHI")
			};
		}

		private VariationDto GetVariation(int id)
		{
			return new VariationDto
			{
				Id = id,
				VariationNumber = 0,
				Definition = "{Test:'test'}",
				Enabled = true,
				ExperimentId = "ABC",
				Weight = 1
			};
		}
		
		public IEnumerable<VariationDto> GetVariations(string experimentId)
		{
			return new List<VariationDto>
			{
				GetVariation(1),
				GetVariation(2),
				GetVariation(3)
			};
		}
	}
}
