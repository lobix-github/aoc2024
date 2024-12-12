using System.Linq;

class d12
{
	static List<char[]> map = new();
	public void Run()
	{
		List<infoD12> regions = new();
		map = File.ReadAllLines(@"..\..\..\inputs\12.txt").Select(l => l.ToCharArray()).ToList();

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
        var sum2 = 0;
        foreach (var region in regions)
		{
			var perimiteres = 0;
			var minY = region.visited.Select(x => x.y).Min();
			var maxY = region.visited.Select(x => x.y).Max();
			var sides = 0;
			for (int y = minY; y <= maxY; y++)
			{
                foreach (var p in region.visited.Where(p => p.y == y).OrderBy(p => p.x).Select(p => p))
				{
				    var xs = region.visited.Where(x => x.x == p.x);
                    if (!sameX(p))
					{
						perimiteres += 2;

						var above = new infoD12(p.x, y - 1, '.');
						var below = new infoD12(p.x, y + 1, '.');
                        if (!xs.Contains(above)) sides++;
                        if (!xs.Contains(below)) sides++;
					}
				}
			}

			var minX = region.visited.Select(x => x.x).Min();
			var maxX = region.visited.Select(x => x.x).Max();
			for (int x = minX; x <= maxX; x++)
			{
				foreach (var p in region.visited.Where(p => p.x == x).OrderBy(p => p.y).Select(p => p))
				{
				    var ys = region.visited.Where(x => x.y == p.y);
                    if (!sameY(p))
					{
						perimiteres += 2;

                        var left = new infoD12(x - 1, p.y, '.');
                        var right = new infoD12(x + 1, p.y + 1, '.');
                        if (!ys.Contains(left)) sides++;
                        if (!ys.Contains(right)) sides++;
                    }
				}
			}

			sum += perimiteres * region.visited.Count;
		
            var inners = region.visited.ToList();
            foreach (var p in inners)
			{
                var lu = new infoD12(p.x - 1, p.y - 1, '.');
                var u = new infoD12(p.x, p.y - 1, '.');
                var ru = new infoD12(p.x + 1, p.y - 1, '.');
                var r = new infoD12(p.x + 1, p.y, '.');
                var rb = new infoD12(p.x + 1, p.y + 1, '.');
                var b = new infoD12(p.x, p.y + 1, '.');
                var lb = new infoD12(p.x - 1, p.y + 1, '.');
                var l = new infoD12(p.x - 1, p.y, '.');

                if (!region.visited.Contains(l) && !region.visited.Contains(u) /*&& !region.visited.Contains(lu)*/)
                {
                    region.edgeCorners.Add(new cornerPoint(p.x, p.y, p.GetHashCode()));
                }
                if (region.visited.Contains(l) && region.visited.Contains(u) && !region.visited.Contains(lu))
                {
                    region.edgeCorners.Add(new cornerPoint(p.x, p.y, p.GetHashCode()));
                }

                if (!region.visited.Contains(u) && !region.visited.Contains(r) /*&& !region.visited.Contains(ru)*/)
                {
                    region.edgeCorners.Add(new cornerPoint(p.x + 1, p.y, p.GetHashCode()));
                }
                if (region.visited.Contains(u) && region.visited.Contains(r) && !region.visited.Contains(ru))
                {
                    region.edgeCorners.Add(new cornerPoint(p.x + 1, p.y, p.GetHashCode()));
                }

                if (!region.visited.Contains(r) && !region.visited.Contains(b) /*&& !region.visited.Contains(rb)*/)
                {
                    region.edgeCorners.Add(new cornerPoint(p.x + 1, p.y + 1, p.GetHashCode()));
                }
                if (region.visited.Contains(r) && region.visited.Contains(b) && !region.visited.Contains(rb))
                {
                    region.edgeCorners.Add(new cornerPoint(p.x + 1, p.y + 1, p.GetHashCode()));
                }

                if (!region.visited.Contains(b) && !region.visited.Contains(l) /*&& !region.visited.Contains(lb)*/)
                {
                    region.edgeCorners.Add(new cornerPoint(p.x, p.y + 1, p.GetHashCode()));
                }
                if (region.visited.Contains(b) && region.visited.Contains(l) && !region.visited.Contains(lb))
                {
                    region.edgeCorners.Add(new cornerPoint(p.x, p.y + 1, p.GetHashCode()));
                }
            }

            var edges = region.edgeCorners.ToList();
            sum2 += edges.Count * region.visited.Count;
        }


        Console.WriteLine(sum);
        Console.WriteLine(sum2);

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
	}

    static bool isInMap(infoD12 p) => p.y >= 0 && p.y < map.Count && p.x >= 0 && p.x < map[0].Length;

	class cornerPoint : CPoint
	{
        public readonly int _id;
        
		public cornerPoint(int x, int y, int _id = 0) : base(x, y)
        {
            this._id = _id;
        }

        public override string id => $"{base.id}, id: {_id}";

        public override string ToString() => $"{base.ToString()}";
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

        public HashSet<cornerPoint> edgeCorners = new();

        public infoD12 GetNext(int x, int y) => new infoD12(x, y, plant) { visited = visited };

        public string id => $"x: {x}, y: {y}";

        public override int GetHashCode() => id.GetHashCode();

        public override bool Equals(object obj) => obj is infoD12 other && other.GetHashCode() == this.GetHashCode();

        public override string ToString() => $"{id}, plant: {plant}, sum: {visited.Count}";
    };
}

