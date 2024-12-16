class d16 : baseD
{
	static Dictionary<infoD16, int> scores = [];
	
	public void Run()
	{
        var lines = File.ReadAllLines(@"..\..\..\inputs\16.txt");
        var map = (
            from y in Enumerable.Range(0, lines.Length)
            from x in Enumerable.Range(0, lines[0].Length)
            select new KeyValuePair<IntComplex, char>(x + y * Down, lines[y][x])
        ).ToDictionary();
		var S = map.Keys.Single(k => map[k] == 'S');
		var E = map.Keys.Single(k => map[k] == 'E');
		var start = new infoD16(S, Right);
		var q = new PriorityQueue<infoD16, int>();
		q.Enqueue(start, Math.Abs(S.Real - E.Real) + Math.Abs(S.Imaginary - E.Imaginary));
		scores[start] = start.score;
		HashSet<IntComplex> histories = [];

		var result = int.MaxValue;
		while (q.Count > 0)
		{
			var cur = q.Dequeue();
			cur.visited.Add(cur.pos);
			if (!scores.TryAdd(cur, cur.score))
			{
				if (cur.score > scores[cur])
				{
					continue;
				}
			}
			scores[cur] = cur.score;

			if (map[cur.pos] == 'E')
			{
				if (cur.score <= result)
				{
					if (cur.score < result)
					{
						histories = [];
					}
					cur.visited.ToList().ForEach(x => histories.Add(x));
					result = cur.score;
					continue;
				}
			}

			TryEnqueue(cur.GetNext(Left));
			TryEnqueue(cur.GetNext(Right));
			TryEnqueue(cur.GetNext(Up));
			TryEnqueue(cur.GetNext(Down));

			void TryEnqueue(infoD16 next)
			{
				if (isOK(next))
				{
					var prio = Math.Abs(next.pos.Real - E.Real) + Math.Abs(next.pos.Imaginary - E.Imaginary);
					q.Enqueue(next, prio * next.score);
				}
			}
			bool isOK(infoD16 next) => next.score <= result && map[next.pos] != '#';
		}

		Console.WriteLine(result);
		Console.WriteLine(histories.Count);
	}

	class infoD16
	{
		public readonly IntComplex pos;
		public readonly IntComplex dir;
		public HashSet<IntComplex> visited = new(); 
		public int score = 0;

		private string id;
		private int hashCode;

		public infoD16(IntComplex pos, IntComplex dir)
		{
			this.pos = pos;
			this.dir = dir;

			id = $"x: {(int)pos.Real}, y: {(int)pos.Imaginary}, dir: {(int)dir.Real}x{(int)dir.Imaginary}";
			hashCode = id.GetHashCode();
		}

		public infoD16 GetNext(IntComplex newDir)
		{
			if (newDir == dir)
			{
				//same dir, we can move on
				var result = new infoD16(pos + newDir, newDir) { visited = visited.ToHashSet(), score = score + 1 };
				return result;
			}
			else if (newDir.Real == dir.Real || newDir.Imaginary == dir.Imaginary)
			{
				//180 degree
				var result = new infoD16(pos, newDir) { visited = visited.ToHashSet(), score = score + 2 * 1000 };
				return result;
			}
			//90 degree
			var next = new infoD16(pos, newDir) { visited = visited.ToHashSet(), score = score + 1000 };
			return next;
		}

		public override int GetHashCode() => hashCode;

		public override bool Equals(object obj) => obj is infoD16 other && other.GetHashCode() == hashCode;

		public override string ToString() => id;
	};
}

