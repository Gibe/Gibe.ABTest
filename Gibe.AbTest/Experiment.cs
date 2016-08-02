using Gibe.AbTest.Dto;
using System;
using System.Collections.Generic;

namespace Gibe.AbTest
{
	public class Experiment : IWeighted
	{
		public string Key { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool Enabled { get; set; }
		public int Weight { get; set; }
		public IEnumerable<Variation> Variations { get; set; }

		public Experiment(ExperimentDto dto, IEnumerable<Variation> variations) : this(dto.Key, dto.Weight, dto.Enabled, dto.StartDate, dto.EndDate, variations) { }

		public Experiment(string key, int weight, bool enabled, DateTime startDate, DateTime? endDate, IEnumerable<Variation> variations)
		{
			Key = key;
			Weight = weight;
			Enabled = enabled;
			StartDate = startDate;
			EndDate = endDate;
			Variations = variations;
		}

		public override string ToString()
		{
			return $"Experiment {Key}";
		}
	}
}
