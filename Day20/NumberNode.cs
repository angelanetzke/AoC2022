namespace Day20
{
	internal class NumberNode
	{
		private readonly int value;
		private NumberNode? previous = null;
		private NumberNode? next = null;

		public NumberNode(int value)
		{
			this.value = value;
		}

		public int GetValue()
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

		public int GetNthNext(int n)
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
			for (int i = 0; i < Math.Abs(value); i++)
			{
				if (value > 0)
				{
					MoveForward();
				}
				else if (value < 0)
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