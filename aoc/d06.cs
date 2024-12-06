class d06 : baseD
{
    public void Run()
    {
        var map = new List<char[]>();
        var visited = new HashSet<DPoint>();

        map = File.ReadAllLines(@"..\..\..\inputs\06.txt").Select(l => l.ToCharArray()).ToList();

        d06Point cur = default, start = default;
        for (int y = 0; y < map.Count; y++)
            for (int x = 0; x < map[y].Length; x++)
                if (map[y][x] == '^') start = cur = new d06Point(x, y, Dirs.N);

        while (true)
        {
			visited.Add(new DPoint(cur.X, cur.Y));
            cur = move(cur);
            if (!isInMap(cur)) break;
		}

        // part 1
        Console.WriteLine(visited.Count);

        var visited2 = new HashSet<d06Point>();
        var obstacles = 0;
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                var org = map[y][x];
				map[y][x] = '#';
                visited2.Clear();
                cur = start;
				while (true)
				{
                    if (visited2.Contains(cur))
                    {
                        obstacles++;
                        break;
                    }
					visited2.Add(cur);
					cur = move(cur);
					if (!isInMap(cur)) break;
				}
				map[y][x] = org;
			}
		}
        // part 2
		Console.WriteLine(obstacles);

		bool isInMap(d06Point cur) => cur.Y >= 0 && cur.Y < map.Count && cur.X >= 0 && cur.X < map[0].Length;

		d06Point move(d06Point cur)
        {
            var next = getNext(cur);
            if (isInMap(next) && map[next.Y][next.X] == '#') return cur with { dir = (Dirs)((((int)cur.dir + 1) % 4)) };
            return next;
        }

		static d06Point getNext(d06Point cur) => cur.dir switch
		{
			Dirs.N => new d06Point(cur.X, cur.Y - 1, Dirs.N),
			Dirs.S => new d06Point(cur.X, cur.Y + 1, Dirs.S),
			Dirs.E => new d06Point(cur.X + 1, cur.Y, Dirs.E),
			Dirs.W => new d06Point(cur.X - 1, cur.Y, Dirs.W),
			_ => throw new NotImplementedException(),
		};
	}

	record d06Point(int X, int Y, Dirs dir);
}


