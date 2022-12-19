using Day17;

var allText = File.ReadAllLines("input.txt")[0];
Part1(allText);

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
