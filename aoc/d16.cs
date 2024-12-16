using System.Numerics;

using Map = System.Collections.Generic.Dictionary<System.Numerics.Complex, char>;

class d16 : baseD
{
	static Dictionary<Complex, int> scores = [];
	
	public void Run()
	{
        var lines = File.ReadAllLines(@"..\..\..\inputs\16.txt");
        var map = (
            from y in Enumerable.Range(0, lines.Length)
            from x in Enumerable.Range(0, lines[0].Length)
            select new KeyValuePair<Complex, char>(x + y * Down, lines[y][x])
        ).ToDictionary();
		var S = map.Keys.Single(k => map[k] == 'S');
		var E = map.Keys.Single(k => map[k] == 'E');
		var start = new infoD16(S, Right);
		var q = new PriorityQueue<infoD16, double>();
		q.Enqueue(start, Math.Abs(S.Real - E.Real) + Math.Abs(S.Imaginary - E.Imaginary));
		scores[S] = start.score;

		var result = int.MaxValue;
		while (q.Count > 0)
		{
			var cur = q.Dequeue();
			cur.visited.Add(cur);

			if (map[cur.pos] == 'E')
			{
				Console.WriteLine($"new result found: {result}");
				result = Math.Min(result, cur.score);
				continue;
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
			bool isOK(infoD16 next) => next.score < result && map[next.pos] != '#' && next.score <= scores[next.pos] && !next.visited.Contains(next);
		}

		Console.WriteLine(result);
	}

	class infoD16
	{
		public readonly Complex pos;
		public readonly Complex dir; 
		public HashSet<infoD16> visited = new();
		public int score = 0;

		public infoD16(Complex pos, Complex dir)
		{
			this.pos = pos;
			this.dir = dir;
		}

		public infoD16 GetNext(Complex newDir)
		{
			var newPos = pos + newDir;
			var newScore = score + 1 + (newDir == dir ? 0 : 1000);
			scores.TryAdd(newPos, newScore);
			return new infoD16(newPos, newDir) { visited = visited.ToHashSet(), score = newScore };
		}

		private string id => $"x: {(int)pos.Real}, y: {(int)pos.Imaginary}";

		public override int GetHashCode() => id.ToString().GetHashCode();

		public override bool Equals(object obj) => obj is infoD16 other && other.GetHashCode() == this.GetHashCode();

		public override string ToString() => $"{id}, dir: {dir.ToString()}";
	};
}

