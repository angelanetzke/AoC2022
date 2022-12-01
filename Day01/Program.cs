var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

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
