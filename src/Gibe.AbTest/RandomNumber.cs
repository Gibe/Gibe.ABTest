using System;

namespace Gibe.AbTest
{
	public class RandomNumber : IRandomNumber
	{
		public int Number(int max)
		{
			return Random.Shared.Next(max);
		}
	}
}
