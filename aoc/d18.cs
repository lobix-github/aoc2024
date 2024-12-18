class d18 : baseD
{
	public void Run()
	{
		//var PARAMS = (12, 7);
		var PARAMS = (1024, 71);

		var lines = File.ReadAllLines(@"..\..\..\inputs\18.txt");
		var map = new Dictionary<IntComplex, (int, char)>();
		var idx = 0;
		foreach (var line in lines)
		{
			var p = new IntComplex(line.Split(',')[0].ToInt32(), line.Split(',')[1].ToInt32());
			map.Add(p, (idx, '#'));
			idx++;
			if (idx == PARAMS.Item1) break;
		}

		//print();

		var S = new IntComplex(0, 0);
		var E = new IntComplex(PARAMS.Item2 - 1, PARAMS.Item2 - 1);
		var start = new infoD18(S);
		var q = new PriorityQueue<infoD18, int>();
		q.Enqueue(start, Math.Abs(S.Real - E.Real) + Math.Abs(S.Imaginary - E.Imaginary));
		Dictionary<infoD18, int> visited = [];

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
				result = Math.Min(result, cur.visited);
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
			bool isOK(infoD18 next) => next.visited < result && isInMap(next.pos) && !map.ContainsKey(next.pos);
			bool isInMap(IntComplex p) => p.Imaginary >= 0 && p.Imaginary < PARAMS.Item2 && p.Real >= 0 && p.Real < PARAMS.Item2;
		}

		Console.WriteLine(result);

		void print()
		{
			for (int y = 0; y < PARAMS.Item2; y++)
			{
				for (int x = 0; x < PARAMS.Item2; x++)
				{
					var p = new IntComplex(x, y);
					Console.Write(map.ContainsKey(p) ? map[p].Item2 : '.');
				}
				Console.Write(Environment.NewLine);
			}
			Console.Write(Environment.NewLine);
		}
	}

	class infoD18
	{
		public readonly IntComplex pos;

		private string id;
		private int hashCode;
		public int visited = 0;

		public infoD18(IntComplex pos)
		{
			this.pos = pos;

			id = $"x: {(int)pos.Real}, y: {(int)pos.Imaginary}";
			hashCode = id.GetHashCode();
		}

		public infoD18 GetNext(IntComplex newDir) => new infoD18(pos + newDir) { visited = visited + 1 };

		public override int GetHashCode() => hashCode;

		public override bool Equals(object obj) => obj is infoD18 other && other.GetHashCode() == hashCode;

		public override string ToString() => id;
	};
}

