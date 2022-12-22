using System.Text.RegularExpressions;

namespace Day21
{	
	internal class Equation
	{
		private static Regex numberRegex = new Regex(@"^-?\d+$");
		private static Regex expressionRegex = new Regex(@"^-?\d+ [+\-*/]{1} -?\d+$");
		private static Regex operatorRegex = new Regex(@"[+\-*/]{1}");
		private readonly string humanSymbol = "humn";
		private readonly string rootSymbol = "root";
		private readonly string left;
		private readonly string right;

		public Equation(string[] allLines)
		{
			var entries = new Dictionary<string, string>();
			foreach (string thisLine in allLines)
			{
				entries[thisLine.Split(": ")[0]] = thisLine.Split(": ")[1];
			}
			left = entries[rootSymbol].Split(' ')[0];
			var evaluationQueue = new Queue<string>();
			evaluationQueue.Enqueue(entries[rootSymbol].Split(' ')[0]);
			while (evaluationQueue.Count > 0)
			{
				var thisEntry = evaluationQueue.Dequeue();
				if (numberRegex.IsMatch(entries[thisEntry]))
				{
					left = left.Replace(thisEntry, entries[thisEntry]);
				}		
				else
				{
					left = left.Replace(thisEntry, "(" + entries[thisEntry] + ")");
					var firstSymbol = entries[thisEntry].Split(' ')[0];
					var secondSymbol = entries[thisEntry].Split(' ')[2];
					if (firstSymbol != humanSymbol)
					{
						evaluationQueue.Enqueue(firstSymbol);
					}
					if (secondSymbol != humanSymbol)
					{
						evaluationQueue.Enqueue(secondSymbol);
					}
				}	
			}
			right = entries[rootSymbol].Split(' ')[2];
			evaluationQueue = new Queue<string>();
			evaluationQueue.Enqueue(entries[rootSymbol].Split(' ')[2]);
			while (evaluationQueue.Count > 0)
			{
				var thisEntry = evaluationQueue.Dequeue();
				if (numberRegex.IsMatch(entries[thisEntry]))
				{
					right = right.Replace(thisEntry, entries[thisEntry]);
				}		
				else
				{
					right = right.Replace(thisEntry, "(" + entries[thisEntry] + ")");
					var firstSymbol = entries[thisEntry].Split(' ')[0];
					var secondSymbol = entries[thisEntry].Split(' ')[2];
					if (firstSymbol != humanSymbol)
					{
						evaluationQueue.Enqueue(firstSymbol);
					}
					if (secondSymbol != humanSymbol)
					{
						evaluationQueue.Enqueue(secondSymbol);
					}
				}	
			}
		}

		public long Solve()
		{
			if (left.Contains(humanSymbol))
			{
				return Solve(left, Evaluate(right));				
			}
			else
			{
				return Solve(right, Evaluate(left));
			}
		}

		private long Evaluate(string theExpression)
		{
			if (expressionRegex.IsMatch(theExpression))
			{
				var leftValue = long.Parse(theExpression.Split(' ')[0]);
				var operation = theExpression.Split(' ')[1];
				var rightValue = long.Parse(theExpression.Split(' ')[2]);
				switch (operation)
				{
					case "+":
						return leftValue + rightValue;
					case "-":
						return leftValue - rightValue;
					case "*":
						return leftValue * rightValue;
					case "/":
						return leftValue / rightValue;
				}				
			}
			else if (numberRegex.IsMatch(theExpression))
			{
				return long.Parse(theExpression);
			}
			else
			{
				var operationIndex = IndexOfOperation(theExpression);
				if (operationIndex < 0)
				{
					return Evaluate(theExpression.Substring(1, theExpression.Length - 2));
				}
				else
				{
					var leftSubstring = theExpression[0..(operationIndex - 1)];
					var rightSubstring = theExpression[(operationIndex + 2)..];
					switch (theExpression[operationIndex])
					{
						case '+':
							return Evaluate(leftSubstring) + Evaluate(rightSubstring);
						case '-':
							return Evaluate(leftSubstring) - Evaluate(rightSubstring);
						case '*':
							return Evaluate(leftSubstring) * Evaluate(rightSubstring);
						case '/':
							return Evaluate(leftSubstring) / Evaluate(rightSubstring);
					}
				}
			}
			return 0L;
		}

		private long Solve(string theExpression, long equalTo)
		{
			var operationIndex = IndexOfOperation(theExpression);
			if (theExpression == humanSymbol)
			{
				return equalTo;
			}
			else if (operationIndex < 0)
			{
				return Solve(theExpression.Substring(1, theExpression.Length - 2), equalTo);
			}
			else
			{
				var leftSubstring = theExpression[0..(operationIndex - 1)];
				var rightSubstring = theExpression[(operationIndex + 2)..];
				if (leftSubstring.Contains(humanSymbol))
				{
					switch (theExpression[operationIndex])
					{
						case '+':
							equalTo -= Evaluate(rightSubstring);
							return Solve(leftSubstring, equalTo);
						case '-':
							equalTo += Evaluate(rightSubstring);
							return Solve(leftSubstring, equalTo);
						case '*':
							equalTo /= Evaluate(rightSubstring);
							return Solve(leftSubstring, equalTo);
						case '/':
							equalTo *= Evaluate(rightSubstring);
							return Solve(leftSubstring, equalTo);
					}
				}
				else
				{
					switch (theExpression[operationIndex])
					{
						case '+':
							equalTo -= Evaluate(leftSubstring);
							return Solve(rightSubstring, equalTo);
						case '-':
							equalTo = Evaluate(leftSubstring) - equalTo;
							return Solve(rightSubstring, equalTo);
						case '*':
							equalTo /= Evaluate(leftSubstring);
							return Solve(rightSubstring, equalTo);
						case '/':
							equalTo = Evaluate(leftSubstring) / equalTo;
							return Solve(rightSubstring, equalTo);
					}
				}
			}
			return 0L;
		}

		private static int IndexOfOperation(string theExpression)
		{
			var operationIndex = -1;
			var openParens = 0;
			for (int i = 0; i < theExpression.Length; i++)
			{
				if (theExpression[i] == '(')
				{
					openParens++;
				}
				else if (theExpression[i] == ')')
				{
					openParens--;
				}
				else if (openParens == 0 && operatorRegex.IsMatch(theExpression[i].ToString()))
				{
					operationIndex = i;
					break;
				}
			}
			return operationIndex;
		}

	}
}