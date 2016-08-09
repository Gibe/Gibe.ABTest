using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public class RandomNumber : IRandomNumber
	{
		private readonly Random _random;

		public RandomNumber()
		{
			_random = new Random();
		}

		public int Number(int max)
		{
			return _random.Next(max);
		}
	}
}
