using Day17;

var allText = File.ReadAllLines("input.txt")[0];
Part1(allText);
Part2(allText);

static void Part1(string allText)
{
	var theTower = new RockTower(allText);
	var rockCount = 2022;
	for (int rock = 1; rock <= rockCount; rock++)
	{
		theTower.NextRock();
		theTower.Push();
		while (theTower.CanFall())
		{
			theTower.Fall();
			theTower.Push();			
		}
		theTower.UpdateCavern();
	}
	Console.WriteLine($"Part 1: {theTower.GetTowerHeight()}");
}

static void Part2(string allText)
{
	var totalCycles = 1000000000000L;
	var theTower = new RockTower(allText);
	var rock = 1;
	var cycleStart = -1;
	var cycleLength = 0L;
	var heightAtCycleStart = 0L;
	var heightOfCycle = 0L;
	var cycleEnd = -1L;
	var towerEnd = -1L;
	var heightAtTowerEnd = 0L;
	var sequentialHeights = new List<long>();
	var previousRows = new Dictionary<(int, int, int, int, int, int, int, int, int), int>();
	while (heightAtTowerEnd == 0)
	{
		var nextRockData = theTower.NextRock();
		if (previousRows.ContainsKey(nextRockData) && cycleLength == 0L)
		{
			cycleLength = rock - previousRows[nextRockData];
			cycleStart = previousRows[nextRockData];
			heightAtCycleStart = sequentialHeights[cycleStart];
			cycleEnd = cycleStart + cycleLength;
			towerEnd = (totalCycles - cycleStart) % cycleLength + cycleStart + cycleLength;
		}
		else
		{
			previousRows[nextRockData] = rock;
		}
		theTower.Push();
		while (theTower.CanFall())
		{
			theTower.Fall();
			theTower.Push();			
		}
		theTower.UpdateCavern();
		if (cycleLength == 0)
		{
			sequentialHeights.Add(theTower.GetTowerHeight());
		}
		if (rock == cycleEnd)
		{
			heightOfCycle = theTower.GetTowerHeight() - heightAtCycleStart;
		}
		if (rock == towerEnd)
		{
			heightAtTowerEnd = theTower.GetTowerHeight() - (heightAtCycleStart + heightOfCycle);
		}
		rock++;
	}
	var cycleCount = (totalCycles - cycleStart) / cycleLength;
	var totalTowerSize = heightAtCycleStart + cycleCount * heightOfCycle + heightAtTowerEnd;
	Console.WriteLine($"Part 2: {totalTowerSize}");	
	
}
