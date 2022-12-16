namespace Day16
{
	internal class State
	{
		private static Dictionary<string, string[]> connections = new ();
		private Dictionary<string, Valve> valveCollection = new ();
		private int timeLeft;
		private string nextMoveName;
		private static readonly string start = "AA";
		
		public State(int timeLeft, string nextMoveName, Dictionary<string, Valve> oldValveCollection)
		{
			this.timeLeft = timeLeft;
			this.nextMoveName = nextMoveName;
			valveCollection = new ();
			foreach (string thisKey in oldValveCollection.Keys)
			{
				valveCollection[thisKey] = new Valve(oldValveCollection[thisKey]);
			}
		}

		public int GetPressureReleased()
		{	
			int pressureReleased;
			if (nextMoveName == start)
			{
				pressureReleased = 0;
			}
			else
			{
				var nextMove = valveCollection[nextMoveName];
				pressureReleased = nextMove.GetPressureReleased();
				nextMove.OpenValve();
				timeLeft = nextMove.GetTimeLeft() - 1;
			}
			var scoredList = GetScores();
			scoredList = scoredList.Where(x => x.GetScore() > 0 && !x.IsOpen()).ToList();			
			if (scoredList.Count == 0)
			{
				return pressureReleased;
			}
			else
			{
				int maxPressureReleased = 0;
				foreach (Valve thisValve in scoredList)
				{
					var nextState = new State(timeLeft, thisValve.GetName(), valveCollection);
					int nextStatePressure = nextState.GetPressureReleased();
					maxPressureReleased = Math.Max(maxPressureReleased, pressureReleased + nextStatePressure);	
				}
				return maxPressureReleased;
			}
		}

		private List<Valve> GetScores()
		{
			var scoredValves = new List<Valve>();
			var visited = new HashSet<Valve>();
			var current = valveCollection[nextMoveName];
			var valveQueue = new Queue<Valve>();
			valveQueue.Enqueue(current);
			foreach (Valve thisValve in valveCollection.Values)
			{
				thisValve.SetDistance(int.MaxValue);
			}
			current.SetDistance(0);
			current.SetTimeLeft(timeLeft);
			while (valveQueue.Count > 0)
			{
				current = valveQueue.Dequeue();
				visited.Add(current);
				scoredValves.Add(current);
				var neighborNames = connections[current.GetName()];
				foreach (string thisNeighborName in neighborNames)
				{
					if (!visited.Contains(valveCollection[thisNeighborName]))
					{
						var thisNeighborDistance = valveCollection[thisNeighborName].GetDistance();
						if (current.GetDistance() + 1 < thisNeighborDistance)
						{
							valveCollection[thisNeighborName].SetDistance(current.GetDistance() + 1);
							valveCollection[thisNeighborName].SetTimeLeft(current.GetTimeLeft() - 1);
						}
						valveQueue.Enqueue(valveCollection[thisNeighborName]);
					}
				}		
			}
			return scoredValves;
		}
		public static void SetConnections(Dictionary<string, string[]> newConnections)
		{
			connections = newConnections;
		}

	}
}