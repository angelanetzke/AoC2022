namespace Day15
{
	internal class SensorAndBeacon
	{
		private readonly int sensorX;
		private readonly int sensorY;
		private readonly int beaconX;
		private readonly int beaconY;

		public SensorAndBeacon(int sensorX, int sensorY, int beaconX, int beaconY)
		{
			this.sensorX = sensorX;
			this.sensorY = sensorY;
			this.beaconX = beaconX;
			this.beaconY = beaconY;
		}

		public (int, int) GetSensorLocation()
		{
			return (sensorX, sensorY);
		}

		public (int, int) GetBeaconLocation()
		{
			return (beaconX, beaconY);
		}

		public EmptyRange? GetEmptyRangeAtY(int targetY)
		{
			int manhattanDistance = GetManhattanDistance();
			var distanceToTargetY = Math.Abs(sensorY - targetY);
			if (distanceToTargetY <= manhattanDistance)
			{
				int minXAtTargetY = sensorX - (manhattanDistance - distanceToTargetY);
				int maxXAtTargetY = sensorX + (manhattanDistance - distanceToTargetY);
				return new EmptyRange(minXAtTargetY, maxXAtTargetY);
			}
			else
			{
				return null;
			}
		}

		private int GetManhattanDistance()
		{
			return Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
		}

		public List<Equation> GetEquations()
		{
			int manhattanDistance = GetManhattanDistance();
			return new List<Equation>()
			{
				new Equation(true, true, manhattanDistance + 1 + sensorX + sensorY),
				new Equation(true, false, manhattanDistance + 1 + sensorX - sensorY),
				new Equation(false, true, manhattanDistance + 1 - sensorX + sensorY),
				new Equation(false, false, manhattanDistance + 1 - sensorX - sensorY)
			};
		}

		public List<(int, int)> GetItersections(SensorAndBeacon other)
		{
			var result = new List<(int, int)>();
			var theseEquations = GetEquations();
			var otherEquations = other.GetEquations();
			for (int i = 0; i < theseEquations.Count; i++)
			{
				for (int ii = 0; ii < otherEquations .Count; ii++)
				{
					var answer = theseEquations[i].GetIntersection(otherEquations[ii]);
					if (answer != null)
					{
						result.Add(((int, int))answer);
					}
				}
			}
			return result;
		}

		public bool IsInRange((int, int) point)
		{
			var distanceToPoint = Math.Abs(point.Item1 - sensorX) + Math.Abs(point.Item2 - sensorY);
			return distanceToPoint <= GetManhattanDistance();
		}

	}
}