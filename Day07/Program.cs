using Day07;

var allLines = File.ReadAllLines("input.txt");
var theFileSystem = new FileSystem(allLines);
Part1(theFileSystem);
Part2(theFileSystem);

static void Part1(FileSystem theFileSystem)
{
	var part1Answer = theFileSystem.GetTotalSizeUnderSize(100000L);
	Console.WriteLine($"Part 1: {part1Answer}");
}

static void Part2(FileSystem theFileSystem)
{
	var part2Answer = theFileSystem.FreeUpSpace(30000000L);
	Console.WriteLine($"Part 2: {part2Answer}");
}
