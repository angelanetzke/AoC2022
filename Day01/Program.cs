var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var currentElfCalories = 0L;
	var maxCalories = long.MinValue;
	foreach (string thisCalorieValue in allLines)
	{
		if (thisCalorieValue.Length == 0)
		{
			maxCalories = Math.Max(maxCalories, currentElfCalories);
  		currentElfCalories = 0;
		}
		else
		{
			currentElfCalories += long.Parse(thisCalorieValue);
		}
	}
	if (currentElfCalories > 0)
	{
		maxCalories = Math.Max(maxCalories, currentElfCalories);
	}
	Console.WriteLine($"Part 1: {maxCalories}");
}

static void Part2(string[] allLines)
{
	var allElvesCalories = new List<long>();
	var currentElfCalories = 0L;
	foreach (string thisCalorieValue in allLines)
	{
		if (thisCalorieValue.Length == 0)
		{
			allElvesCalories.Add(currentElfCalories);
  		currentElfCalories = 0;
		}
		else
		{
			currentElfCalories += long.Parse(thisCalorieValue);
		}
	}
	if (currentElfCalories > 0)
	{
		allElvesCalories.Add(currentElfCalories);
	}	
	allElvesCalories.Sort();
	allElvesCalories.Reverse();
	var top3Total = allElvesCalories[0] + allElvesCalories[1] + allElvesCalories[2];
	Console.WriteLine($"Part 2: {top3Total}");
}