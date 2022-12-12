namespace Day12
{
	internal class AreaMap
	{
		private readonly Node start;
		private readonly Node end;
		private readonly HashSet<Node> unvisited = new ();
		public AreaMap(string[] data)
		{
			for (int row = 0; row < data.Length; row++)
			{
				for (int column = 0; column < data[row].Length; column++)
				{
					var newNode = new Node(column, row, data[row][column]);
					if (data[row][column] == 'S')
					{
						start = newNode;
					}
					if (data[row][column] == 'E')
					{
						end = newNode;
					}
					unvisited.Add(newNode);
				}
			}
		}

		public int GetDistanceToEnd()
		{
			var nodeQueue = new Queue<Node>();
			start.SetDistance(0);
			nodeQueue.Enqueue(start);
			Node current;
			do
			{
				current = nodeQueue.Dequeue();
				unvisited.Remove(current);
				var neighbors = unvisited.Where(x => current.CanMoveTo(x)).ToList();
				neighbors.ForEach(x => x.SetDistance(Math.Min(x.GetDistance(), current.GetDistance() + 1)));
				if (neighbors.Contains(end))
				{
					return end.GetDistance();
				}
				neighbors.Where(x => !nodeQueue.Contains(x)).ToList().ForEach(x => nodeQueue.Enqueue(x));
			} while (nodeQueue.Count > 0);
			return -1;
		}

		private class Node
		{
			private readonly int height;
			private int distance = int.MaxValue;
			private readonly (int, int) location;

			public Node(int x, int y, char value)
			{
				location = (x, y);
				if (value == 'S')
				{
					height = 0;
				}
				else if (value == 'E')
				{
					height = 25;
				}
				else
				{
					height = (int)(value - 'a');
				}
			}

			public int GetDistance()
			{
				return distance;
			}

			public void SetDistance(int newDistance)
			{
				distance = newDistance;
			}

			public bool CanMoveTo(Node other)
			{
				if (location.Item1 == other.location.Item1 
					&& Math.Abs(location.Item2 - other.location.Item2) == 1
					&& other.height - height <= 1)
				{
					return true;
				}
				if (location.Item2 == other.location.Item2 
					&& Math.Abs(location.Item1 - other.location.Item1) == 1
					&& other.height - height <= 1)
				{
					return true;
				}
				return false;
			}

			public override bool Equals(object? obj)
			{
				if (obj is Node other)
				{
					return location == other.location;
				}
				else
				{
					return false;
				}
			}

			public override int GetHashCode()
			{
				return location.GetHashCode();
			}
		}
	}
}