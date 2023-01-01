namespace Day19
{
	internal class Blueprint
	{
		private readonly int ID;
		private readonly int oreRobotOreCost;
		private readonly int clayRobotOreCost;
		private readonly int obsidianRobotOreCost;
		private readonly int obsidianRobotClayCost;
		private readonly int geodeRobotOreCost;
		private readonly int geodeRobotObsidianCost;
		private readonly int maxOreNeeded;
		private readonly int maxClayNeeded;
		private readonly int maxObsidianNeeded;
		private static int totalTime = 0;

		public Blueprint(int ID, int oreRobotOreCost, int clayRobotOreCost, int obsidianRobotOreCost,
			int obsidianRobotClayCost, int geodeRobotOreCost, int geodeRobotObsidianCost)
		{
			this.ID = ID;
			this.oreRobotOreCost = oreRobotOreCost;
			this.clayRobotOreCost = clayRobotOreCost;
			this.obsidianRobotOreCost = obsidianRobotOreCost;
			this.obsidianRobotClayCost = obsidianRobotClayCost;
			this.geodeRobotOreCost = geodeRobotOreCost;
			this.geodeRobotObsidianCost = geodeRobotObsidianCost;
			maxOreNeeded = (new List<int>() 
				{oreRobotOreCost, clayRobotOreCost, obsidianRobotOreCost, geodeRobotOreCost}).Max();
			maxClayNeeded = obsidianRobotClayCost;
			maxObsidianNeeded = geodeRobotObsidianCost;
		}

		public int GetID()
		{
			return ID;
		}

		public static void SetTotalTime(int newTime)
		{
			totalTime = newTime;
		}

		public int GetMaxGeodes()
		{
			State.ClearCache();
			var startState = new State(this, (totalTime, 1, 0, 0, 0, 0, 0, 0, 0));
			return startState.GetMaxGeodes();
		}

		private class State
		{
			private readonly Blueprint blueprint;
			private readonly int timeLeft;
			private readonly int oreRobotCount;
			private readonly int clayRobotCount;
			private readonly int obsidianRobotCount;
			private readonly int geodeRobotCount;
			private readonly int oreCount;
			private readonly int clayCount;
			private readonly int obsidianCount;
			private readonly int geodeCount;
			private static Dictionary<(int, int, int, int, int, int, int, int, int), int> cache = new ();
			private static int bestSoFar = int.MinValue;

			public State(Blueprint blueprint, (int, int, int, int, int, int, int, int, int) values)
			{
				this.blueprint = blueprint;
				this.timeLeft = values.Item1;
				this.oreRobotCount = values.Item2;
				this.clayRobotCount = values.Item3;
				this.obsidianRobotCount = values.Item4;
				this.geodeRobotCount = values.Item5;
				this.oreCount = values.Item6;
				this.clayCount = values.Item7;
				this.obsidianCount = values.Item8;
				this.geodeCount = values.Item9;
			}

			public int GetMaxGeodes()
			{
				if (timeLeft == 1)
				{
					var thisResult = geodeCount + geodeRobotCount;
					bestSoFar = Math.Max(bestSoFar, thisResult);
					return thisResult;
				}
				var bestPossible = geodeCount + geodeRobotCount * timeLeft + (timeLeft - 1) * timeLeft / 2;
				if (bestPossible < bestSoFar)
				{
					return int.MinValue;
				}
				var maxGeodes = int.MinValue;	
				if (oreRobotCount < blueprint.maxOreNeeded && oreCount >= blueprint.oreRobotOreCost)
				{
					maxGeodes = Math.Max(maxGeodes, MakeOreRobotNow());
				}
				else if (oreRobotCount < blueprint.maxOreNeeded)
				{
					maxGeodes = Math.Max(maxGeodes, FastForwardToNextOreRobot());
				}
				if (clayRobotCount < blueprint.maxClayNeeded && oreCount >= blueprint.clayRobotOreCost)
				{
					maxGeodes = Math.Max(maxGeodes, MakeClayRobotNow());
				}
				else if (clayRobotCount < blueprint.maxClayNeeded)
				{
					maxGeodes = Math.Max(maxGeodes, FastForwardToNextClayRobot());
				}
				if (obsidianRobotCount < blueprint.maxObsidianNeeded
					&& oreCount >= blueprint.obsidianRobotOreCost 
					&& clayCount >= blueprint.obsidianRobotClayCost)
				{
					maxGeodes = Math.Max(maxGeodes, MakeObsidianRobotNow());
				}
				else if (obsidianRobotCount < blueprint.maxObsidianNeeded && clayRobotCount > 0)
				{
					maxGeodes = Math.Max(maxGeodes, FastForwardToNextObsidianRobot());
				}
				if (oreCount >= blueprint.geodeRobotOreCost 
					&& obsidianCount >= blueprint.geodeRobotObsidianCost
					&& obsidianRobotCount > 0)
				{
					maxGeodes = Math.Max(maxGeodes, MakeGeodeRobotNow());
				}
				else if (obsidianRobotCount > 0)
				{
					maxGeodes = Math.Max(maxGeodes, FastForwardToNextGeodeRobot());
				}
				maxGeodes = Math.Max(maxGeodes, WaitUntilEnd());
				return maxGeodes;
			}

			public static void ClearCache()
			{
				cache = new ();
				bestSoFar = int.MinValue;
			}

			private int MakeOreRobotNow()
			{
				var nextValues = 
					(
						timeLeft - 1,
						oreRobotCount + 1,
						clayRobotCount,
						obsidianRobotCount,
						geodeRobotCount,
						oreCount + oreRobotCount - blueprint.oreRobotOreCost,
						clayCount + clayRobotCount,
						obsidianCount + obsidianRobotCount,
						geodeCount + geodeRobotCount
					);
				if (cache.ContainsKey(nextValues))
				{
					return cache[nextValues];
				}
				else
				{
					var nextState = new State(blueprint, nextValues);
					var maxGeodes = nextState.GetMaxGeodes();
					cache[nextValues] = maxGeodes;
					return maxGeodes;
				}
			}

			private int FastForwardToNextOreRobot()
			{
				int timeForOre;
				if ((blueprint.oreRobotOreCost - oreCount) % oreRobotCount == 0)
				{
					timeForOre = (blueprint.oreRobotOreCost - oreCount) / oreRobotCount; 
				}
				else
				{
					timeForOre = (blueprint.oreRobotOreCost - oreCount) / oreRobotCount + 1; 
				}
				var timeToNextOreRobot = timeForOre;
				if (timeLeft - timeToNextOreRobot >= 4)
				{
					var nextValues = 
						(
							timeLeft - timeToNextOreRobot,
							oreRobotCount,
							clayRobotCount,
							obsidianRobotCount,
							geodeRobotCount,
							oreCount + oreRobotCount * timeToNextOreRobot,
							clayCount + clayRobotCount * timeToNextOreRobot,
							obsidianCount + obsidianRobotCount * timeToNextOreRobot,
							geodeCount + geodeRobotCount * timeToNextOreRobot
						);
					if (cache.ContainsKey(nextValues))
					{
						return cache[nextValues];
					}
					else
					{
						var nextState = new State(blueprint, nextValues);
						var maxGeodes = nextState.GetMaxGeodes();
						cache[nextValues] = maxGeodes;
						return maxGeodes;
					}
				}
				else
				{
					return int.MinValue;
				}
			}

			private int MakeClayRobotNow()
			{
				var nextValues = 
					(
						timeLeft - 1,
						oreRobotCount,
						clayRobotCount + 1,
						obsidianRobotCount,
						geodeRobotCount,
						oreCount + oreRobotCount - blueprint.clayRobotOreCost,
						clayCount + clayRobotCount,
						obsidianCount + obsidianRobotCount,
						geodeCount + geodeRobotCount
					);
				if (cache.ContainsKey(nextValues))
				{
					return cache[nextValues];
				}
				else
				{
					var nextState = new State(blueprint, nextValues);
					var maxGeodes = nextState.GetMaxGeodes();
					cache[nextValues] = maxGeodes;
					return maxGeodes;
				}
			}

			private int FastForwardToNextClayRobot()
			{
				int timeForOre;
				if ((blueprint.clayRobotOreCost - oreCount) % oreRobotCount == 0)
				{
					timeForOre = (blueprint.clayRobotOreCost - oreCount) / oreRobotCount; 
				}
				else
				{
					timeForOre = (blueprint.clayRobotOreCost - oreCount) / oreRobotCount + 1; 
				}
				var timeToNextClayRobot = timeForOre;
				if (timeLeft - timeToNextClayRobot >= 6)
				{
					var nextValues = 
						(
							timeLeft - timeToNextClayRobot,
							oreRobotCount,
							clayRobotCount,
							obsidianRobotCount,
							geodeRobotCount,
							oreCount + oreRobotCount * timeToNextClayRobot,
							clayCount + clayRobotCount * timeToNextClayRobot,
							obsidianCount + obsidianRobotCount * timeToNextClayRobot,
							geodeCount + geodeRobotCount * timeToNextClayRobot
						);
					if (cache.ContainsKey(nextValues))
					{
						return cache[nextValues];
					}
					else
					{
						var nextState = new State(blueprint, nextValues);
						var maxGeodes = nextState.GetMaxGeodes();
						cache[nextValues] = maxGeodes;
						return maxGeodes;
					}
				}
				else
				{
					return int.MinValue;
				}
			}

			private int MakeObsidianRobotNow()
			{
				var nextValues = 
					(
						timeLeft - 1,
						oreRobotCount,
						clayRobotCount,
						obsidianRobotCount + 1,
						geodeRobotCount,
						oreCount + oreRobotCount- blueprint.obsidianRobotOreCost,
						clayCount + clayRobotCount - blueprint.obsidianRobotClayCost,
						obsidianCount + obsidianRobotCount,
						geodeCount + geodeRobotCount
					);
				if (cache.ContainsKey(nextValues))
				{
					return cache[nextValues];
				}
				else
				{
					var nextState = new State(blueprint, nextValues);
					var maxGeodes = nextState.GetMaxGeodes();
					cache[nextValues] = maxGeodes;
					return maxGeodes;
				}
			}

			private int FastForwardToNextObsidianRobot()
			{
				int timeForOre;
				if ((blueprint.obsidianRobotOreCost - oreCount) % oreRobotCount == 0)
				{
					timeForOre = (blueprint.obsidianRobotOreCost - oreCount) / oreRobotCount; 
				}
				else
				{
					timeForOre = (blueprint.obsidianRobotOreCost - oreCount) / oreRobotCount + 1; 
				}
				int timeForClay;
				if ((blueprint.obsidianRobotClayCost - clayCount) % clayRobotCount == 0)
				{
					timeForClay = (blueprint.obsidianRobotClayCost - clayCount) / clayRobotCount; 
				}
				else
				{
					timeForClay = (blueprint.obsidianRobotClayCost - clayCount) / clayRobotCount + 1; 
				}
				var timeToNextObsidianRobot = Math.Max(timeForOre, timeForClay);
				if (timeLeft - timeToNextObsidianRobot >= 4)
				{
					var nextValues = 
						(
							timeLeft - timeToNextObsidianRobot,
							oreRobotCount,
							clayRobotCount,
							obsidianRobotCount,
							geodeRobotCount,
							oreCount + oreRobotCount * timeToNextObsidianRobot,
							clayCount + clayRobotCount * timeToNextObsidianRobot,
							obsidianCount + obsidianRobotCount * timeToNextObsidianRobot,
							geodeCount + geodeRobotCount * timeToNextObsidianRobot
						);
					if (cache.ContainsKey(nextValues))
					{
						return cache[nextValues];
					}
					else
					{
						var nextState = new State(blueprint, nextValues);
						var maxGeodes = nextState.GetMaxGeodes();
						cache[nextValues] = maxGeodes;
						return maxGeodes;
					}
				}
				else
				{
					return int.MinValue;
				}
			}

			private int MakeGeodeRobotNow()
			{
				var nextValues = 
					(
						timeLeft - 1,
						oreRobotCount,
						clayRobotCount,
						obsidianRobotCount,
						geodeRobotCount + 1,
						oreCount + oreRobotCount - blueprint.geodeRobotOreCost,
						clayCount + clayRobotCount,
						obsidianCount + obsidianRobotCount - blueprint.geodeRobotObsidianCost,
						geodeCount + geodeRobotCount
					);
				if (cache.ContainsKey(nextValues))
				{
					return cache[nextValues];
				}
				else
				{
					var nextState = new State(blueprint, nextValues);
					var maxGeodes = nextState.GetMaxGeodes();
					cache[nextValues] = maxGeodes;
					return maxGeodes;
				}
			}

			private int FastForwardToNextGeodeRobot()
			{
				int timeForOre;
				if ((blueprint.geodeRobotOreCost - oreCount) % oreRobotCount == 0)
				{
					timeForOre = (blueprint.geodeRobotOreCost - oreCount) / oreRobotCount; 
				}
				else
				{
					timeForOre = (blueprint.geodeRobotOreCost - oreCount) / oreRobotCount + 1; 
				}
				int timeForObsidian;
				if ((blueprint.geodeRobotObsidianCost - obsidianCount) % obsidianRobotCount == 0)
				{
					timeForObsidian = (blueprint.geodeRobotObsidianCost - obsidianCount) / obsidianRobotCount; 
				}
				else
				{
					timeForObsidian = (blueprint.geodeRobotObsidianCost - obsidianCount) / obsidianRobotCount + 1; 
				}
				var timeToNextGeodeRobot = Math.Max(timeForOre, timeForObsidian);
				if (timeLeft - timeToNextGeodeRobot >= 2)
				{
					var nextValues = 
						(
							timeLeft - timeToNextGeodeRobot,
							oreRobotCount,
							clayRobotCount,
							obsidianRobotCount,
							geodeRobotCount,
							oreCount + oreRobotCount * timeToNextGeodeRobot,
							clayCount + clayRobotCount * timeToNextGeodeRobot,
							obsidianCount + obsidianRobotCount * timeToNextGeodeRobot,
							geodeCount + geodeRobotCount * timeToNextGeodeRobot
						);
					if (cache.ContainsKey(nextValues))
					{
						return cache[nextValues];
					}
					else
					{
						var nextState = new State(blueprint, nextValues);
						var maxGeodes = nextState.GetMaxGeodes();
						cache[nextValues] = maxGeodes;
						return maxGeodes;
					}
				}
				else
				{
					return int.MinValue;
				}
			}

			private int WaitUntilEnd()
			{
				var timeToWait = timeLeft - 2;
				var nextValues = 
					(
						1,
						oreRobotCount,
						clayRobotCount,
						obsidianRobotCount,
						geodeRobotCount,
						oreCount + oreRobotCount * (timeToWait + 1),
						clayCount + clayRobotCount * (timeToWait + 1),
						obsidianCount + obsidianRobotCount * (timeToWait + 1),
						geodeCount + geodeRobotCount * (timeToWait + 1)
					);
				if (cache.ContainsKey(nextValues))
				{
					return cache[nextValues];
				}
				else
				{
					var nextState = new State(blueprint, nextValues);
					var maxGeodes = nextState.GetMaxGeodes();
					cache[nextValues] = maxGeodes;
					return maxGeodes;
				}
			}

		}
	}
}