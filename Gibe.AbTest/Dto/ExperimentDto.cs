using System;
using NPoco;

namespace Gibe.AbTest.Dto
{
	[TableName("AbExperiment")]
	[PrimaryKey("Id", AutoIncrement = false)]
	public class ExperimentDto
	{
		public string Id { get; set; }
		public string Key { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool Enabled { get; set; }
		public int Weight { get; set; }
	}
}
