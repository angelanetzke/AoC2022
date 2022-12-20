namespace Day18
{
	internal class Cube
	{
		private readonly int x;
		private readonly int y;
		private readonly int z;

		public Cube(string data)
		{
			var coordinates = data.Split(',');
			x = int.Parse(coordinates[0]);
			y = int.Parse(coordinates[1]);
			z = int.Parse(coordinates[2]);
		}

		public Cube(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public (int, int, int) GetLocation()
		{
			return (x, y, z);
		}

		public List<Cube> GetNeighbors()
		{
			return new List<Cube>()
			{
				new Cube(x + 1, y, z),
				new Cube(x - 1, y, z),
				new Cube(x, y + 1, z),
				new Cube(x, y - 1, z),
				new Cube(x, y, z + 1),
				new Cube(x, y, z - 1)
			};
		}

		public override bool Equals(object? obj)
		{
			if (obj is Cube other)
			{
				return x == other.x && y == other.y && z == other.z;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return (x.GetHashCode() + y.GetHashCode() + z.GetHashCode()).GetHashCode();
		}


	}
}