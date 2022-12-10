using Day10;

var allLines = File.ReadAllLines("input.txt");
var theDevice = new Device(allLines);
Part1(theDevice);
Part2(theDevice);

static void Part1(Device theDevice)
{
	Console.WriteLine($"Part 1: {theDevice.GetSignalStrengthSum()}");
}

static void Part2(Device theDevice)
{
	var display = theDevice.GetDisplay();
	Console.WriteLine("Part 2:");
	for (int i = 0; i < display.Length; i += Device.SCREEN_WIDTH)
	{
		Console.WriteLine(display.Substring(i, Device.SCREEN_WIDTH));
	}
}