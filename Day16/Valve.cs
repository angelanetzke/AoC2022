namespace Day16
{
	internal class Valve
	{
		private readonly string name;
		private readonly int flowRate;
		private int distance;
		private int timeLeft;
		private bool isOpen;

		public Valve(string name, int flowRate)
		{
			this.name = name;
			this.flowRate = flowRate;
			isOpen = false;
		}

		public Valve (Valve original)
		{
			name = original.name;
			flowRate = original.flowRate;
			distance = original.distance;
			timeLeft = original.timeLeft;
			isOpen = original.isOpen;
		}

		public string GetName()
		{
			return name;
		}

		public int GetFlowRate()
		{
			return flowRate;
		}

		public void SetDistance(int newDistance)
		{
			distance = newDistance;
		}

		public int GetDistance()
		{
			return distance;
		}

		public void SetTimeLeft(int newTimeLeft)
		{
			timeLeft = newTimeLeft;
		}

		public int GetTimeLeft()
		{
			return timeLeft;
		}

		public int GetScore()
		{
			if (timeLeft < 1)
			{
				return -1;
			}
			else
			{
				return GetPressureReleased() - distance;
			}
		}

		public int GetPressureReleased()
		{
			return flowRate * (timeLeft - 1);
		}

		public void OpenValve()
		{
			isOpen = true;
		}

		public bool IsOpen()
		{
			return isOpen;
		}

	}
}