using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gibe.AbTest
{
	public class RandomNumber : IRandomNumber
	{
		public int Number(int max)
		{
			return new Random().Next(max);
		}
	}
}
