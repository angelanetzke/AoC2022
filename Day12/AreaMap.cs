namespace Day12
{
	internal class AreaMap
	{
		private readonly Dictionary<(int, int), int> distances = new();
		private readonly List<Node> initialUnvisited = new ();
		private Node? start = null;
		
		public AreaMap(string[] data)
		{
			
			var end  = new Node(-1, -1, 'X');
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
					initialUnvisited.Add(newNode);
				}
			}
			var unvisited = new HashSet<Node>(initialUnvisited);
			var nodeQueue = new Queue<Node>();
			end.SetDistance(0);
			nodeQueue.Enqueue(end);
			Node current;
			do
			{
				current = nodeQueue.Dequeue();
				unvisited.Remove(current);
				distances[current.GetLocation()] = current.GetDistance();
				var neighbors = unvisited.Where(x => current.CanMoveTo(x)).ToList();
				neighbors.ForEach(x => x.SetDistance(Math.Min(x.GetDistance(), current.GetDistance() + 1)));
				neighbors.Where(x => !nodeQueue.Contains(x)).ToList().ForEach(x => nodeQueue.Enqueue(x));
			} while (nodeQueue.Count > 0);
		}

		public int GetDistanceToEnd()
		{
			if (start == null)
			{
				return -1;
			}
			else
			{
				return GetDistanceToEnd(start);
			}
		}

		private int GetDistanceToEnd(Node startPoint)
		{
			if (!distances.Keys.Contains(startPoint.GetLocation()))
			{
				return -1;
			}
			else
			{
				return distances[startPoint.GetLocation()];
			}
		}

		public int GetHikingPathDistance()
		{
			var shortest = int.MaxValue;
			var startPoints = initialUnvisited.Where(x => x.GetHeight() == 0).ToList();
			foreach(Node thisStartPoint in startPoints)
			{
				start = thisStartPoint;
				var thisDistance = GetDistanceToEnd(thisStartPoint);
				if (thisDistance > -1)
				{
					shortest = Math.Min(shortest, thisDistance);
				}
			}
			return shortest;
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

			public int GetHeight()
			{
				return height;
			}

			public (int, int) GetLocation()
			{
				return location;
			}

			public bool CanMoveTo(Node other)
			{
				if (location.Item1 == other.location.Item1 
					&& Math.Abs(location.Item2 - other.location.Item2) == 1
					&& other.height - height >= -1)
				{
					return true;
				}
				if (location.Item2 == other.location.Item2 
					&& Math.Abs(location.Item1 - other.location.Item1) == 1
					&& other.height - height >= -1)
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