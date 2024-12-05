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

        var sum1 = 0;
        var sum2 = 0;
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
            var incorrects = new List<List<int>>();

            foreach (var val in update)
            {
                if (toCheck.Any(x => x.value == val && !x.visited)) { ok = false; break; }
                toCheck.Where(x => x.key == val).ToList().ForEach(x => x.visited = true);
            }
            if (ok)
            {
                // part 1
                sum1 += update[update.Count / 2];
            }
            else
            {
                // part 2
                bool again;
                do
                {
                    again = false;
                    foreach (var val in update)
                    {
                        var pair = toCheck.FirstOrDefault(x => x.value == val && !x.visited);
                        if (pair != default) 
                        {
                            update[update.IndexOf(pair.key)] = pair.value;
                            update[update.IndexOf(pair.value)] = pair.key;
                            again = true;
                            break;
                        }
                        toCheck.Where(x => x.key == val).ToList().ForEach(x => x.visited = true);
                    }
                } while(again);
                sum2 += update[update.Count / 2];
            }
        }

        Console.WriteLine(sum1);
        Console.WriteLine(sum2);
    }
}

record d05info(int key, int value)
{
    public bool visited { get; set; } = false;
    public d05info Copy() => new d05info(key, value);
}


