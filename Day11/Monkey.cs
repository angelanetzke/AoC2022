namespace Day11
{
	internal class Monkey
	{
		private readonly Queue<long> items;
		private readonly string operand1;
		private readonly string operand2;
		private readonly string operation;
		private readonly int divisor;
		private readonly int trueRecipient;
		private readonly int falseRecipient;
		private readonly List<Monkey> group;
		private long inspectionCount = 0;
		private long reductionFactor = 0;

		public Monkey(List<Monkey> group, List<string> data)
		{
			this.group = group;
			var itemString = data[1][18..];
			items = new (itemString.Split(", ").Select(x => long.Parse(x)));
			var operationString = data[2][19..];
			operand1 = operationString.Split(' ')[0];
			operation = operationString.Split(' ')[1];
			operand2 = operationString.Split(' ')[2];
			divisor = int.Parse(data[3][21..]);
			trueRecipient = int.Parse(data[4][29..]);
			falseRecipient = int.Parse(data[5][30..]);
		}

		public void TakeTurn()
		{
			var op1 = 0L;
			var op2 = 0L;
			while (items.Count > 0)
			{
				inspectionCount++;
				long worryLevel = items.Dequeue();
				if (operand1 == "old")
				{
					op1 = worryLevel;
				}
				else
				{
					op1 = long.Parse(operand1);
				}
				if (operand2 == "old")
				{
					op2 = worryLevel;
				}
				else
				{
					op2 = long.Parse(operand2);
				}
				if (operation == "*")
				{
					worryLevel = op1 * op2;
				}
				else
				{
					worryLevel = op1 + op2;
				}
				if (reductionFactor > 0)
				{
					worryLevel %= reductionFactor;
				}
				else
				{
					worryLevel /= 3;
				}
				if (worryLevel % divisor == 0)
				{
					group[trueRecipient].AddItem(worryLevel);
				}
				else
				{
					group[falseRecipient].AddItem(worryLevel);
				}
			}
		}

		public void AddItem(long newItem)
		{
			items.Enqueue(newItem);
		}

		public long GetInspectionCount()
		{
			return inspectionCount;
		}

		public int GetDivisor()
		{
			return divisor;
		}

		public void SetReductionFactor(long newFactor)
		{
			reductionFactor = newFactor;
		}

	}
}