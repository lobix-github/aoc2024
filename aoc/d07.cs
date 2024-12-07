class d07
{
    public void Run()
    {
        List<int> list1 = new();

        var q = new Queue<infod07>();
        var lines = File.ReadLines(@"..\..\..\inputs\07.txt").ToList();
        foreach (var line in lines)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);
            var entry = new infod07() { expected = parts[0].ToInt64() };
            var list = parts[1].Split(" ", StringSplitOptions.TrimEntries).Select(x => x.ToInt64());
            entry.cur = list.First();
            entry.remaining = list.Skip(1).ToList();
            q.Enqueue(entry);
        }

        var results = new HashSet<long>();
        while (q.Any())
        {
            var entry = q.Dequeue();
            if (!entry.remaining.Any())
            {
                // finished counting
                if (entry.cur == entry.expected) results.Add(entry.expected);
                continue;
            }
            var newCur = op(entry.cur, entry.remaining.First(), '+');
            q.Enqueue(new infod07() { cur = newCur, expected = entry.expected, remaining = entry.remaining.Skip(1).ToList() });
            newCur = op(entry.cur, entry.remaining.First(), '*');
            q.Enqueue(new infod07() { cur = newCur, expected = entry.expected, remaining = entry.remaining.Skip(1).ToList() });
        }

        Console.WriteLine(results.Sum());

        long op(long cur, long arg, char sign) => sign switch
        {
            '+' => cur + arg,
            '*' => cur * arg,
        };
    }
}

class infod07
{
    public long expected;
    public long cur;
    public List<long> remaining = new();
}
