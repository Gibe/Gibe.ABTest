using NPoco;

namespace Gibe.AbTest.Dto
{
	[TableName("ABVariation")]
	[PrimaryKey("Id", AutoIncrement = true)]
	public class VariationDto
	{
		public int Id { get; set; }
		public string ExperimentId { get; set; }
		public int VariationNumber { get; set; }
		public int Weight { get; set; }
		public bool Enabled { get; set; }
		public string Definition { get; set; }
		public bool DesktopOnly { get; set; }
	}
}
