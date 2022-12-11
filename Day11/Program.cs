using Day11;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var group = GetGroup(allLines);
	const int TURN_COUNT = 20;
	for (int turn = 1; turn <= TURN_COUNT; turn++)
	{
		foreach (Monkey thisMonkey in group)
		{
			thisMonkey.TakeTurn();
		}
	}
	List<long> inspectionCounts = group.Select(x => x.GetInspectionCount()).ToList();
	inspectionCounts.Sort();
	inspectionCounts.Reverse();
	var monkeyBusiness = inspectionCounts[0] * inspectionCounts[1];
	Console.WriteLine($"Part 1: {monkeyBusiness}");
}

static void Part2(string[] allLines)
{
	var group = GetGroup(allLines);
	var reductionFactor = 1L;
	foreach (Monkey thisMonkey in group)
	{
		reductionFactor *= thisMonkey.GetDivisor();
	}
	foreach (Monkey thisMonkey in group)
	{
		thisMonkey.SetReductionFactor(reductionFactor);
	}
	const int TURN_COUNT = 10000;
	for (int turn = 1; turn <= TURN_COUNT; turn++)
	{
		foreach (Monkey thisMonkey in group)
		{
			thisMonkey.TakeTurn();
		}
	}
	List<long> inspectionCounts = group.Select(x => x.GetInspectionCount()).ToList();
	inspectionCounts.Sort();
	inspectionCounts.Reverse();
	var monkeyBusiness = inspectionCounts[0] * inspectionCounts[1];
	Console.WriteLine($"Part 2: {monkeyBusiness}");
}

static List<Monkey> GetGroup(string[] allLines)
{
	var group = new List<Monkey>();
	var thisMonkeyData = new List<string>();
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length == 0)
		{
			var newMonkey = new Monkey(group, thisMonkeyData);
			group.Add(newMonkey);
			thisMonkeyData.Clear();
		}
		else
		{
			thisMonkeyData.Add(thisLine);
		}
	}
	if (thisMonkeyData.Count > 0)
	{
		var newMonkey = new Monkey(group, thisMonkeyData);
		group.Add(newMonkey);
	}
	return group;
}