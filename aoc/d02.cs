class d02
{
    public void Run()
    {
        List<List<int>> list = new();

        var lines = File.ReadLines(@"..\..\..\inputs\02.txt").ToList();
        foreach (var line in lines)
        {
            list.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToInt32()).ToList());
        }

        var result1 = list.Where(x => test(x.ToList())).Count();
        Console.WriteLine(result1);

        var unsafes = list.Where(x => !test(x.ToList())).ToList();
        var @fixed = unsafes.Where(x =>
        {
            for (int i = 0; i < x.Count; i++)
            {
                var candidate = x.ToList();
                candidate.RemoveAt(i);

                if (test(candidate.ToList())) return true;
            }
            return false;
        }).Count();

        var result2 = result1 + @fixed;
        Console.WriteLine(result2);
	}

	bool test(List<int> x)
    {
		if (x[0] == x[1]) return false;

		var set = new HashSet<int>(x);
		if (x[0] < x[1]) x.Sort(); else x = x.OrderDescending().ToList();
		if (!x.SequenceEqual(set)) return false;

		var safe = true;
		x.Skip(1).Select((i, idx) => safe &= Math.Abs(x[idx] - i) <= 3).ToArray();
		return safe;
	}
}
