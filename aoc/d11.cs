class d11
{
	public void Run()
	{
		var list = new List<long>();
		list = File.ReadAllText(@"..\..\..\inputs\11.txt").Split(' ').Select(x => x.ToInt64()).ToList();

		var cache = new Dictionary<long, long>();
		foreach (var item in list) cache[item] = 1;
		for (int idx = 0; idx < 75; idx++)
		{
			foreach (var (key, value) in cache.ToList())
			{
				cache[key] -= value;

                if (key == 0) addCreate(1, value);
                else if (key.ToString() is string stoneString && stoneString.Length % 2 == 0)
                {
                    var halfLen = stoneString.Length / 2;
					addCreate(stoneString.Substring(0, halfLen).ToInt64(), value);
					addCreate(stoneString.Substring(halfLen, halfLen).ToInt64(), value);
                }
                else addCreate(key * 2024, value);
            }
        }

		void addCreate(long k, long v)
		{
			cache.TryAdd(k, 0);
			cache[k] += v;
		}

		var sum = cache.Values.Sum();
        Console.WriteLine(sum);
	}
}
