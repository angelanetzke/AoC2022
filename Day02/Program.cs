var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var totalScore = 0L;
	foreach (string thisLine in allLines)
	{
		var otherShape = thisLine.Split(' ')[0];
		var myShape = thisLine.Split(' ')[1];
		switch (myShape)
		{
			case "X": //I choose rock
				totalScore += 1;
				switch (otherShape)
				{
					case "A": //Other choose rock
						totalScore += 3;
						break;
					case "B": //Other choose paper
						totalScore += 0;
						break;
					case "C": //Other choose scissors
						totalScore += 6;
						break;
				}
				break;
			case "Y": //I choose paper
				totalScore += 2;
				switch (otherShape)
				{
					case "A": //Other choose rock
						totalScore += 6;
						break;
					case "B": //Other choose paper
						totalScore += 3;
						break;
					case "C": //Other choose scissors
						totalScore += 0;
						break;
				}
				break;
			case "Z": //I choose scissors
				totalScore += 3;
				switch (otherShape)
				{
					case "A": //Other choose rock
						totalScore += 0;
						break;
					case "B": //Other choose paper
						totalScore += 6;
						break;
					case "C": //Other choose scissors
						totalScore += 3;
						break;
				}
				break;
		}
	}
	Console.WriteLine($"Part 1: {totalScore}");
}

