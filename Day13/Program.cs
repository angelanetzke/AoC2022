using Day13;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	Item? item1 = null;
	Item? item2 = null;
	var thisPairIndex = 1;
	var indices = new List<int>();
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length == 0)
		{
			if (item1.CompareTo(item2) < 0)
			{
				indices.Add(thisPairIndex);
			}
			item1 = null;
			item2 = null;
			thisPairIndex++;
		}
		else if (item1 == null)
		{
			item1 = new Item(thisLine);
		}
		else
		{
			item2 = new Item(thisLine);
		}
	}
	if (item1 != null && item2 != null)
	{
		if (item1.CompareTo(item2) < 0)
		{
			indices.Add(thisPairIndex);
		}
	}
	Console.WriteLine($"Part 1: {indices.Sum()}");
}
