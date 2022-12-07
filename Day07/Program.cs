using Day07;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var theFileSystem = new FileSystem(allLines);
	var part1Answer = theFileSystem.GetTotalSizeUnderSize(100000L);
	Console.WriteLine($"Part 1: {part1Answer}");
}
