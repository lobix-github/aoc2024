using System.Collections.Concurrent;

class d20 : baseD
{
	public void Run()
	{
        var lines = File.ReadAllLines(@"..\..\..\inputs\20.txt");
		var map = new HashSet<IntComplex>();
		var free = new HashSet<infoD20>();
		IntComplex S = default;
		IntComplex E = default;
		for (int y = 0; y < lines.Length; y++)
		{
			for (int x = 0; x < lines[y].Length; x++)
			{
				var c = lines[y][x];
				var p = new IntComplex(x, y);
				if (c == '#')
				{
					map.Add(p);
				}
				else
				{
					if (c == 'S') S = p;
					if (c == 'E') E = p;
					free.Add(new infoD20(p));
				}
			}
		}
		var start = new infoD20(S);
		var q = new PriorityQueue<infoD20, int>();
		q.Enqueue(start, Math.Abs(S.Real - E.Real) + Math.Abs(S.Imaginary - E.Imaginary));
		Dictionary<infoD20, int> visited = [];
		HashSet<IntComplex> shortestPath = [];

		var result = int.MaxValue;
		while (q.Count > 0)
		{
			var cur = q.Dequeue();
			if (!visited.TryAdd(cur, cur.visited))
			{
				if (cur.visited >= visited[cur])
				{
					continue;
				}
			}
			visited[cur] = cur.visited;

			if (cur.pos == E)
			{
				if (cur.visited < result)
				{
					result = cur.visited;
					shortestPath = cur.hist;
				}
				continue;
			}

			TryEnqueue(cur.GetNext(Left));
			TryEnqueue(cur.GetNext(Right));
			TryEnqueue(cur.GetNext(Up));
			TryEnqueue(cur.GetNext(Down));

			void TryEnqueue(infoD20 next)
			{
				if (isOK(next))
				{
					var prio = Math.Abs(next.pos.Real - E.Real) + Math.Abs(next.pos.Imaginary - E.Imaginary);
					q.Enqueue(next, prio);
				}
			}
			bool isOK(infoD20 next) => next.visited < result && !map.Contains(next.pos);
		}
		Console.WriteLine(result);

		//print();
		SurroundMapWithEdges(map);
		SurroundMapWithEdges(map);
		SurroundMapWithEdges(map);
		//print();
		var candidates = new HashSet<(infoD20, infoD20)> ();
		foreach (var p in shortestPath)
		{
			checkAdd(Left);
			checkAdd(Right);
			checkAdd(Up);
			checkAdd(Down);

			void checkAdd(IntComplex dir)
			{
				if (map.Contains(p + dir) && visited.Keys.Contains(new infoD20(p + dir + dir))) candidates.Add((new infoD20(p + dir), new infoD20(p + dir)));
				if (map.Contains(p + dir) && map.Contains(p + dir + dir) && visited.Keys.Contains(new infoD20(p + dir + dir + dir))) candidates.Add((new infoD20(p + dir), new infoD20(p + dir + dir)));
			}
		}

		List<HashedHashSet<IntComplex>> uniqueMaps = new();
		foreach (var candidate in candidates)
		{
			var localMap = new HashedHashSet<IntComplex>(map);
			localMap.Remove(candidate.Item1.pos);
			localMap.Remove(candidate.Item2.pos);
			if (uniqueMaps.Contains(localMap))
			{

			}
			uniqueMaps.Add(localMap);
		}

		var idx = 0;
		var count = 0;
		var rangePartitioner = Partitioner.Create(0, uniqueMaps.Count);
		Parallel.ForEach(rangePartitioner, (range, loopState) =>
		{
			for (int i = range.Item1; i < range.Item2; i++)
			{
				Interlocked.Increment(ref idx);
				Console.WriteLine($"{idx}/{uniqueMaps.Count}");
				var localMap = uniqueMaps[i];
				var localResult = int.MaxValue;
				var q = new PriorityQueue<infoD20, int>();
				q.Enqueue(start, Math.Abs(S.Real - E.Real) + Math.Abs(S.Imaginary - E.Imaginary));
				Dictionary<infoD20, int> localVisited = [];
				while (q.Count > 0)
				{
					var cur = q.Dequeue();
					if (!localVisited.TryAdd(cur, cur.visited))
					{
						if (cur.visited >= localVisited[cur])
						{
							continue;
						}
					}
					if (visited.ContainsKey(cur))
					{
						if (cur.visited > visited[cur])
						{
							continue;
						}
					}
					localVisited[cur] = cur.visited;

					if (cur.pos == E)
					{
						if (cur.visited < result)
						{
							localResult = cur.visited;
						}
						continue;
					}

					TryEnqueue(cur.GetNext2(Left));
					TryEnqueue(cur.GetNext2(Right));
					TryEnqueue(cur.GetNext2(Up));
					TryEnqueue(cur.GetNext2(Down));

					void TryEnqueue(infoD20 next)
					{
						if (isOK(next))
						{
							var prio = Math.Abs(next.pos.Real - E.Real) + Math.Abs(next.pos.Imaginary - E.Imaginary);
							q.Enqueue(next, prio);
						}
					}
					bool isOK(infoD20 next) => next.visited < localResult && !localMap.Contains(next.pos);
				}
				if (result - localResult >= 100)
				{
					Interlocked.Increment(ref count);
				}
			}
		});

		Console.WriteLine(count);

		void print()
		{
			var xMin = map.Select(x => x.Real).Min();
			var xMax = map.Select(x => x.Real).Max();
			var yMin = map.Select(x => x.Imaginary).Min();
			var yMax = map.Select(x => x.Imaginary).Max();

			for (int y = yMin; y <= yMax; y++)
			{
				for (int x = xMin; x <= xMax; x++)
				{
					var p = new IntComplex(x, y);
					var info = new infoD20(p);
					Console.Write(visited.ContainsKey(info) ? 'O' : map.Contains(p) ? '#' : '.');
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
	}

	class infoD20
	{
		public readonly IntComplex pos;

		private string id;
		private int hashCode;
		public int visited = 0;
		public HashSet<IntComplex> hist = [];

		public infoD20(IntComplex pos)
		{
			this.pos = pos;

			id = $"x: {(int)pos.Real}, y: {(int)pos.Imaginary}";
			hashCode = id.GetHashCode();
		}

		public infoD20 GetNext(IntComplex newDir)
		{
			var h = hist.ToHashSet();
			h.Add(pos + newDir);
			return new infoD20(pos + newDir) { visited = visited + 1, hist = h };
		}

		public infoD20 GetNext2(IntComplex newDir) => new infoD20(pos + newDir) { visited = visited + 1 };

		public override int GetHashCode() => hashCode;

		public override bool Equals(object obj) => obj.GetHashCode() == hashCode;

		public override string ToString() => id;
	};
}

