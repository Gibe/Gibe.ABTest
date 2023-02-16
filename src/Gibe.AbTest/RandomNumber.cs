using System;

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
