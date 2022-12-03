var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

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

