namespace Day15
{
	internal class Equation
	{
		private bool isXPositive;
		private bool isYPositive;
		private int sum;

		public Equation(bool isXPositive, bool isYPositive, int sum)
		{
			this.isXPositive = isXPositive;
			this.isYPositive = isYPositive;
			this.sum = sum;
		}
		
		public (int, int)? GetIntersection(Equation other)
		{
			if (isXPositive != other.isXPositive)
			{
				if (isYPositive == other.isYPositive && isYPositive)
				{
					var y = (sum + other.sum) / 2;
					var x = sum - y;
					return (x, y);
				}
				else if (isYPositive == other.isYPositive && !isYPositive)
				{
					var y = (sum + other.sum) / -2;
					var x = sum - y;
					return (x, y);
				}
				else
				{
					return null;
				}
			}
			else if (isYPositive != other.isYPositive)
			{
				if (isXPositive == other.isXPositive && isXPositive)
				{
					var x = (sum + other.sum) / 2;
					var y = sum - x;
					return (x, y);
				}
				else if (isXPositive == other.isXPositive && !isXPositive)
				{
					var x = (sum + other.sum) / -2;
					var y = sum - x;
					return (x, y);
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

	}
}