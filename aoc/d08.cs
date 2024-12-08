class d08
{
    public void Run()
    {
        var antinodes = new HashSet<DPoint>();
        var antennas = new Dictionary<char, List<DPoint>>();

        var map = File.ReadLines(@"..\..\..\inputs\08.txt").Select(l => l.ToCharArray()).ToList();
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                var c = map[y][x];
                if (c != '.')
                {
                    if (!antennas.ContainsKey(c)) antennas[c] = new List<DPoint>();
                    antennas[c].Add(new DPoint(x, y));
                }
            }
        }

        foreach (var antennaType in antennas)
        {
            for(int i = 0; i < antennaType.Value.Count; i++)
            { 
                var antenna = antennaType.Value[i];
                var rest = antennaType.Value.Except([antenna]);
                foreach (var anotherAntenna in rest)
                {
                    foreach (var antinode in calcAntinodes(antenna, anotherAntenna))
                    {
                        antinodes.Add(antinode);
                    }
                }
            }
        }

        var result = antinodes.Count;
        Console.WriteLine(result);

        IEnumerable<DPoint> calcAntinodes(DPoint a1, DPoint a2)
        {
            var deltaX = Math.Abs(a1.X - a2.X);
            var minX = Math.Min(a1.X, a2.X) - deltaX;
            var maxX = Math.Max(a1.X, a2.X) + deltaX;

            var deltaY = Math.Abs(a1.Y - a2.Y);
            var minY = Math.Min(a1.Y, a2.Y) - deltaY;
            var maxY = Math.Max(a1.Y, a2.Y) + deltaY;

            if (a1.Y < a2.Y)
            {
                if (a1.X < a2.X)
                {
                    var p = new DPoint(minX, minY);
                    if (isInMap(p)) yield return p;
                    p = new DPoint(maxX, maxY);
                    if (isInMap(p)) yield return p;
                }
                else
                {
                    var p = new DPoint(maxX, minY);
                    if (isInMap(p)) yield return p;
                    p = new DPoint(minX, maxY);
                    if (isInMap(p)) yield return p;
                }
            }
            else
            {
                if (a1.X < a2.X)
                {
                    var p = new DPoint(minX, maxY);
                    if (isInMap(p)) yield return p;
                    p = new DPoint(maxX, minY);
                    if (isInMap(p)) yield return p;
                }
                else
                {
                    var p = new DPoint(maxX, maxY);
                    if (isInMap(p)) yield return p;
                    p = new DPoint(minX, minY);
                    if (isInMap(p)) yield return p;
                }
            }
        }

		bool isInMap(DPoint p) => p.Y >= 0 && p.Y < map.Count && p.X >= 0 && p.X < map[0].Length;
    }
}
