using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Gibe.AbTest.Dto;

namespace Gibe.AbTest
{
	public interface IAbTestRepository
	{
		ExperimentDto GetExperiment(int id);
		ExperimentDto GetExperiment(string key);
		IEnumerable<ExperimentDto> GetExperiments();
		VariationDto GetVariation(int id);
		VariationDto GetVariation(string key);
		IEnumerable<VariationDto> GetVariations(int experimentId);

		
	}

	public class FakeAbTestRepository : IAbTestRepository
	{
		public ExperimentDto GetExperiment(int id)
		{
			return new ExperimentDto
			{
				Id = id,
				Enabled = true,
				StartDate = DateTime.Now,
				EndDate = null,
				Key = "ABCDEFGHI",
				Weight = 1
			};
		}

		public ExperimentDto GetExperiment(string key)
		{
			return new ExperimentDto
			{
				Id = 1,
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
				GetExperiment(1),
				GetExperiment(2),
				GetExperiment(3)
			};
		}

		public VariationDto GetVariation(int id)
		{
			return new VariationDto
			{
				Id = id,
				Definition = "{Definition:'Test'}",
				Enabled = true,
				Key = "ABCDEF-0",
				Weight = 1
			};
		}

		public VariationDto GetVariation(string key)
		{
			return new VariationDto
			{
				Id = 1,
				Key = key,
				Definition = "{Definition:'Test'}",
				Enabled = true,
				Weight = 1
			};
		}

		public IEnumerable<VariationDto> GetVariations(int experimentId)
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
