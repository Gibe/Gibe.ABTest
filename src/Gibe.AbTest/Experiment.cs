using Gibe.AbTest.Dto;
using System;
using System.Collections.Generic;

namespace Gibe.AbTest
{
	public class Experiment : IWeighted
	{
		public string Id { get; set; }
		public string Key { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool Enabled { get; set; }
		public int Weight { get; set; }
		public IEnumerable<Variation> Variations { get; set; }

		public Experiment(ExperimentDto dto, Variation[] variations) : this(dto.Id, dto.Key, dto.Description, dto.Weight, dto.Enabled, dto.StartDate, dto.EndDate, variations) { }

		public Experiment(string id, string key, string description, int weight, bool enabled, DateTime startDate, DateTime? endDate, Variation[] variations)
		{
			Id = id;
			Key = key;
			Weight = weight;
			Enabled = enabled;
			StartDate = startDate;
			EndDate = endDate;
			Variations = variations;
			Description = description;
		}

		public override string ToString()
		{
			return $"Experiment {Key}";
		}
	}
}
