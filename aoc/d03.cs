using System.Text.RegularExpressions;

class d03 : baseD
{
    public void Run()
    {
        var line = File.ReadAllText(@"..\..\..\inputs\03.txt").Replace('^', '_').Replace('$', '_');
    
		long sum = getValue(line);
        Console.WriteLine(sum);

		var indexes = AllIndexesOf(line, "do()").Concat(AllIndexesOf(line, "don't()").Append(0)).ToArray();
		Array.Sort(indexes);
		var ok = true;
		sum = 0;
		for (int i = 1; i < indexes.Length; i++)
		{
			if (ok) sum += getValue(line.Substring(indexes[i - 1], indexes[i] - indexes[i - 1]));
			if (line.Substring(indexes[i], "don't()".Length).StartsWith("don't()"))
			{
				ok = false;
			}
			else if (line.Substring(indexes[i], "do()".Length).StartsWith("do()"))
			{
				ok = true;
			}
		}

		if (ok) sum += getValue(line.Substring(indexes[indexes.Length - 1], line.Length - indexes[indexes.Length - 1]));

		Console.WriteLine(sum);
	}

	long getValue(string text)
    {
		var regex = new Regex(@"mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)");
		var matches = regex.Matches(text);
		long sum = 0;
		foreach (Match match in matches)
		{
			var a = match.Groups["a"].Value.ToInt32();
			var b = match.Groups["b"].Value.ToInt32();
			sum += a * b;
		}
		return sum;
	}
}
