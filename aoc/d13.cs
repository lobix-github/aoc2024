class d13 : baseD
{
	public void Run()
	{
		//var extra = 0L;
		var extra = 10000000000000L;

		var lines = File.ReadAllLines(@"..\..\..\inputs\13.txt").ToList();

		var sum = 0L;
		for (int i = 0; i < lines.Count; i++)
        {
            var parts = lines[i].Split(' ');
            var ax = parts[2].Split('+')[1].TrimEnd(',').ToInt64();
            var ay = parts[3].Split('+')[1].ToInt64();

			parts = lines[i + 1].Split(' ');
			var bx = parts[2].Split('+')[1].TrimEnd(',').ToInt64();
			var by = parts[3].Split('+')[1].ToInt64();

			parts = lines[i + 2].Split(' ');
			var x = parts[1].Split('=')[1].TrimEnd(',').ToInt64() + extra;
			var y = parts[2].Split('=')[1].ToInt64() + extra;

			i += 3;

			long idxB = (ax * y - ay * x) / (ax * by - ay * bx);
			long t1 = (ax * y - ay * x) % (ax * by - ay * bx);
			long idxA = (y - by * idxB) / ay;
			long t2 = (y - by * idxB) % ay;

			if (t1 == 0 && t2 == 0)
			{
				sum += idxA * 3 + idxB;
			}
		}

		Console.WriteLine(sum);
	}
}

