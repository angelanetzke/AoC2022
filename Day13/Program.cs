using Day13;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

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

static void Part2(string[] allLines)
{
	var allItems = new List<Item>();
	var dividerPacket1 = new Item("[[2]]");
	var dividerPacket2 = new Item("[[6]]");
	allItems.Add(dividerPacket1);
	allItems.Add(dividerPacket2);
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length > 0)
		{
			allItems.Add(new Item(thisLine));
		}
	}
	allItems.Sort();
	var index1 = allItems.IndexOf(dividerPacket1) + 1;
	var index2 = allItems.IndexOf(dividerPacket2) + 1;
	var decoderKey = index1 * index2;
	Console.WriteLine($"Part 2: {decoderKey}");	
}
