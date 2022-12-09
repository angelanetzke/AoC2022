var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var visited = new HashSet<(int, int)>();
	visited.Add((0, 0));
	var head = (0, 0);
	var tail = (0, 0);
	foreach (string thisLine in allLines)
	{
		var direction = thisLine.Split(' ')[0];
		var steps = int.Parse(thisLine.Split(' ')[1]);
		for (int i = 0; i < steps; i++)
		{
			switch (direction)
			{
				case "U":
					head = (head.Item1, head.Item2 - 1);
					break;
				case "D":
					head = (head.Item1, head.Item2 + 1);
					break;
				case "L":
					head = (head.Item1 - 1, head.Item2);
					break;
				case "R":
					head = (head.Item1 + 1, head.Item2);
					break;
			}
			if (Math.Abs(head.Item1 - tail.Item1) > 1 || Math.Abs(head.Item2 - tail.Item2) > 1)
			{
				var newTailX = tail.Item1 + GetDelta(head.Item1, tail.Item1);
				var newTailY = tail.Item2 + GetDelta(head.Item2, tail.Item2);
				tail = (newTailX, newTailY);
				visited.Add(tail);
			}			
		}
	}
	Console.WriteLine($"Part 1: {visited.Count}");
}

static int GetDelta(int first, int second)
{
	var delta = first - second;
	if (Math.Abs(delta) > 1)
	{
		delta /= Math.Abs(delta);
	}
	return delta;
}