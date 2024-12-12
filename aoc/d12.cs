class d12 : baseD
{
	public void Run()
	{
		var map = File.ReadAllLines(@"..\..\..\inputs\12.txt").Select(l => l.ToCharArray()).ToList();

        var regions = ReadRegions(map);

		var sum = 0;
        var sum2 = 0;
        foreach (var region in regions)
		{
			var perimiteres = TotalLengthOgEdges(region.visited, map);

			sum += perimiteres * region.visited.Count;

            var edges = GetNumberOfEdges(region.visited);
            sum2 += edges.Count() * region.visited.Count;
        }

        Console.WriteLine(sum);
        Console.WriteLine(sum2);
    }
}

