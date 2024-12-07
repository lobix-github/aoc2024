class d07
{
    public void Run()
    {
        var results = new Dictionary<int, HashSet<long>>();
        var q = new Queue<infod07>();
        var lines = File.ReadLines(@"..\..\..\inputs\07.txt").ToList();
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var parts = line.Split(':', StringSplitOptions.TrimEntries);
            var entry = new infod07() { id = i, expected = parts[0].ToInt64() };
            results[i] = new();
            var list = parts[1].Split(" ", StringSplitOptions.TrimEntries).Select(x => x.ToInt64());
            entry.cur = list.First();
            entry.remaining = list.Skip(1).ToList();
            q.Enqueue(entry);
        }

        while (q.Any())
        {
            var entry = q.Dequeue();
            if (!entry.remaining.Any())
            {
                // finished counting
                if (entry.cur == entry.expected) results[entry.id].Add(entry.expected);
                continue;
            }
            var newCur = op(entry.cur, entry.remaining.First(), '+');
            q.Enqueue(new infod07() { id = entry.id, cur = newCur, expected = entry.expected, remaining = entry.remaining.Skip(1).ToList() });
            newCur = op(entry.cur, entry.remaining.First(), '*');
            q.Enqueue(new infod07() { id = entry.id, cur = newCur, expected = entry.expected, remaining = entry.remaining.Skip(1).ToList() });
            // if part 1 comment out below two lines
            newCur = op(entry.cur, entry.remaining.First(), '|');
            q.Enqueue(new infod07() { id = entry.id, cur = newCur, expected = entry.expected, remaining = entry.remaining.Skip(1).ToList() });
        }

        Console.WriteLine(results.Values.SelectMany(h => h).Sum());

        long op(long cur, long arg, char sign) => sign switch
        {
            '+' => cur + arg,
            '*' => cur * arg,
            '|' => (cur.ToString() + arg.ToString()).ToInt64(),
        };
    }
}

class infod07
{
    public int id;
    public long expected;
    public long cur;
    public List<long> remaining = new();
}
