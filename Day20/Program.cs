using Day20;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	NumberNode? zero = null;
	var nodeQueue = new Queue<NumberNode>();
	var index = 0;
	NumberNode? previous = null;
	foreach (string thisLine in allLines)
	{
		NumberNode newNode = new NumberNode(int.Parse(thisLine));
		nodeQueue.Enqueue(newNode);
		if (newNode.GetValue() == 0)
		{
			zero = newNode;
		}		
		if (previous != null)
		{
			previous.SetNext(newNode);
			newNode.SetPrevious(previous);
		}
		previous = newNode;
		index++;
	}
	if (previous != null)
	{
		previous.SetNext(nodeQueue.Peek());
		nodeQueue.Peek().SetPrevious(previous);
	}
	while (nodeQueue.Count > 0)
	{
		nodeQueue.Dequeue().Mix();
	}
	int first = zero.GetNthNext(1000);
	int second = zero.GetNthNext(2000);
	int third = zero.GetNthNext(3000);
	Console.WriteLine($"Part 1: {first + second + third}");
	
}