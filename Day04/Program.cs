using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var count = 0;
	var regex = new Regex(@"^(?<min1>\d+)-(?<max1>\d+),(?<min2>\d+)-(?<max2>\d+)$");
	foreach (string thisLine in allLines)
	{
		var min1 = int.Parse(regex.Match(thisLine).Groups["min1"].Value);
		var max1 = int.Parse(regex.Match(thisLine).Groups["max1"].Value);
		var min2 = int.Parse(regex.Match(thisLine).Groups["min2"].Value);
		var max2 = int.Parse(regex.Match(thisLine).Groups["max2"].Value);
		if (min1 <= min2 && max2 <= max1)
		{
			count++;
		}
		else if (min2 <= min1 && max1 <= max2)
		{
			count++;
		}
	}
	Console.WriteLine($"Part 1: {count}");
}