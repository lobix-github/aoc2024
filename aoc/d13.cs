class d13 : baseD
{
	public void Run()
	{
		var extra = 0L;
		//var extra = 10000000000000L;


		var lines = File.ReadAllLines(@"..\..\..\inputs\13.txt").ToList();

		var sum = 0;
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

			for (int idxA = 0; idxA < 100; idxA++)
			{
				for (int idxB = 0; idxB < 100; idxB++)
				{
					if ((idxA * ax + idxB * bx == x) && (idxA * ay + idxB * by == y))
					{
						sum += idxA * 3 + idxB;
					}
				}
			}
		}


		Console.WriteLine(sum);
	}
}

