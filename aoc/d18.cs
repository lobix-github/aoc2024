class d18 : baseD
{
	public void Run()
	{
		//var PARAMS = (12, 7);
		var PARAMS = (1024, 71);

		var lines = File.ReadAllLines(@"..\..\..\inputs\18.txt");
		var map = new HashSet<IntComplex>();
		var idx = 0;
		foreach (var line in lines)
		{
			var p = new IntComplex(line.Split(',')[0].ToInt32(), line.Split(',')[1].ToInt32());
			map.Add(p);
			idx++;
			if (idx == PARAMS.Item1) break;
		}

		SurroundMapWithEdges(map);

		// find shortest path by flood fill
		var S = new IntComplex(0, 0);
		var E = new IntComplex(PARAMS.Item2 - 1, PARAMS.Item2 - 1);
		var start = new infoD18(S);
		var q = new PriorityQueue<infoD18, int>();
		q.Enqueue(start, Math.Abs(S.Real - E.Real) + Math.Abs(S.Imaginary - E.Imaginary));
		Dictionary<infoD18, int> visited = [];
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

			void TryEnqueue(infoD18 next)
			{
				if (isOK(next))
				{
					var prio = Math.Abs(next.pos.Real - E.Real) + Math.Abs(next.pos.Imaginary - E.Imaginary);
					q.Enqueue(next, prio);
				}
			}
			bool isOK(infoD18 next) => next.visited < result && !map.Contains(next.pos);
		}

		Console.WriteLine(result);

		var c = 0;
		foreach (var line in lines.Skip(idx))
		{
			Console.WriteLine($"{c++}/{lines.Length - idx}");
			var p = new IntComplex(line.Split(',')[0].ToInt32(), line.Split(',')[1].ToInt32());
			map.Add(p);

			q = new PriorityQueue<infoD18, int>();
			q.Enqueue(start, Math.Abs(S.Real - E.Real) + Math.Abs(S.Imaginary - E.Imaginary));
			bool endReached = false;
			result = int.MaxValue;
			visited = [];
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
					endReached = true;
					break;
				}

				TryEnqueue(cur.GetNext(Left));
				TryEnqueue(cur.GetNext(Right));
				TryEnqueue(cur.GetNext(Up));
				TryEnqueue(cur.GetNext(Down));

				void TryEnqueue(infoD18 next)
				{
					if (isOK(next))
					{
						var prio = Math.Abs(next.pos.Real - E.Real) + Math.Abs(next.pos.Imaginary - E.Imaginary);
						q.Enqueue(next, prio);
					}
				}
				bool isOK(infoD18 next) => next.visited < result && !map.Contains(next.pos);
			}

			if (!endReached)
			{
				Console.WriteLine(p);
				break;
			}
		}
	}

	class infoD18
	{
		public readonly IntComplex pos;

		private string id;
		private int hashCode;
		public int visited = 0;
		public HashSet<IntComplex> hist = [];

		public infoD18(IntComplex pos)
		{
			this.pos = pos;

			id = $"x: {(int)pos.Real}, y: {(int)pos.Imaginary}";
			hashCode = id.GetHashCode();
		}

		public infoD18 GetNext(IntComplex newDir)
		{
			var h = hist.ToHashSet();
			h.Add(pos + newDir);
			return new infoD18(pos + newDir) { visited = visited + 1, hist = h };
		}

		public override int GetHashCode() => hashCode;

		public override bool Equals(object obj) => obj is infoD18 other && other.GetHashCode() == hashCode;

		public override string ToString() => id;
	};
}

