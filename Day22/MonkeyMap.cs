using System.Text.RegularExpressions;

namespace Day22
{
	internal class MonkeyMap
	{
		private readonly List<string> mapData = new ();
		private string directions = "";
		private enum Facing { RIGHT, DOWN, LEFT, UP };
		private static readonly int facingCount = 4;
		private static readonly Regex tokenRegex = new Regex(@"^(\d+|[RL]{1})");

		public void AddRow(string newRow)
		{
			mapData.Add(newRow);
		}

		public void AddDirections(string newDirections)
		{
			directions = newDirections;
		}

		public int GetPassword()
		{
			var currentPosition = (0, GetMinColumn(0));
			var currentFacing = Facing.RIGHT;
			var tokenIndex = 0;
			while (tokenIndex < directions.Length)
			{
				var nextToken = tokenRegex.Match(directions.Substring(tokenIndex)).Value;
				if (nextToken == "L")
				{
					currentFacing = (Facing)(((int)currentFacing + facingCount - 1) % facingCount);
				}
				else if (nextToken == "R")
				{
					currentFacing = (Facing)(((int)currentFacing + 1) % facingCount);
				}
				else
				{
					var stepCount = int.Parse(nextToken);
					for (int thisStep = 1; thisStep <= stepCount; thisStep++)
					{
						var nextPosition = GetNextPosition(currentPosition, currentFacing);
						if (mapData[nextPosition.Item1][nextPosition.Item2] == '.')
						{
							currentPosition = nextPosition;
						}
					}
				}
				tokenIndex += nextToken.Length;
			}
			return 1000 * (currentPosition.Item1 + 1) + 4 * (currentPosition.Item2 + 1) + (int)currentFacing;
		}

		private (int, int) GetNextPosition((int, int) currentPosition, Facing currentFacing)
		{
			var row = currentPosition.Item1;
			var column = currentPosition.Item2;
			switch (currentFacing)
			{
				case Facing.RIGHT:
					if (column == GetMaxColumn(row))
					{
						return (row, GetMinColumn(row));
					}
					else
					{
						return (row, column + 1);
					}
				case Facing.DOWN:
					if (row == GetMaxRow(column))
					{
						return (GetMinRow(column), column);
					}
					else
					{
						return (row + 1, column);
					}
				case Facing.LEFT:
					if (column == GetMinColumn(row))
					{
						return (row, GetMaxColumn(row));
					}
					else
					{
						return (row, column - 1);
					}
				case Facing.UP:
					if (row == GetMinRow(column))
					{
						return (GetMaxRow(column), column);
					}
					else
					{
						return (row - 1, column);
					}
			}
			return currentPosition;
		}

		private int GetMinColumn(int row)
		{
			if (mapData[row].IndexOf('#') == -1)
			{
				return mapData[row].IndexOf('.');
			}
			else if (mapData[row].IndexOf('.') == -1)
			{
				return mapData[row].IndexOf('#');
			}
			else
			{
				return Math.Min(mapData[row].IndexOf('.'), mapData[row].IndexOf('#'));
			}
		}
		private int GetMaxColumn(int row)
		{
			return mapData[row].Length - 1;
		}

		private int GetMinRow(int column)
		{
			var columnString = new string(mapData.Where(x => column < x.Length).Select(x => x[column]).ToArray());
			if (columnString.IndexOf('#') == -1)
			{
				return columnString.IndexOf('.');
			}
			else if (columnString.IndexOf('.') == -1)
			{
				return columnString.IndexOf('#');
			}
			else
			{
				return Math.Min(columnString.IndexOf('.'), columnString.IndexOf('#'));
			}
		}

		private int GetMaxRow(int column)
		{
			var columnString = new string(mapData.Where(x => column < x.Length).Select(x => x[column]).ToArray());
			return columnString.Length - 1;
		}

	}
}