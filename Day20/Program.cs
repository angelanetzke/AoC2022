using Day20;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	NumberNode? zero = null;
	var nodeQueue = new Queue<NumberNode>();
	NumberNode? previous = null;
	foreach (string thisLine in allLines)
	{
		NumberNode newNode = new NumberNode(long.Parse(thisLine));
		nodeQueue.Enqueue(newNode);
		if (newNode.GetValue() == 0L)
		{
			zero = newNode;
		}		
		if (previous != null)
		{
			previous.SetNext(newNode);
			newNode.SetPrevious(previous);
		}
		previous = newNode;
	}
	if (previous != null)
	{
		previous.SetNext(nodeQueue.Peek());
		nodeQueue.Peek().SetPrevious(previous);
	}
	NumberNode.nodeCount = nodeQueue.Count;
	while (nodeQueue.Count > 0)
	{
		nodeQueue.Dequeue().Mix();
	}
	var first = zero.GetNthNext(1000);
	var second = zero.GetNthNext(2000);
	var third = zero.GetNthNext(3000);
	Console.WriteLine($"Part 1: {first + second + third}");	
}

static void Part2(string[] allLines)
{
	var decryptionKey = 811589153L;
	NumberNode? zero = null;
	var nodeQueue = new Queue<NumberNode>();
	NumberNode? previous = null;
	foreach (string thisLine in allLines)
	{
		NumberNode newNode = new NumberNode(long.Parse(thisLine) * decryptionKey);
		nodeQueue.Enqueue(newNode);
		if (newNode.GetValue() == 0L)
		{
			zero = newNode;
		}		
		if (previous != null)
		{
			previous.SetNext(newNode);
			newNode.SetPrevious(previous);
		}
		previous = newNode;
	}
	if (previous != null)
	{
		previous.SetNext(nodeQueue.Peek());
		nodeQueue.Peek().SetPrevious(previous);
	}
	NumberNode.nodeCount = nodeQueue.Count;
	var mixRound = 1;
	var nextRoundQueue = new Queue<NumberNode>();
	while (mixRound <= 10)
	{
		var nextNode = nodeQueue.Dequeue();
		nextRoundQueue.Enqueue(nextNode);
		nextNode.Mix();		
		if (nodeQueue.Count == 0)
		{
			nodeQueue = nextRoundQueue;
			nextRoundQueue = new ();
			mixRound++;
		}
	}
	var first = zero.GetNthNext(1000);
	var second = zero.GetNthNext(2000);
	var third = zero.GetNthNext(3000);
	Console.WriteLine($"Part 2: {first + second + third}");	
}