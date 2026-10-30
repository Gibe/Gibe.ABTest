namespace Gibe.AbTest
{
	public interface IRandomNumber
	{
		int Number(int max);
	}

	public class FakeRandomNumber : IRandomNumber
	{
		private readonly int[] _numbers;
		private int _currentIndex;

		public FakeRandomNumber(int[] numbers)
		{
			_numbers = numbers;
		}

		public int Number(int max)
		{
			return _numbers[_currentIndex++];
		}
	}
}
