using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Gibe.AbTest.Dto;

namespace Gibe.AbTest
{
	public interface IAbTestRepository
	{
		ExperimentDto GetExperiment(string key);
		IEnumerable<ExperimentDto> GetExperiments();
		VariationDto GetVariation(int id);
		IEnumerable<VariationDto> GetVariations(string experimentKey);

		
	}

	public class FakeAbTestRepository : IAbTestRepository
	{
		
		public ExperimentDto GetExperiment(string key)
		{
			return new ExperimentDto
			{
				Enabled = true,
				StartDate = DateTime.Now,
				EndDate = null,
				Key = key,
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

		public VariationDto GetVariation(int id)
		{
			return new VariationDto
			{
				Id = id,
				VariationNumber = 0,
				Definition = "{Test:'test'}",
				Enabled = true,
				ExperimentKey = "ABC",
				Weight = 1
			};
		}
		
		public IEnumerable<VariationDto> GetVariations(string experimentKey)
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
