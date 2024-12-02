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

        var result1 = list.Where(x =>
        {
            if (x[0] == x[1]) return false;

            var set = new HashSet<int>(x);
			if (x[0] < x[1]) x.Sort(); else x.OrderDescending();
            if (!x.SequenceEqual(set)) return false;

            var safe = true;
			x.Skip(1).Select((i, idx) => safe &= Math.Abs(x[idx] - i) <= 3).ToArray();
            return safe;
        }).Count();
        Console.WriteLine(result1);
    }
}
