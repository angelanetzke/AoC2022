using System.Text;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
var separationLine = 0;
while (allLines[separationLine].Length > 0)
{
	separationLine++;
}
Part1(allLines, separationLine);
Part2(allLines, separationLine);

static void Part1(string[] allLines, int separationLine)
{	
	var stacks = GetInitialStacks(separationLine, allLines);
	var instructionRegex = new Regex(@"move (?<count>\d+) from (?<start>\d+) to (?<end>\d+)");
	for (int lineIndex = separationLine + 1; lineIndex < allLines.Count(); lineIndex++)
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

static void Part2(string[] allLines, int separationLine)
{
	var moveList = new LinkedList<char>();
	var stacks = GetInitialStacks(separationLine, allLines);
	var instructionRegex = new Regex(@"move (?<count>\d+) from (?<start>\d+) to (?<end>\d+)");
	for (int lineIndex = separationLine + 1; lineIndex < allLines.Count(); lineIndex++)
	{
		var match = instructionRegex.Match(allLines[lineIndex]);
		var count = int.Parse(match.Groups["count"].Value);
		var start = int.Parse(match.Groups["start"].Value);
		var end = int.Parse(match.Groups["end"].Value);
		for (int i = 0; i < count; i++)
		{
			moveList.AddLast(stacks[start - 1].Pop());
		}
		for (int i = 0; i < count; i++)
		{
			if (moveList.Last != null)
			{
				stacks[end - 1].Push(moveList.Last.Value);
				moveList.RemoveLast();
			}
		}		
	}
	var builder = new StringBuilder();
	foreach (Stack<char> thisStack in stacks)
	{
		builder.Append(thisStack.Peek());
	}
	Console.WriteLine($"Part 2: {builder.ToString()}");
}

static Stack<char>[] GetInitialStacks(int separationLine, string[] allLines)
{
	var stacks = new Stack<char>[(allLines[0].Length + 1) / 4];
	for (int i = 0; i < stacks.Count(); i++)
	{
		stacks[i] = new Stack<char>();
	}
	for (int lineIndex = separationLine - 2; lineIndex >= 0; lineIndex--)
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
	return stacks;
}