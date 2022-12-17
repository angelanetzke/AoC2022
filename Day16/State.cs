using System.Text;

namespace Day16
{
	internal class State
	{
		private static Dictionary<string, string[]> connections = new ();
		private Dictionary<string, Valve> valveCollection = new ();
		private int timeLeft;
		private string nextMoveName;
		private static readonly string start = "AA";
		private bool isSecondTurnNeeded;
		private static int totalTime;
		private static Dictionary<(int, string, string, string, bool), int> cache = new ();
		private static Dictionary<string, int> secondTurnCache = new ();
		
		public State(int timeLeft, string nextMoveName, 
			Dictionary<string, Valve> oldValveCollection, bool isSecondTurnNeeded)
		{
			this.timeLeft = timeLeft;
			this.nextMoveName = nextMoveName;
			this.isSecondTurnNeeded = isSecondTurnNeeded;
			valveCollection = new ();
			foreach (string thisKey in oldValveCollection.Keys)
			{
				valveCollection[thisKey] = new Valve(oldValveCollection[thisKey]);
			}
		}

		public static void SetTotalTime(int newTime)
		{
			totalTime = newTime;
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
				int nextStatePressure;
				var currentOpenValves = GetOpenValveString();
				foreach (Valve thisValve in scoredList)
				{					
					if (cache.ContainsKey((timeLeft, nextMoveName, thisValve.GetName(), currentOpenValves, isSecondTurnNeeded)))
					{
						nextStatePressure = cache[(timeLeft, nextMoveName, thisValve.GetName(), currentOpenValves, isSecondTurnNeeded)];
					}
					else
					{
						var nextState = new State(timeLeft, thisValve.GetName(), valveCollection, isSecondTurnNeeded);
						nextStatePressure = nextState.GetPressureReleased();
						cache[(timeLeft, nextMoveName, thisValve.GetName(), currentOpenValves, isSecondTurnNeeded)] = nextStatePressure;
					}
					maxPressureReleased = Math.Max(maxPressureReleased, pressureReleased + nextStatePressure);					
				}
				if (isSecondTurnNeeded)
				{										
					if (secondTurnCache.ContainsKey(currentOpenValves))
					{
						nextStatePressure = secondTurnCache[currentOpenValves];
					}
					else
					{
						var nextState = new State(totalTime, start, valveCollection, false);
						nextStatePressure = nextState.GetPressureReleased();
						secondTurnCache[currentOpenValves] = nextStatePressure;
					}					
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

		private string GetOpenValveString()
		{
			var builder = new StringBuilder();
			var openValveList = valveCollection.Values.Where(x => x.IsOpen()).Select(x => x.GetName()).ToList();
			openValveList.Sort();
			openValveList.ForEach(x => builder.Append(x));
			return builder.ToString();
		}

		public static void SetConnections(Dictionary<string, string[]> newConnections)
		{
			connections = newConnections;
		}

	}
}