using static System.Net.Mime.MediaTypeNames;

class d10
{
    public void Run()
    {
		var trailheads = new HashSet<infoD10>();

		var map = new List<char[]>();
        map = File.ReadAllLines(@"..\..\..\inputs\10.txt").Select(l => l.ToCharArray()).ToList();

		var q = new Queue<infoD10>();
		for (int y = 0; y < map.Count; y++)
			for (int x = 0; x < map[y].Length; x++)
				if (map[y][x] == '0')
				{
					var trailhead = new infoD10(x, y);
					trailheads.Add(trailhead);
					q.Enqueue(trailhead);
				}

		while (q.Any())
		{
			var cur = q.Dequeue();

			if (cur.visited.Contains(cur))
			{
				continue;
			}
			cur.visited.Add(cur);

			if (cur.height == 9)
			{
				cur.score.Add(cur);
				continue;
			}

			TryQueue(cur.GetNext(cur.x - 1, cur.y));
			TryQueue(cur.GetNext(cur.x + 1, cur.y));
			TryQueue(cur.GetNext(cur.x, cur.y - 1));
			TryQueue(cur.GetNext(cur.x, cur.y + 1));

			void TryQueue(infoD10 next)
			{
				if (isInMap(next) && int.TryParse([map[next.y][next.x]], out var height) && height == cur.height + 1)
				{
					q.Enqueue(next);
				}
			}
		}

		var sum = trailheads.Select(x => x.score.Count).Sum();
		Console.WriteLine(sum);

		bool isInMap(infoD10 p) => p.y >= 0 && p.y < map.Count && p.x >= 0 && p.x < map[0].Length;
	}

	class infoD10
	{
		public infoD10(int x, int y)
		{
			this.x = x; 
			this.y = y;
		}

		public HashSet<infoD10> visited = new();
		public HashSet<infoD10> score = new();
		public int len = 0;
		public int height = 0;
		public int x;
		public int y;

		public infoD10 GetNext(int x, int y) => new infoD10(x, y) { len = len + 1, height = height + 1, visited = visited, score = score };

		public override int GetHashCode() => $"x: {x}, y: {y}".GetHashCode();

		public override bool Equals(object obj) => obj is infoD10 other && other.GetHashCode() ==  this.GetHashCode();

		public override string ToString() => $"x: {x}, y: {y}, height: {height}";
	};
}
