namespace Day17
{
	internal class RockTower
	{
		private string wind = "";
		private int windIndex = 0;
		private int rockType = -1;
		private (int, int) location;
		private static readonly int rockTypeCount = 5;
		private static readonly int cavernWidth = 7;
		private static readonly int[] rockWidths = new int[] { 4, 3, 3, 1, 2 };
		private readonly List<HashSet<int>> settledRocks = new List<HashSet<int>>();
		private static readonly Dictionary<int, (int, int)[]> relativeLocations = new ()
		{
			[0] = new (int, int)[] { (0, 0), (1, 0), (2, 0), (3,0) },
			[1] = new (int, int)[] { (1, 2), (0, 1), (1, 1), (2, 1), (1, 0) },
			[2] = new (int, int)[] { (2, 2), (2, 1), (0, 0), (1, 0), (2, 0) },
			[3] = new (int, int)[] { (0, 3), (0, 2), (0, 1), (0, 0) },
			[4] = new (int, int)[] { (0, 1), (1, 1), (0, 0), (1, 0) }
		};
		private static readonly Dictionary<int, (int, int)[]> relativeBottomLocations = new ()
		{
			[0] = new (int, int)[] { (0, 0), (1, 0), (2, 0), (3,0) },
			[1] = new (int, int)[] { (0, 1), (1, 0), (2, 1) },
			[2] = new (int, int)[] { (0, 0), (1, 0), (2, 0) },
			[3] = new (int, int)[] { (0, 0) },
			[4] = new (int, int)[] { (0, 0), (1, 0) }
		};
		private static readonly Dictionary<int, (int, int)[]> relativeLeftLocations = new ()
		{
			[0] = new (int, int)[] { (0, 0) },
			[1] = new (int, int)[] { (1, 2), (0, 1), (1, 0) },
			[2] = new (int, int)[] { (2, 2), (2, 1), (0, 0) },
			[3] = new (int, int)[] { (0, 3), (0, 2), (0, 1), (0, 0) },
			[4] = new (int, int)[] { (0, 1), (0, 0) },
		};
		private static readonly Dictionary<int, (int, int)[]> relativeRightLocations = new ()
		{
			[0] = new (int, int)[] { (3, 0) },
			[1] = new (int, int)[] { (1, 2), (2, 1), (1, 0) },
			[2] = new (int, int)[] { (2, 2), (2, 1), (2, 0) },
			[3] = new (int, int)[] { (0, 3), (0, 2), (0, 1), (0, 0) },
			[4] = new (int, int)[] { (1, 1), (1, 0) },
		};		
		
		public RockTower(string wind)
		{
			this.wind = wind;
			for (int i = 0; i < cavernWidth; i++)
			{
				settledRocks.Add(new HashSet<int>());
			}
		}

		public void NextRock()
		{
			rockType = (rockType + 1) % rockTypeCount;
			location = (2, GetTowerHeight() + 4);
		}

		public bool CanFall()
		{
			if (location.Item2 == 1)
			{
				return false;
			}
			else
			{
				for (int i = 0; i < relativeBottomLocations[rockType].Length; i++)
				{
					var absoluteX = location.Item1 + relativeBottomLocations[rockType][i].Item1;
					var absoluteY = location.Item2 + relativeBottomLocations[rockType][i].Item2;
					if (settledRocks[absoluteX].Contains(absoluteY - 1))
					{
						return false;
					}
				}
				return true;
			}
		}

		public void Fall()
		{
			location = (location.Item1, location.Item2 - 1);			
		}

		public bool CanSlideLeft()
		{
			if (location.Item1 == 0)
			{
				return false;
			}
			else
			{
				for (int i = 0; i < relativeLeftLocations[rockType].Length; i++)
				{
					var absoluteX = location.Item1 + relativeLeftLocations[rockType][i].Item1;
					var absoluteY = location.Item2 + relativeLeftLocations[rockType][i].Item2;
					if (settledRocks[absoluteX - 1].Contains(absoluteY))
					{
						return false;
					}
				}
				return true;
			}
		}

		public bool CanSlideRight()
		{
			if (location.Item1 + rockWidths[rockType] == cavernWidth)
			{
				return false;
			}
			else
			{
				for (int i = 0; i < relativeRightLocations[rockType].Length; i++)
				{
					var absoluteX = location.Item1 + relativeRightLocations[rockType][i].Item1;
					var absoluteY = location.Item2 + relativeRightLocations[rockType][i].Item2;
					if (settledRocks[absoluteX + 1].Contains(absoluteY))
					{
						return false;
					}
				}
				return true;
			}
		}

		public void Push()
		{
			var nextWind = GetNextWind();
			if (nextWind == '<' && CanSlideLeft())
			{
				location = (Math.Max(location.Item1 - 1, 0), location.Item2);
			}
			else if (nextWind == '>' && CanSlideRight())
			{
				location = (Math.Min(location.Item1 + 1, cavernWidth - rockWidths[rockType]), location.Item2);
			}
		}

		public void UpdateCavern()
		{
			for (int i = 0; i < relativeLocations[rockType].Length; i++)
			{
				var absoluteX = location.Item1 + relativeLocations[rockType][i].Item1;
				var absoluteY = location.Item2 + relativeLocations[rockType][i].Item2;
				settledRocks[absoluteX].Add(absoluteY);
			}
		}

		public int GetTowerHeight()
		{
			return settledRocks.Select(x => x.Count == 0 ? 0 : x.Max()).Max();
		}		
		
		public char GetNextWind()
		{
			char nextChar = wind[windIndex];
			windIndex = (windIndex + 1) % wind.Length;
			return nextChar;
		}
	}
}