using Day21;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var entries = new Dictionary<string, string>();
	foreach (string thisLine in allLines)
	{
		entries[thisLine.Split(": ")[0]] = thisLine.Split(": ")[1];
	}
	var evaluatedEntriesCount = 0;
	var numberRegex = new Regex(@"^-?\d+$");
	var expressionRegex = new Regex(@"^-?\d+ [+\-*/]{1} -?\d+$");
	while (evaluatedEntriesCount < entries.Keys.Count)
	{
		evaluatedEntriesCount = 0;
		foreach (string thisKey in entries.Keys)
		{
			var thisValue = entries[thisKey];
			if (numberRegex.IsMatch(thisValue))
			{
				evaluatedEntriesCount++;
			}
			else
			{
				var firstSymbol = thisValue.Split(' ')[0];
				var secondSymbol = thisValue.Split(' ')[2];
				if (!numberRegex.IsMatch(firstSymbol) && numberRegex.IsMatch(entries[firstSymbol]))
				{
					entries[thisKey] = thisValue.Replace(firstSymbol, entries[firstSymbol]);					
				}
				if (!numberRegex.IsMatch(secondSymbol) && numberRegex.IsMatch(entries[secondSymbol]))
				{
					entries[thisKey] = thisValue.Replace(secondSymbol, entries[secondSymbol]);
				}
				if (expressionRegex.IsMatch(thisValue))
				{
					var firstValue = long.Parse(thisValue.Split(' ')[0]);
					var secondValue = long.Parse(thisValue.Split(' ')[2]);
					switch (thisValue.Split(' ')[1])
					{
						case "+":
							entries[thisKey] = (firstValue + secondValue).ToString();
							break;
						case "-":
							entries[thisKey] = (firstValue - secondValue).ToString();
							break;
						case "*":
							entries[thisKey] = (firstValue * secondValue).ToString();
							break;
						case "/":
							entries[thisKey] = (firstValue / secondValue).ToString();
							break;
					}
					evaluatedEntriesCount++;
				}
			}
		}
	}
	Console.WriteLine($"Part 1: {entries["root"]}");
}

static void Part2(string[] allLines)
{
	var theEquation = new Equation(allLines);
	Console.WriteLine($"Part 2: {theEquation.Solve()}");
}