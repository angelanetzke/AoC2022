var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var prioritySum = 0L;
	foreach (string thisLine in allLines)
	{
		var firstCompartment = thisLine[..(thisLine.Length / 2)];
		var secondCompartment = thisLine[((thisLine.Length / 2))..];
		foreach (char thisItem in firstCompartment)
		{
			if (secondCompartment.Contains(thisItem))
			{
				if (Char.IsLower(thisItem))
				{
					prioritySum += thisItem - 'a' + 1;
				}
				else
				{
					prioritySum += thisItem - 'A' + 27;
				}
				break;
			}
		}
	}
	Console.WriteLine($"Part1: {prioritySum}");
}

static void Part2(string[] allLines)
{
	var prioritySum = 0L;
	for (int i = 0; i < allLines.Length; i += 3)
	{
		foreach (char thisItem in allLines[i])
		{
			if (allLines[i + 1].Contains(thisItem) && allLines[i + 2].Contains(thisItem))
			{
				if (Char.IsLower(thisItem))
				{
					prioritySum += thisItem - 'a' + 1;
				}
				else
				{
					prioritySum += thisItem - 'A' + 27;
				}
				break;
			}
		}
	}
	Console.WriteLine($"Part2: {prioritySum}");
}