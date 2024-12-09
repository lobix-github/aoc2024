using System.Numerics;

class d09
{
    public void Run()
    {
        var input = File.ReadAllText(@"..\..\..\inputs\09.txt");
        var list = new List<infoD09>();
        var empties = new List<int>();

        int id = 0;
        int idx = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            for (int j = 0; j < c - '0'; j++)
            {
				if (i % 2 == 0)
				{
                    list.Add(new infoD09(id, idx));
				}
                else
                {
                    list.Add(new infoD09(-1, idx));
                    empties.Add(idx);
				}
                idx++;
			}
			if (i % 2 == 0)
			{
                id++;
			}
		}

        while (empties.Any())
        {
            if (list.Skip(empties.First()).Any(x => x.id != -1))
            {
				var last = list.Last(x => x.id != -1);
				var lastIdx = list.IndexOf(last);
				list[empties.First()] = last;
				list[lastIdx] = last with { id = -1 };
				empties.RemoveAt(0);
			}
            else
            {
                break;
            }
		}

        long sum = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].id == -1) break;
            sum += i * list[i].id;
        }

		Console.WriteLine(sum);
	}

    record infoD09(int id, int idx);
}
