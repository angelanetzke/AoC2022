namespace Day15
{
	internal class EmptyRange
	{
		private readonly int min;
		private readonly int max;
		public EmptyRange(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		public int GetLength()
		{
			return max - min + 1;
		}

		public List<EmptyRange> Merge(EmptyRange other)
		{
			List<EmptyRange> result = new ();
			if (Math.Max(min, other.min) <= Math.Min(max, other.max)) //overlap
			{
				result.Add(new EmptyRange(Math.Min(min, other.min), Math.Max(max, other.max)));
			}
			else
			{
				result.Add(this);
				result.Add(other);
			}
			return result;
		}

		public override bool Equals(object? obj)
		{
			if (obj is EmptyRange other)
			{
				return min == other.min && max == other.max;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return (min + max).GetHashCode();
		}

		public override string ToString()
		{
			return min + "," + max;
		}
		
	}
}