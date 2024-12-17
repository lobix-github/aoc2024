class d17
{
    public void Run()
    {
        List<int> list1 = new();

        var lines = File.ReadAllText(@"..\..\..\inputs\17.txt");
        var blocks = lines.Split($"{Environment.NewLine}{Environment.NewLine}");
        var A = blocks[0].Split(Environment.NewLine)[0].Split(' ')[2].ToInt32();
        var B = blocks[0].Split(Environment.NewLine)[1].Split(' ')[2].ToInt32();
		var C = blocks[0].Split(Environment.NewLine)[2].Split(' ')[2].ToInt32();
        var program = blocks[1].Split(' ')[1].Split(',').Select(x => x.ToInt32()).ToList();

        var computer = new compD17(A, B, C);

        var result = string.Join(',', computer.Run(program));
        Console.WriteLine(result);
    }

    class compD17
    {
        private int A;
        private int B;
        private int C;

        private int counter = 0;

        public compD17(int A, int B, int C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public IEnumerable<int> Run(List<int> program)
        {
            while (counter < program.Count)
            {
                var instr = program[counter];
                var op = program[counter + 1];

                if (instr != 5) Execute(instr, op);
				else yield return Output(instr, op);

				if (instr == 3 && A != 0)
                {
                    // jnz
                    counter = op - 2;
                }

                counter += 2;
            }
        }

        private int Output(int instr, int op)
        {
            if (instr != 5) throw new ArgumentException();
            return resolveOperand(op) % 8;
		}


		private void Execute(int instr, int op)
        {
			switch (instr)
            {
                case 0: A = (int)Math.Truncate(A / Math.Pow(2, resolveOperand(op))); return;
				case 1: B = B ^ op; return;
				case 2: B = resolveOperand(op) % 8; return;
				case 3 when A != 0: counter = op - 2; return;
                case 4: B = B ^ C; return;
                case 5: throw new ArgumentException();
				case 6: B = (int)Math.Truncate(A / Math.Pow(2, resolveOperand(op))); return;
				case 7: C = (int)Math.Truncate(A / Math.Pow(2, resolveOperand(op))); return;
			}
	    }

	    private int resolveOperand(int op) => op switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => A,
            5 => B,
            6 => C,
			7 => throw new ArgumentException(),
		};
    }
}
