var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var visibleTrees = new HashSet<(int, int)>();
	var largestSoFar = -1;
	//Top to bottom
	for (int column = 0; column < allLines[0].Length; column++)
	{
		var row = 0;
		largestSoFar = -1;
		while (row < allLines.Length && largestSoFar < 9)
		{
			if (int.Parse(allLines[row][column].ToString()) > largestSoFar)
			{
				largestSoFar = int.Parse(allLines[row][column].ToString());
				visibleTrees.Add((row, column));
			}
			row++;
		}
	}
	//Bottom to top
	for (int column = 0; column < allLines[0].Length; column++)
	{
		var row = allLines.Length - 1;
		largestSoFar = -1;
		while (row >= 0 && largestSoFar < 9)
		{
			if (int.Parse(allLines[row][column].ToString()) > largestSoFar)
			{
				largestSoFar = int.Parse(allLines[row][column].ToString());
				visibleTrees.Add((row, column));
			}
			row--;
		}
	}
	//Left to right
	for (int row = 0; row < allLines.Length; row++)
	{
		var column = 0;
		largestSoFar = -1;
		while (column < allLines[0].Length && largestSoFar < 9)
		{
			if (int.Parse(allLines[row][column].ToString()) > largestSoFar)
			{
				largestSoFar = int.Parse(allLines[row][column].ToString());
				visibleTrees.Add((row, column));
			}
			column++;
		}
	}
	//Right to left
	for (int row = 0; row < allLines.Length; row++)
	{
		var column = allLines[0].Length - 1;
		largestSoFar = -1;
		while (column >= 0 && largestSoFar < 9)
		{
			if (int.Parse(allLines[row][column].ToString()) > largestSoFar)
			{
				largestSoFar = int.Parse(allLines[row][column].ToString());
				visibleTrees.Add((row, column));
			}
			column--;
		}
	}
	Console.WriteLine($"Part 1: {visibleTrees.Count}");
}

static void Part2(string[] allLines)
{
	var bestViewScore = int.MinValue;
	var scores = new List<int>();
	for (int row = 0; row < allLines.Length; row++)
	{
		for (int column = 0; column < allLines[0].Length; column++)
		{
			if (row == 0 || row == allLines.Length - 1 || column == 0 || column == allLines[0].Length - 1)
			{
				continue;
			}
			else 
			{
				var thisTreeHeight = int.Parse(allLines[row][column].ToString());
				scores.Clear();
				//Count right
				var thisDirectionScore = 1;
				while (column + thisDirectionScore + 1 < allLines[0].Length 
					&& int.Parse(allLines[row][column + thisDirectionScore].ToString()) < thisTreeHeight)
				{
					thisDirectionScore++;
				}
				scores.Add(thisDirectionScore);
				//Count left
				thisDirectionScore = 1;
				while (column - thisDirectionScore - 1 >= 0 
					&& int.Parse(allLines[row][column - thisDirectionScore].ToString()) < thisTreeHeight)
				{
					thisDirectionScore++;
				}
				scores.Add(thisDirectionScore);
				//Count down
				thisDirectionScore = 1;
				while (row + thisDirectionScore + 1 < allLines.Length 
					&& int.Parse(allLines[row + thisDirectionScore][column].ToString()) < thisTreeHeight)
				{
					thisDirectionScore++;
				}
				scores.Add(thisDirectionScore);
				//Count up
				thisDirectionScore = 1;
				while (row - thisDirectionScore - 1 >= 0 
					&& int.Parse(allLines[row - thisDirectionScore][column].ToString()) < thisTreeHeight)
				{
					thisDirectionScore++;
				}
				scores.Add(thisDirectionScore);
				var thisViewScore = scores.Aggregate((current, next) => current * next);
				bestViewScore = Math.Max(bestViewScore, thisViewScore);
			}
		}
	}
	Console.WriteLine($"Part 2: {bestViewScore}");
}