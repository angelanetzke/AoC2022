var allText = File.ReadAllText("input.txt");
Part1(allText);

static void Part1(string allText)
{
	var markerStart = -1;
	const int MARKER_LENGTH = 4;
	for (int i = 0; i <= allText.Length - MARKER_LENGTH; i++)
	{
		if (AreLettersUnique(allText[i .. (i  + MARKER_LENGTH)]))
		{
			markerStart = i + MARKER_LENGTH;
			break;
		}
	}
	Console.WriteLine($"Part 1: {markerStart}");
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