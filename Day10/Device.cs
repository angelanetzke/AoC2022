using System.Text;

namespace Day10
{
	internal class Device
	{
		public static readonly int SCREEN_WIDTH = 40;
		private static readonly List<int> keyCycles = new List<int>() { 20, 60, 100, 140, 180, 220 };
		private readonly List<string> commandList;
		
		public Device(string[] commands)
		{
			commandList = new (commands);
		}

		public int GetSignalStrengthSum()
		{
			int x = 1;
		 	var commandQueue = new Queue<string>(commandList);
			var additionsQueue = new Queue<int>();
			int nextAdditionCycle = -1;
			int sum = 0;
			bool isExecutingCommand = false;
			int cycle = 1;
			while (commandQueue.Count > 0 || additionsQueue.Count > 0)
			{
				if (!isExecutingCommand && commandQueue.Count > 0)
				{
					var nextCommand = commandQueue.Dequeue();
					if (nextCommand.StartsWith("addx"))
					{
						additionsQueue.Enqueue(int.Parse(nextCommand.Split(' ')[1]));
						nextAdditionCycle = cycle + 1;
						isExecutingCommand = true;
					}					
				}
				if (keyCycles.Contains(cycle))
				{
					sum += cycle * x;
				}
				if (isExecutingCommand && additionsQueue.Count > 0 && cycle == nextAdditionCycle)
				{
					x += additionsQueue.Dequeue();
					isExecutingCommand = false;
				}
				cycle++;
			}
			return sum;
		}

		public string GetDisplay()
		{
			var builder = new StringBuilder();
			int x = 1;
		 	var commandQueue = new Queue<string>(commandList);
			var additionsQueue = new Queue<int>();
			int nextAdditionCycle = -1;
			bool isExecutingCommand = false;
			int cycle = 1;
			while (commandQueue.Count > 0 || additionsQueue.Count > 0)
			{
				if (!isExecutingCommand && commandQueue.Count > 0)
				{
					var nextCommand = commandQueue.Dequeue();
					if (nextCommand.StartsWith("addx"))
					{
						additionsQueue.Enqueue(int.Parse(nextCommand.Split(' ')[1]));
						nextAdditionCycle = cycle + 1;
						isExecutingCommand = true;
					}					
				}
				if (Math.Abs((cycle - 1) % SCREEN_WIDTH - x) <= 1)
				{
					builder.Append('#');
				}
				else
				{
					builder.Append('.');
				}
				if (isExecutingCommand && additionsQueue.Count > 0 && cycle == nextAdditionCycle)
				{
					x += additionsQueue.Dequeue();
					isExecutingCommand = false;
				}
				cycle++;
			}
			return builder.ToString();
		}

	}
}