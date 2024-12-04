using System.Text;
using System.Text.RegularExpressions;

class d04 : baseD
{
    public void Run()
    {
        var lines = File.ReadAllLines(@"..\..\..\inputs\04.txt").ToList();
        var lines2 = File.ReadAllLines(@"..\..\..\inputs\04.txt").ToList();

        var sum = 0;
		//horizontal back and forth
		foreach (var line in lines)
        {
            sum += getAmount(line);
            sum += getAmount(Reverse(line));
        }

		//vertical back and forth
		lines = rotate(lines);
		foreach (var line in lines)
		{
			sum += getAmount(line);
            sum += getAmount(Reverse(line));
		}

		//allign to left and flip
		lines = rotate(lines);
		for (var i = 0; i < lines.Count; i++)
        {
            var line = new StringBuilder(lines[i]);
            for (var j = 0; j < line.Length; j++)
            {
                line[j] = lines[i][(j + i) % line.Length];
            }
            lines2[i] = line.ToString();
        }

        lines2 = rotate(lines2);
		foreach (var line in lines2.Skip(1))
		{
			sum += getAmount(line);
			sum += getAmount(Reverse(line));
		}

		//allign to right and flip
		for (var i = 0; i < lines.Count; i++)
		{
			var line = new StringBuilder(lines[i]);
			for (var j = line.Length - 1; j >= 0; j--)
			{
				line[j] = lines[i][(j - i + line.Length) % line.Length];
			}
			lines2[i] = line.ToString();
		}
		lines2 = rotate(lines2);
		foreach (var line in lines2.Skip(1))
		{
			sum += getAmount(line);
			sum += getAmount(Reverse(line));
		}

		Console.WriteLine(sum);

		//part 2
		sum = 0;
		var l = new List<char[]>();
		foreach (var line in lines) l.Add(line.ToCharArray());
		for (int y = 1; y < lines.Count - 1; y++)
		{
			for (int x = 1; x < lines[y].Length - 1; x++)
			{
				sum += checkXMAS(x, y);
			}
		}

        Console.WriteLine(sum);

        int checkXMAS(int x, int y)
		{
			if (l[y][x] == 'A')
			{
				if (axis1_1(x, y) && (axis2_1(x, y) || axis2_2(x, y))) return 1;
				if (axis1_2(x, y) && (axis2_1(x, y) || axis2_2(x, y))) return 1;
				
				if (axis2_1(x, y) && (axis1_1(x, y) || axis1_2(x, y))) return 1;
				if (axis2_2(x, y) && (axis1_1(x, y) || axis1_2(x, y))) return 1;
            }

			bool axis1_1(int x, int y) => l[y - 1][x - 1] == 'M' && l[y + 1][x + 1] == 'S';
			bool axis1_2(int x, int y) => l[y - 1][x - 1] == 'S' && l[y + 1][x + 1] == 'M';
            bool axis2_1(int x, int y) => l[y - 1][x + 1] == 'S' && l[y + 1][x - 1] == 'M';
			bool axis2_2(int x, int y) => l[y - 1][x + 1] == 'M' && l[y + 1][x - 1] == 'S';

            return 0;
		}
    }

	int getAmount(string text) => AllIndexesOf(text, "XMAS").Length;
}


