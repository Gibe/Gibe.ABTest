﻿using System;
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
		public int VariationNumber { get; set; }
		public string ExperimentKey { get; set; }
		public int Weight { get; set; }
		public bool Enabled { get; set; }
		public string Defintion { get; set; }

		public Variation(VariationDto dto) : this(dto.Id, dto.VariationNumber, dto.Weight, dto.Enabled, dto.Definition, dto.ExperimentKey) { }

		public Variation(int id, int variationNumber, int weight, bool enabled, string definition, string experimentKey)
		{
			Id = id;
			VariationNumber = variationNumber;
			ExperimentKey = experimentKey;
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
			return $"Id : {Id} {ExperimentKey}-{VariationNumber}";
		}
	}
}
