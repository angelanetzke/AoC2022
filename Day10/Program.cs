using Day10;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var theDevice = new Device(allLines);
	Console.WriteLine($"Part 1: {theDevice.GetSignalStrengthSum()}");
}