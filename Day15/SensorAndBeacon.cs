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
			int manhattanDistance = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
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

	}
}