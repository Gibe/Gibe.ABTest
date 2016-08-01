using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gibe.AbTest.Dto;
using Newtonsoft.Json;

namespace Gibe.AbTest
{
	public class Variation : IWeighted
	{
		public int Id { get; set; }
		public string Key { get; set; }
		public int Weight { get; set; }
		public bool Enabled { get; set; }
		public string Defintion { get; set; }

		public Variation(VariationDto dto) : this(dto.Id, dto.Key, dto.Weight, dto.Enabled, dto.Definition) { }

		public Variation(int id, string key, int weight, bool enabled, string definition)
		{
			Id = id;
			Key = key;
			Weight = weight;
			Enabled = enabled;
			Defintion = definition;
		}
		
		public T GetDefinition<T>()
		{
			return JsonConvert.DeserializeObject<T>(Defintion);
		}

		public override string ToString()
		{
			return $"Id : {Id} - Key : {Key}";
		}
	}
}
