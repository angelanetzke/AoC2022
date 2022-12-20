using Day18;

var allLines = File.ReadAllLines("input.txt");
var allCubes = new HashSet<Cube>();
foreach (string thisLine in allLines)
	{
		allCubes.Add(new Cube(thisLine));
	}
Part1(allCubes);
Part2(allCubes);

static void Part1(HashSet<Cube> allCubes)
{
	var totalSurfaces = 0;
	foreach (Cube thisCube in allCubes)
	{
		var neighbors = thisCube.GetNeighbors();
		var thisCubeSurfaces = 6 - neighbors.Count(x => allCubes.Contains(x));
		totalSurfaces += thisCubeSurfaces;
	}
	Console.WriteLine($"Part 1: {totalSurfaces}");
}

static void Part2(HashSet<Cube> allCubes)
{
	var totalSurfaces = 0;
	var minX = allCubes.Select(x => x.GetLocation().Item1).Min() - 1;
	var minY = allCubes.Select(x => x.GetLocation().Item2).Min() - 1;
	var minZ = allCubes.Select(x => x.GetLocation().Item3).Min() - 1;
	var maxX = allCubes.Select(x => x.GetLocation().Item1).Max() + 1;
	var maxY = allCubes.Select(x => x.GetLocation().Item2).Max() + 1;
	var maxZ = allCubes.Select(x => x.GetLocation().Item3).Max() + 1;
	var visitedWater = new HashSet<Cube>();
	var waterQueue = new Queue<Cube>();
	waterQueue.Enqueue(new Cube(minX, minY, minZ));
	while (waterQueue.Count > 0)
	{
		var current = waterQueue.Dequeue();
		visitedWater.Add(current);
		var neighbors = current.GetNeighbors();
		foreach (Cube thisNeighbor in neighbors)
		{
			if (allCubes.Contains(thisNeighbor))
			{
				totalSurfaces += 1;
			}
			else if (!visitedWater.Contains(thisNeighbor) && !waterQueue.Contains(thisNeighbor)
				&& minX <= thisNeighbor.GetLocation().Item1 && thisNeighbor.GetLocation().Item1 <= maxX
				&& minY <= thisNeighbor.GetLocation().Item2 && thisNeighbor.GetLocation().Item2 <= maxY
				&& minZ <= thisNeighbor.GetLocation().Item3 && thisNeighbor.GetLocation().Item3 <= maxZ)
			{
				waterQueue.Enqueue(thisNeighbor);
			}			
		}
	}	
	Console.WriteLine($"Part 2: {totalSurfaces}");
}



