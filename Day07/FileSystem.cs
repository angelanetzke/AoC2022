namespace Day07
{
	internal class FileSystem
	{
		private Directory root = new ("/", null);
		private Directory? current;

		private string[] allLines;

		private int lineIndex;
		public FileSystem(string[] allLines)
		{
			this.allLines = allLines;
			current = root;
			lineIndex = 0;
			while (lineIndex < allLines.Length)
			{
				var nextCommand = allLines[lineIndex].Split(' ')[1];
				if (nextCommand == "cd")
				{
					CD();
				}
				else if (nextCommand == "ls")
				{
					LS();
				}
			}
		}

		private void CD()
		{
			var newDirectory = allLines[lineIndex].Split(' ')[2];
			if (newDirectory == "/")
			{
				current = root;
			}
			else if (current != null)
			{
				current = current.GoTo(newDirectory);
			}
			lineIndex++;
		}

		private void LS()
		{
			lineIndex++;
			while (lineIndex < allLines.Length && !allLines[lineIndex].StartsWith("$"))
			{
				if (allLines[lineIndex].StartsWith("dir") && current != null)
				{
					current.AddSubdirectory(allLines[lineIndex].Split(' ')[1]);
				}
				else if (current != null)
				{
					var fileSize = long.Parse(allLines[lineIndex].Split(' ')[0]);
					var fileName = allLines[lineIndex].Split(' ')[1];
					current.AddFile(fileName, fileSize);
				}
				lineIndex++;
			}
		}

		public long GetTotalSizeUnderSize(long maxSize)
		{
			var directorySet = new HashSet<Directory>();
			root.GetDirectoriesUnderSize(maxSize, directorySet);
			return directorySet.Sum(x => x.GetTotalSize());
		}
		private class Directory
		{
			private readonly string name;
			private Directory? parent;
			private Dictionary<string, long> files = new ();
			private HashSet<Directory> subdirectories = new ();

			public Directory(string name, Directory? parent)
			{
				this.name = name;
				this.parent = parent;
			}

			public string GetName()
			{
				return name;
			}

			public void AddSubdirectory(string directoryName)
			{
				subdirectories.Add(new Directory(directoryName, this));
			}

			public void AddFile(string fileName, long size)
			{
				files[fileName] = size;
			}

			public Directory? GoTo(string newDirectoy)
			{
				if (newDirectoy == "..")
				{
					return parent;
				}
				else
				{
					foreach (Directory thisSubdirectory in subdirectories)
					{
						if (thisSubdirectory.GetName() == newDirectoy)
						{
							return thisSubdirectory;
						}
					}
				}
				return null;
			}

			public long GetTotalSize()
			{
				long totalSize = 0L;
				foreach (string thisFileName in files.Keys)
				{
					totalSize += files[thisFileName];
				}
				foreach (Directory thisSubdirectory in subdirectories)
				{
					totalSize += thisSubdirectory.GetTotalSize();
				}
				return totalSize;
			}

			public void GetDirectoriesUnderSize(long maxSize, HashSet<Directory> result)
			{
				if (GetTotalSize() <= maxSize)
				{
					result.Add(this);
				}
				foreach (Directory thisSubdirectory in subdirectories)
				{
					thisSubdirectory.GetDirectoriesUnderSize(maxSize,result);
				}
			}

			public override bool Equals(object? obj)
			{
				if (obj is Directory other)
				{
					var thisCursor = this;
					var otherCursor = other;
					while (thisCursor != null && otherCursor != null)
					{
						if (thisCursor.GetName() != otherCursor.GetName())
						{
							return false;
						}
						if (thisCursor.GetName() == "/" && otherCursor.GetName() == "/")
						{
							return true;
						}
						thisCursor = thisCursor.parent;
						otherCursor = otherCursor.parent;
					}
					return false;
				}
				else
				{
					return false;
				}
			}

			public override int GetHashCode()
			{
				int hashCode = 17;
				var cursor = this;				
				while (cursor != null)
				{
					hashCode = (hashCode + cursor.GetName()).GetHashCode();
					cursor = cursor.parent;
				}
				return hashCode;
			}

		}
	}
}