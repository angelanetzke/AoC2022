namespace Day13
{
	internal class Item : IComparable<Item>
	{
		private readonly int value;
		private readonly List<Item> list = new ();
		private readonly bool isList;

		public Item(string data)
		{
			if (data == "[]") // Is empty list
			{
				isList = true;
			}
			else if (data[0] == '[') // Is a list
			{
				isList = true;
				data = data.Substring(1, data.Length - 2);				
				var outsideCommas = new List<int>();
				var openBrackets = 0;
				for (int i = 0; i < data.Length; i++)
				{
					if (data[i] == '[')
					{
						openBrackets++;
					}
					if (data[i] == ']')
					{
						openBrackets--;
					}
					if (openBrackets == 0 && data[i] == ',')
					{
						outsideCommas.Add(i);
					}
				}
				if (outsideCommas.Count == 0) // List is made up of single list
				{
					list.Add(new Item(data));
				}
				else // List is made up of some mix of lists and integers
				{
					int startIndex = 0;
					foreach (int thisIndex in outsideCommas)
					{
						list.Add(new Item(data[startIndex..thisIndex]));
						startIndex = thisIndex + 1;
					}
					list.Add(new Item(data[startIndex..]));
				}
			}
			else // Is a single integer
			{
				isList = false;
				value = int.Parse(data);
			}
		}

		public int CompareTo(Item? other)
		{
			if (other == null)
			{
				return -1;
			}
			else
			{
				if (!isList && !other.isList)
				{
					return value.CompareTo(other.value);
				}
				else if (isList && other.isList)
				{
					if (list.Count == 0 || other.list.Count == 0)
					{
						return list.Count.CompareTo(other.list.Count);
					}
					else
					{
						var currentIndex = 0;
						var maxIndex = Math.Min(list.Count - 1, other.list.Count - 1);
						while (currentIndex <= maxIndex)
						{
							var returnVal = list[currentIndex].CompareTo(other.list[currentIndex]);
							if (returnVal != 0)
							{
								return returnVal;
							}
							currentIndex++;
						}
						return list.Count.CompareTo(other.list.Count);
					}
				}
				else if (isList && !other.isList)
				{
					if (list.Count == 0)
					{
						return -1;
					}
					var returnVal = list[0].CompareTo(other);
					if (returnVal != 0)
					{
						return returnVal;
					}
					else
					{
						return list.Count == 1 ? 0 : list.Count > 1 ? 1 : -1;
					}
				}
				else
				{
					if (other.list.Count == 0)
					{
						return 1;
					}
					var returnVal = CompareTo(other.list[0]);
					if (returnVal != 0)
					{
						return returnVal;
					}
					else
					{
						return other.list.Count == 1 ? 0 : other.list.Count > 1 ? -1 : 1;
					}
				}				
			}
		}

		public override int GetHashCode()
		{
			if (!isList)
			{
				return value.GetHashCode();
			}
			else
			{
				if (list.Count == 0)
				{
					return 17;
				}
				else
				{
					int hashCode = 17;
					list.ForEach(x => hashCode = (hashCode + x.GetHashCode()).GetHashCode());
					return hashCode;
				}
			}
		}

		public override bool Equals(object? obj)
		{
			if (obj is Item other)
			{
				return CompareTo(other) == 0;
			}
			else
			{
				return false;
			}
		}

	}
}