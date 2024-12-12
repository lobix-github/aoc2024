class d12
{
	public void Run()
	{
		List<infoD12> regions = new();
		var map = File.ReadAllLines(@"..\..\..\inputs\12.txt").Select(l => l.ToCharArray()).ToList();

		for (int y = 0; y < map.Count; y++)
		{
			for (int x = 0; x < map[y].Length; x++)
			{
				var point = new infoD12(x, y, map[y][x]);
				if (!regions.SelectMany(x => x.visited).Contains(point))
				{
					regions.Add(point);
					crowl(point);
				}
			}
		}

		var sum = 0;
		foreach (var region in regions)
		{
			var perimiteres = 0;
			var minY = region.visited.Select(x => x.y).Min();
			var maxY = region.visited.Select(x => x.y).Max();
			for (int y = minY; y <= maxY; y++)
			{
				foreach (var p in region.visited.Where(p => p.y == y).OrderBy(p => p.x).Select(p => p))
				{
					if (!sameX(p))
					{
						perimiteres += 2;
					}
				}
			}

			var minX = region.visited.Select(x => x.x).Min();
			var maxX = region.visited.Select(x => x.x).Max();
			for (int x = minX; x <= maxX; x++)
			{
				foreach (var p in region.visited.Where(p => p.x == x).OrderBy(p => p.y).Select(p => p))
				{
					if (!sameY(p))
					{
						perimiteres += 2;
					}
				}
			}

			sum += perimiteres * region.visited.Count;
		}

		Console.WriteLine(sum);

		bool sameX(infoD12 p) => p.x - 1 >= 0 && map[p.y][p.x - 1] == p.plant;
		bool sameY(infoD12 p) => p.y - 1 >= 0 && map[p.y - 1][p.x] == p.plant;

		void crowl(infoD12 point)
		{
			var q = new Queue<infoD12>();
			q.Enqueue(point);
			while (q.Any())
			{
				var cur = q.Dequeue();

				if (cur.visited.Contains(cur))
				{
					continue;
				}
				cur.visited.Add(cur);

				TryQueue(cur.GetNext(cur.x - 1, cur.y));
				TryQueue(cur.GetNext(cur.x + 1, cur.y));
				TryQueue(cur.GetNext(cur.x, cur.y - 1));
				TryQueue(cur.GetNext(cur.x, cur.y + 1));

				void TryQueue(infoD12 next)
				{
					if (isInMap(next) && map[next.y][next.x] == cur.plant)
					{
						q.Enqueue(next);
					}
				}
			}
		}
		
		bool isInMap(infoD12 p) => p.y >= 0 && p.y < map.Count && p.x >= 0 && p.x < map[0].Length;
	}
}

class infoD12
{
	public infoD12(int x, int y, char plant)
	{
		this.x = x;
		this.y = y;
		this.plant = plant;
	}

	public HashSet<infoD12> visited = new();
	public int x;
	public int y;
	public char plant;

	public infoD12 GetNext(int x, int y) => new infoD12(x, y, plant) { visited = visited };

	public string id => $"x: {x}, y: {y}";

	public override int GetHashCode() => id.GetHashCode();

	public override bool Equals(object obj) => obj is infoD12 other && other.GetHashCode() == this.GetHashCode();

	public override string ToString() => $"{id}, plant: {plant}, sum: {visited.Count}";
};
