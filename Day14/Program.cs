var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var occupied = GetInitialOccupied(allLines);
	var maxY = occupied.Select(x => x.Item2).Max();
	int sandStartX = 500;
	int sandStartY = 0;
	int successCount = 0;
	bool wasSuccessful = true;
	while (wasSuccessful)
	{
		var sandX = sandStartX;
		var sandY = sandStartY;
		bool canMove = true;
		while (canMove)
		{
			if (!occupied.Contains((sandX, sandY + 1)))
			{
				sandY++;
			}
			else if (!occupied.Contains((sandX - 1, sandY + 1)))
			{
				sandX--;
				sandY++;
			}
			else if (!occupied.Contains((sandX + 1, sandY + 1)))
			{
				sandX++;
				sandY++;
			}
			wasSuccessful = sandY < maxY;
			canMove = wasSuccessful &&
				(!occupied.Contains((sandX, sandY + 1))
				|| !occupied.Contains((sandX - 1, sandY + 1))
				|| !occupied.Contains((sandX + 1, sandY + 1)));
			if (wasSuccessful && !canMove)
			{
				occupied.Add((sandX, sandY));
			}
		}
		successCount = wasSuccessful ? successCount + 1 : successCount;
	}
	Console.WriteLine($"Part 1: {successCount}");
}

static HashSet<(int, int)> GetInitialOccupied(string[] allLines)
{
	var occupied = new HashSet<(int, int)>();
	foreach (string thisLine in allLines)
	{
		var points = thisLine.Split(" -> ");
		var lastX = int.Parse(points[0].Split(',')[0]);
		var lastY = int.Parse(points[0].Split(',')[1]);
		occupied.Add((lastX, lastY));
		for (int i = 1; i < points.Length; i++)
		{
			var thisX = int.Parse(points[i].Split(',')[0]);
			var thisY = int.Parse(points[i].Split(',')[1]);
			var deltaX = thisX == lastX ? 0 : (thisX - lastX) / Math.Abs(thisX - lastX);
			var deltaY = thisY == lastY ? 0 : (thisY - lastY) / Math.Abs(thisY - lastY);
			var newX = lastX;
			var newY = lastY;
			while (newX != thisX || newY != thisY)
			{
				newX += deltaX;
				newY += deltaY;
				occupied.Add((newX, newY));
			}
			lastX = thisX;
			lastY = thisY;
		}
	}
	return occupied;
}