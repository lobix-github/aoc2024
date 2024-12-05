class d05 : baseD
{
    public void Run()
    {
        var pairs = new List<d05info>();
        var updates = new List<List<int>>();

        var lines = File.ReadAllLines(@"..\..\..\inputs\05.txt").ToList();
        var readPairs = true;
        foreach (var line in lines)
        {
            if (line == "")
            {
                readPairs = false;
                continue;
            }

            if (readPairs)
            {
                pairs.Add(new d05info(line.Split('|')[0].ToInt32(), line.Split('|')[1].ToInt32()));
                continue;
            }

            var h = new List<int>();
            updates.Add(h);
            line.Split(',').ToList().ForEach(x => h.Add(x.ToInt32()));
        }

        var sum = 0;
        foreach (var update in updates)
        {
            var toCheck = new List<d05info>();
            foreach (var pair in pairs) 
            {
                if (update.Contains(pair.key) && update.Contains(pair.value))
                {
                    toCheck.Add(pair.Copy());
                }
            }

            var ok = true;
            foreach (var val in update)
            {
                if (toCheck.Any(x => x.value == val && !x.visited)) { ok = false; break; }
                toCheck.Where(x => x.key == val).ToList().ForEach(x => x.visited = true);
            }
            if (ok)
            {
                sum += update[update.Count / 2];
            }
        }
		Console.WriteLine(sum);
    }
}

record d05info(int key, int value)
{
    public bool visited { get; set; } = false;
    public d05info Copy() => new d05info(key, value);
}


