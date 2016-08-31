namespace NotLimited.Framework.Common.Helpers
{
	public class MinMaxTracker
	{
		public double Min { get; private set; } = double.MaxValue;
		public double Max { get; private set; } = double.MinValue;

	    public double Range { get; private set; } = double.NaN;

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

			    Range = Max - Min;
			}
		}

	    public double Normalize(double value)
	    {
	        return (value - Min) / Range;
	    }

	    public void Reset()
	    {
	        Min = double.MaxValue;
	        Max = double.MinValue;
	        Range = double.NaN;
	    }
	}
}