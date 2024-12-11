using System.Numerics;

class d11
{
	public void Run()
	{
		var list = new List<long>();
		list = File.ReadAllText(@"..\..\..\inputs\11.txt").Split(' ').Select(x => x.ToInt64()).ToList();
		
		var list2 = new List<long>();
		for (int idx = 0; idx < 25; idx++)
		{
			var count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (i % 10000000 == 0) Console.WriteLine($"idx: {idx}, i: {i}/{count} ({(long)i * 100 / count}%)");

				var stone = list[i];
				if (stone == 0) list2.Add(1);
				else if (stone.ToString() is string stoneString && stoneString.Length % 2 == 0)
				{
					var halfLen = stoneString.Length / 2;
					list2.Add(stoneString.Substring(0, halfLen).ToInt64());
					list2.Add(stoneString.Substring(halfLen, halfLen).ToInt64());
				}
				else list2.Add(stone * 2024);
			}
			list = list2.ToList();
			list2.Clear();
		}

		Console.WriteLine(list.Count);
	}
}
