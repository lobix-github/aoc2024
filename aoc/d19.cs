class d19
{
	public void Run()
	{
		var lines = File.ReadAllText(@"..\..\..\inputs\19.txt");
		var blocks = lines.Split($"{Environment.NewLine}{Environment.NewLine}");
		var towels = blocks[0].Split(", ").ToArray();
		var patterns = blocks[1].Split(Environment.NewLine).ToArray();

		var dict = new Dictionary<string, long>();

		var possible = 0;
		long sum = 0;
		foreach (var pattern in patterns)
		{
			long count = checkAll(pattern);
			if (count > 0)
			{
				possible++;
				sum += count;
			}
		}
		Console.WriteLine(possible);
		Console.WriteLine(sum);

		long checkAll(string pattern)
		{
			if (dict.ContainsKey(pattern))
			{
				return dict[pattern];
			}

			if (pattern.Length == 0) return 1;
			long result = 0;
			foreach (var towel in towels)
			{
				if (pattern.StartsWith(towel))
				{
					result += checkAll(pattern.Substring(towel.Length));
				}
			}
			dict[pattern] = result;
			return result;
		}
	}
}

