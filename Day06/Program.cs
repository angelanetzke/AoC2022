var allText = File.ReadAllText("input.txt");
Part1(allText);
Part2(allText);

static void Part1(string allText)
{
	var markerStart = FindMarker(allText, 4);
	Console.WriteLine($"Part 1: {markerStart}");
}

static void Part2(string allText)
{
	var markerStart = FindMarker(allText, 14);
	Console.WriteLine($"Part 2: {markerStart}");
}

static int FindMarker(string allText, int markerLength)
{
	var markerStart = -1;
	for (int i = 0; i <= allText.Length - markerLength; i++)
	{
		if (AreLettersUnique(allText[i .. (i  + markerLength)]))
		{
			markerStart = i + markerLength;
			break;
		}
	}
	return markerStart;
}

static bool AreLettersUnique(string theString)
{
	for (int i = 0; i <= theString.Length - 2; i++)
	{
		if (theString.LastIndexOf(theString[i]) != i)
		{
			return false;
		}
	}
	return true;
}