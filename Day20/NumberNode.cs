namespace Day20
{
	internal class NumberNode
	{
		public static int nodeCount = 0;
		private readonly long value;
		private NumberNode? previous = null;
		private NumberNode? next = null;

		public NumberNode(long value)
		{
			this.value = value;
		}

		public long GetValue()
		{
			return value;
		}

		public void SetPrevious(NumberNode newPrevious)
		{
			previous = newPrevious;
		}

		public void SetNext(NumberNode newNext)
		{
			next = newNext;
		}

		public long GetNthNext(int n)
		{
			if (n == 0)
			{
				return value;
			}
			else
			{
				return next.GetNthNext(n - 1);
			}
		}

		public void Mix()
		{
			long repititions;
			if (Math.Abs(value) < nodeCount)
			{
				repititions = Math.Abs(value);
			}
			else
			{
				repititions = Math.Abs(value) % (nodeCount - 1);
			}
			for (long i = 0; i < repititions; i++)
			{
				if (value > 0L)
				{
					MoveForward();
				}
				else if (value < 0L)
				{
					MoveBackward();
				}
			}			
		}

		private void MoveForward()
		{
			var Z = previous;
			var A = this;
			var B = next;
			var C = next.next;
			Z.SetNext(B);
			A.SetPrevious(B);
			A.SetNext(C);
			B.SetPrevious(Z);
			B.SetNext(A);
			C.SetPrevious(A);
		}

		private void MoveBackward()
		{
			var Y = previous.previous;
			var Z = previous;
			var A = this;
			var B = next;
			Y.SetNext(A);
			Z.SetPrevious(A);
			Z.SetNext(B);
			A.SetPrevious(Y);
			A.SetNext(Z);
			B.SetPrevious(Z);
		}

	}
}