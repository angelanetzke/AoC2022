using Day12;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] alLines)
{
	var theAreaMap = new AreaMap(alLines);
	var distance = theAreaMap.GetDistanceToEnd();
	Console.WriteLine($"Part 1: {distance}");
}
