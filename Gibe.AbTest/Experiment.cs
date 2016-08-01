using Gibe.AbTest.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public class Experiment : IWeighted
	{
		public int Id { get; set; }
		public string Key { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool Enabled { get; set; }
		public int Weight { get; set; }
		public IEnumerable<Variation> Variations { get; set; }

		public Experiment(ExperimentDto dto, IEnumerable<Variation> variations) : this(dto.Id, dto.Key, dto.Weight, dto.Enabled, dto.StartDate, dto.EndDate, variations) { }

		public Experiment(int id, string key, int weight, bool enabled, DateTime startDate, DateTime? endDate, IEnumerable<Variation> variations)
		{
			Id = id;
			Key = key;
			Weight = weight;
			Enabled = enabled;
			StartDate = startDate;
			EndDate = endDate;
			Variations = variations;
		}

		public override string ToString()
		{
			return $"Id : {Id} - Key : {Key}";
		}
	}
}
