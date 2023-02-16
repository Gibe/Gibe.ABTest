using System;
// ReSharper disable DTN

namespace Gibe.AbTest.Dates
{
	public interface ITimeProvider
	{
		DateTime Now { get; }
	}
}
