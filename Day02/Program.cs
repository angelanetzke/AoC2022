var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

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
					case "A": //Other chooses rock
						totalScore += 3;
						break;
					case "B": //Other chooses paper
						totalScore += 0;
						break;
					case "C": //Other chooses scissors
						totalScore += 6;
						break;
				}
				break;
			case "Y": //I choose paper
				totalScore += 2;
				switch (otherShape)
				{
					case "A": //Other chooses rock
						totalScore += 6;
						break;
					case "B": //Other chooses paper
						totalScore += 3;
						break;
					case "C": //Other chooses scissors
						totalScore += 0;
						break;
				}
				break;
			case "Z": //I choose scissors
				totalScore += 3;
				switch (otherShape)
				{
					case "A": //Other chooses rock
						totalScore += 0;
						break;
					case "B": //Other chooses paper
						totalScore += 6;
						break;
					case "C": //Other chooses scissors
						totalScore += 3;
						break;
				}
				break;
		}
	}
	Console.WriteLine($"Part 1: {totalScore}");
}

static void Part2(string[] allLines)
{
	var totalScore = 0L;
	foreach (string thisLine in allLines)
	{
		var otherShape = thisLine.Split(' ')[0];
		var myAction = thisLine.Split(' ')[1];
		switch (otherShape)
		{
			case "A": //Other chooses rock
				switch (myAction)
				{
					case "X": //I choose scissors to lose
						totalScore += 3 + 0;
						break;
					case "Y": //I choose rock to draw
						totalScore += 1 + 3;
						break;
					case "Z": //I choose paper to win
						totalScore += 2 + 6;
						break;
				}
				break;
			case "B": //Other chooses paper
				switch (myAction)
				{
					case "X": //I choose rock to lose
						totalScore += 1 + 0;
						break;
					case "Y": //I choose paper to draw
						totalScore += 2 + 3;
						break;
					case "Z": //I choose scissors to win
						totalScore += 3 + 6;
						break;
				}
				break;
			case "C": //Other chooses scissors
				switch (myAction)
				{
					case "X": //I choose paper to lose
						totalScore += 2 + 0;
						break;
					case "Y": //I choose scissors to draw
						totalScore += 3 + 3;
						break;
					case "Z": //I choose rock to win
						totalScore += 1 + 6;
						break;
				}
				break;
		}
	}
	Console.WriteLine($"Part 2: {totalScore}");
}
