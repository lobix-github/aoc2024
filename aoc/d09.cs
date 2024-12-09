class d09
{
    public void Run()
    {
        var input = File.ReadAllText(@"..\..\..\inputs\09.txt");
        var list = new List<infoD09>();
        var empties = new List<int>();
        var blocks = new List<infoD09>();
        var emptyBlocks = new List<infoD09>();

        int id = 0;
        int idx = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            for (int j = 0; j < c - '0'; j++)
            {
				if (i % 2 == 0)
				{
					var info = new infoD09(id, idx, c - '0', false);
					list.Add(info);
					if (j == 0)
					{
						blocks.Add(info);
					}
				}
				else
                {
					var info = new infoD09(-1, idx, c - '0', false);

					list.Add(info);
                    empties.Add(idx);
					if (j == 0)
					{
						emptyBlocks.Add(info);
					}
				}
                idx++;
			}
			if (i % 2 == 0)
			{
                id++;
			}
		}

		part1();
		part2();

		long sum = 0;
        for (int i = 0; i < list.Count; i++)
        {
			var val = list[i].id;
			if (val != -1)
            {
				sum += i * val;
			}
		}

		Console.WriteLine(sum);

        void part1()
        {
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
		}

		void part2()
		{
			for (int i = blocks.Count - 1; i >= 0; i--)
			{
				var block = blocks[i];
				for (int j = 0; j < emptyBlocks.Count; j++)
				{
					var emptyBlock = emptyBlocks[j];
					if (emptyBlock.len >= block.len)
					{
						if (emptyBlock.idx < block.idx)
						{
							for (int idx = 0; idx < block.len; idx++)
							{
								list[emptyBlock.idx + idx] = block;
								list[block.idx + idx] = block with { id = -1 };
							}

							emptyBlocks[j] = emptyBlock with { idx = emptyBlock.idx + block.len, len = emptyBlock.len - block.len };
							break;
						}
					}
				}
			}
		}
	}

	record infoD09(int id, int idx, int len, bool moved);
}
