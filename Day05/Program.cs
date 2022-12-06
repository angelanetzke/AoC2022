using System.Text;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var separationLine = 0;
	var separationRegex = new Regex(@"^[\s\d]+$");
	while (!separationRegex.IsMatch(allLines[separationLine]))
	{
		separationLine++;
	}
	var stacks = new Stack<char>[(allLines[separationLine].Length + 1) / 4];
	for (int i = 0; i < stacks.Count(); i++)
	{
		stacks[i] = new Stack<char>();
	}
	for (int lineIndex = separationLine - 1; lineIndex >= 0; lineIndex--)
	{
		for (int stackIndex = 0; stackIndex < stacks.Count(); stackIndex++)
		{
			var stringIndex = stackIndex * 4 + 1;
			if (allLines[lineIndex][stringIndex] != ' ')
			{
				stacks[stackIndex].Push(allLines[lineIndex][stringIndex]);
			}
		}
	}
	var instructionRegex = new Regex(@"move (?<count>\d+) from (?<start>\d+) to (?<end>\d+)");
	for (int lineIndex = separationLine + 2; lineIndex < allLines.Count(); lineIndex++)
	{
		var match = instructionRegex.Match(allLines[lineIndex]);
		var count = int.Parse(match.Groups["count"].Value);
		var start = int.Parse(match.Groups["start"].Value);
		var end = int.Parse(match.Groups["end"].Value);
		for (int i = 0; i < count; i++)
		{
			stacks[end - 1].Push(stacks[start - 1].Pop());
		}
	}
	var builder = new StringBuilder();
	foreach (Stack<char> thisStack in stacks)
	{
		builder.Append(thisStack.Peek());
	}
	Console.WriteLine($"Part 1: {builder.ToString()}");
}