using System;
using NPoco;

namespace Gibe.AbTest.Dto
{
	[TableName("ABExperiment")]
	public class ExperimentDto
	{
		public string Key { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool Enabled { get; set; }
		public int Weight { get; set; }
	}
}
