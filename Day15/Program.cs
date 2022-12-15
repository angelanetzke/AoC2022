using Day15;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	int targetY = 2000000;
	List<SensorAndBeacon> knownObjects = new ();
	var regex = new Regex(@"Sensor at x=(?<sensorX>-?\d+), y=(?<sensorY>-?\d+): " 
		+ @"closest beacon is at x=(?<beaconX>-?\d+), y=(?<beaconY>-?\d+)");
	foreach (string thisLine in allLines)
	{
		var sensorX = int.Parse(regex.Match(thisLine).Groups["sensorX"].Value);
		var sensorY = int.Parse(regex.Match(thisLine).Groups["sensorY"].Value);
		var beaconX = int.Parse(regex.Match(thisLine).Groups["beaconX"].Value);
		var beaconY = int.Parse(regex.Match(thisLine).Groups["beaconY"].Value);
		knownObjects.Add(new SensorAndBeacon(sensorX, sensorY, beaconX, beaconY));
	}	
	List<EmptyRange> targetYRanges = new ();
	foreach (SensorAndBeacon thisObject in knownObjects)
	{
		var thisRange = thisObject.GetEmptyRangeAtY(targetY);
		if (thisRange != null)
		{
			targetYRanges.Add(thisRange);
		}
	}
	HashSet<EmptyRange> temp = new ();
	var madeAnyMerges = true;
	var hasBeenMerged = new Dictionary<EmptyRange, bool>();
	while (madeAnyMerges && targetYRanges.Count > 1)
	{
		hasBeenMerged.Clear();
		targetYRanges.ForEach(x => hasBeenMerged[x] = false);
		temp.Clear();
		madeAnyMerges = false;
		for (int i = 0; i < targetYRanges.Count - 1; i++)
		{
			for (int ii = i + 1; ii < targetYRanges.Count; ii++)
			{
				var thisMerge = targetYRanges[i].Merge(targetYRanges[ii]);
				if (thisMerge.Count == 1)
				{
					madeAnyMerges = true;
					if (!thisMerge[0].Equals(targetYRanges[i]))
					{
						hasBeenMerged[targetYRanges[i]] = true;
					}
					if (!thisMerge[0].Equals(targetYRanges[ii]))
					{
						hasBeenMerged[targetYRanges[ii]] = true;
					}
				}
				thisMerge.ForEach(x => temp.Add(x));
			}
		}
		hasBeenMerged.Where(x => x.Value).ToList().ForEach(x => temp.Remove(x.Key));
		targetYRanges.Clear();
		targetYRanges.AddRange(temp);
	}
	var emptyCount = targetYRanges.Select(x => x.GetLength()).Sum();
	var objectsInTargetY = new HashSet<(int, int)>();
	var sensorsInTargetY = knownObjects.Where(x => x.GetSensorLocation().Item2 == targetY)
		.Select(x => x.GetSensorLocation()).ToList();
	var beaconsInTargetY = knownObjects.Where(x => x.GetBeaconLocation().Item2 == targetY)
		.Select(x => x.GetBeaconLocation()).ToList();
	sensorsInTargetY.ForEach(x => objectsInTargetY.Add(x));
	beaconsInTargetY.ForEach(x => objectsInTargetY.Add(x));
	emptyCount -= objectsInTargetY.Count;
	Console.WriteLine($"Part 1: {emptyCount}");	
}



static void Part2(string[] allLines)
{
	var minX = 0;
	var maxX = 4000000;
	var minY = 0;
	var maxY = 4000000;
	List<SensorAndBeacon> knownObjects = new ();
	var regex = new Regex(@"Sensor at x=(?<sensorX>-?\d+), y=(?<sensorY>-?\d+): " 
		+ @"closest beacon is at x=(?<beaconX>-?\d+), y=(?<beaconY>-?\d+)");
	foreach (string thisLine in allLines)
	{
		var sensorX = int.Parse(regex.Match(thisLine).Groups["sensorX"].Value);
		var sensorY = int.Parse(regex.Match(thisLine).Groups["sensorY"].Value);
		var beaconX = int.Parse(regex.Match(thisLine).Groups["beaconX"].Value);
		var beaconY = int.Parse(regex.Match(thisLine).Groups["beaconY"].Value);
		knownObjects.Add(new SensorAndBeacon(sensorX, sensorY, beaconX, beaconY));
	}	
	var foundBeaconX = 0;
	var foundBeaconY = 0;
	var isBeaconFound = false;
	for (int targetY = minY; targetY <= maxY; targetY++)
	{
		List<EmptyRange> targetYRanges = new ();
		foreach (SensorAndBeacon thisObject in knownObjects)
		{
			var thisRange = thisObject.GetEmptyRangeAtY(targetY);
			if (thisRange != null)
			{
				targetYRanges.Add(thisRange);
			}
		}
		HashSet<EmptyRange> temp = new ();
		var madeAnyMerges = true;
		var hasBeenMerged = new Dictionary<EmptyRange, bool>();
		while (madeAnyMerges && targetYRanges.Count > 1)
		{
			hasBeenMerged.Clear();
			targetYRanges.ForEach(x => hasBeenMerged[x] = false);
			temp.Clear();
			madeAnyMerges = false;
			for (int i = 0; i < targetYRanges.Count - 1; i++)
			{
				for (int ii = i + 1; ii < targetYRanges.Count; ii++)
				{
					var thisMerge = targetYRanges[i].Merge(targetYRanges[ii]);
					if (thisMerge.Count == 1)
					{
						madeAnyMerges = true;
						if (!thisMerge[0].Equals(targetYRanges[i]))
						{
							hasBeenMerged[targetYRanges[i]] = true;
						}
						if (!thisMerge[0].Equals(targetYRanges[ii]))
						{
							hasBeenMerged[targetYRanges[ii]] = true;
						}
					}
					thisMerge.ForEach(x => temp.Add(x));
				}
			}
			hasBeenMerged.Where(x => x.Value).ToList().ForEach(x => temp.Remove(x.Key));
			targetYRanges.Clear();
			targetYRanges.AddRange(temp);
		}
		foreach (EmptyRange thisRange in targetYRanges)
		{
			var test = thisRange.GetSpaceNotInRange(minX, maxX) ;
			if (test != null)
			{
				foundBeaconX = (int)test;
				foundBeaconY = targetY;
				isBeaconFound = true;
			}
		}
		if (isBeaconFound)
		{
			break;
		}
	}	
	var frequency = 4000000L * foundBeaconX + foundBeaconY;
	Console.WriteLine($"Part 2: {frequency}");
}
