using Day19;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var regex = new Regex(@"\d+");
	var blueprintList = new List<Blueprint>();
	foreach (string thisLine in allLines)
	{
		var matches = regex.Matches(thisLine);
		blueprintList.Add(new Blueprint(
			int.Parse(matches[0].ToString()),
			int.Parse(matches[1].ToString()),
			int.Parse(matches[2].ToString()),
			int.Parse(matches[3].ToString()),
			int.Parse(matches[4].ToString()),
			int.Parse(matches[5].ToString()),
			int.Parse(matches[6].ToString())));
	}
	Blueprint.SetTotalTime(24);
	var qualityLevelSum = 0;
	foreach (Blueprint thisBlueprint in blueprintList)
	{
		qualityLevelSum += thisBlueprint.GetID() * thisBlueprint.GetMaxGeodes();
	}
	Console.WriteLine($"Part 1: {qualityLevelSum}");
}

static void Part2(string[] allLines)
{
	var regex = new Regex(@"\d+");
	var blueprintList = new List<Blueprint>();
	for (int i = 0; i < 3; i++)
	{
		var matches = regex.Matches(allLines[i]);
		blueprintList.Add(new Blueprint(
			int.Parse(matches[0].ToString()),
			int.Parse(matches[1].ToString()),
			int.Parse(matches[2].ToString()),
			int.Parse(matches[3].ToString()),
			int.Parse(matches[4].ToString()),
			int.Parse(matches[5].ToString()),
			int.Parse(matches[6].ToString())));
	}
	Blueprint.SetTotalTime(32);
	var geodeProduct = 1;
	foreach (Blueprint thisBlueprint in blueprintList)
	{
		geodeProduct *= thisBlueprint.GetMaxGeodes();
	}
	Console.WriteLine($"Part 2: {geodeProduct}");
}
