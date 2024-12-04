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
	}

	int getAmount(string text) => AllIndexesOf(text, "XMAS").Length;
}


