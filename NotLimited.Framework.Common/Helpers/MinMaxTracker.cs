namespace NotLimited.Framework.Common.Helpers
{
	public class MinMaxTracker
	{
		public double Min = double.MaxValue;
		public double Max = double.MinValue;

		public void Track(params double[] numbers)
		{
			for (int i = 0; i < numbers.Length; i++)
			{
				if (numbers[i] > Max)
				{
					Max = numbers[i];
				}

				if (numbers[i] < Min)
				{
					Min = numbers[i];
				}
			}
		}
	}
}