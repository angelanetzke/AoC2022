using Day22;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var theMap = new MonkeyMap();
	var mapLineRegex = new Regex("[ .#]+");
	foreach (string thisLine in allLines)
	{
		if (mapLineRegex.IsMatch(thisLine))
		{
			theMap.AddRow(thisLine);
		}
		else if (thisLine.Length > 0)
		{
			theMap.AddDirections(thisLine);
		}
	}
	var password = theMap.GetPassword();
	Console.WriteLine($"Part 1: {password}");
}
