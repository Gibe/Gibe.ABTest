using System;
// ReSharper disable DTN

namespace Gibe.AbTest.Dates
{
	public class DateTimeUtcTimeProvider : ITimeProvider
	{
		public virtual DateTime Now => DateTime.UtcNow;
	}
}
