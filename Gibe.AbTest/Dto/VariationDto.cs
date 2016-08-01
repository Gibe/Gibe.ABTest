using NPoco;

namespace Gibe.AbTest.Dto
{
	[TableName("ABVariation")]
	public class VariationDto
	{
		public int Id { get; set; }
		public string Key { get; set; }
		public int Weight { get; set; }
		public bool Enabled { get; set; }
		public string Definition { get; set; }
	}
}
