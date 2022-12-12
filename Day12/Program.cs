using Day12;

var allLines = File.ReadAllLines("input.txt");
var theAreaMap = new AreaMap(allLines);
Part1(theAreaMap);
Part2(theAreaMap);

static void Part1(AreaMap theAreaMap)
{
	var distance = theAreaMap.GetDistanceToEnd();
	Console.WriteLine($"Part 1: {distance}");
}

static void Part2(AreaMap theAreaMap)
{
	var distance = theAreaMap.GetHikingPathDistance();
	Console.WriteLine($"Part 2: {distance}");
}
