using System.Collections.Concurrent;
using System.Security.Cryptography;

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
        var programString = blocks[1].Split(' ')[1];
		var program = programString.Split(',').Select(x => x.ToInt32()).ToList();

        //var computer = new compD17(A, B, C);
        //var result = string.Join(',', computer.Run(program));
        //Console.WriteLine(result);

        List<List<long>> maybes = new List<List<long>>();
        List<Dictionary<long, int>> shifts = [];
        for (int i = 0; i < program.Count; i++)
        {
            maybes.Add([]);
            shifts.Add([]);
			for (int aa = 0; aa < 8; aa++)
			{
				var bb = aa ^ 5;
				var shift = bb;
				bb = bb ^ 6;
				var cc = bb ^ program[i];
				long a = aa;
				a |= cc << shift;

				var aShifted = a << (3 * i);
                maybes[i].Add(aShifted);
                shifts[i][aShifted] = shift + (3 * i);
			}
		}

        ConcurrentBag<long> wins = new();
        long count = 0;
        Parallel.ForEach(maybes[0], (m0, state, id) =>
        //foreach (var m0 in maybes[0])
        {
            Console.WriteLine($"0: {id}");
            int im1 = 0;
            foreach (var m1 in maybes[1])
            {
                Console.WriteLine($"1: {im1++}");
                int im2 = 0;
                foreach (var m2 in maybes[2])
                {
                    Console.WriteLine($"2: {im2++}");
                    int im3 = 0;
                    foreach (var m3 in maybes[3])
                    {
                        Console.WriteLine($"3: {im3++}");
                        int im4 = 0;
                        foreach (var m4 in maybes[4])
                        {
                            Console.WriteLine($"4: {im4++}");
                            int im5 = 0;
                            foreach (var m5 in maybes[5])
                            {
                                Console.WriteLine($"5: {im5++}");
                                int im6 = 0;
                                foreach (var m6 in maybes[6])
                                {
                                    Console.WriteLine($"6: {im6++}");
                                    int im7 = 0;
                                    foreach (var m7 in maybes[7])
                                    {
                                        Console.WriteLine($"0/1/2/3/4/5/6/7: {id}/{im1}/{im2}/{im3}/{im4}/{im5}/{im6}/{im7++}");
                                        foreach (var m8 in maybes[8])
                                        {
                                            foreach (var m9 in maybes[9])
                                            {
                                                foreach (var m10 in maybes[10])
                                                {
                                                    foreach (var m11 in maybes[11])
                                                    {
                                                        foreach (var m12 in maybes[12])
                                                        {
                                                            foreach (var m13 in maybes[13])
                                                            {
                                                                foreach (var m14 in maybes[14])
                                                                {
                                                                    foreach (var m15 in maybes[15])
                                                                    {
                                                                        count++;

                                                                        int i = 0;
                                                                        long a = m0 | m1 | m2 | m3 | m4 | m5 | m6 | m7 | m8 | m9 | m10 | m11 | m12 | m13 | m14 | m15;
                                                                        long b = 0;
                                                                        long c = 0;
                                                                        while (i < program.Count)
                                                                        {
                                                                            b = (a % 8) ^ 5;
                                                                            c = a >> (byte)b;
                                                                            b = b ^ 6;
                                                                            b = b ^ c;
                                                                            var r = b % 8;
                                                                            if (r == program[i])
                                                                            {
                                                                                if (i++ == 15)
                                                                                {
                                                                                    Console.WriteLine("łuhu!");
                                                                                    wins.Add(a);
                                                                                }
                                                                                a = a >> 3;
                                                                                continue;
                                                                            }
                                                                            if (i > 0)
                                                                            {
                                                                                //// last good position was i - 1
                                                                                var idx = getM(i - 1);
                                                                                if (idx == -1) break;
                                                                                var shift = shifts[i - 1][idx];
                                                                                if (shift < 3) goto continue_m0;
                                                                                if (shift < 6) goto continue_m1;
                                                                                if (shift < 9) goto continue_m2;
                                                                                if (shift < 12) goto continue_m3;
                                                                                if (shift < 15) goto continue_m4;
                                                                                if (shift < 18) goto continue_m5;
                                                                                if (shift < 21) goto continue_m6;
                                                                                if (shift < 24) goto continue_m7;
                                                                                if (shift < 27) goto continue_m8;
                                                                                if (shift < 30) goto continue_m9;
                                                                                if (shift < 33) goto continue_m10;
                                                                                if (shift < 36) goto continue_m11;
                                                                                if (shift < 39) goto continue_m12;
                                                                                if (shift < 42) goto continue_m13;
                                                                                if (shift < 45) goto continue_m14;
                                                                            }

                                                                            break;
                                                                        }
                                                                        long getM(int idx) => idx switch
                                                                        {
                                                                            0 => m0,
                                                                            1 => m1,
                                                                            2 => m2,
                                                                            3 => m3,
                                                                            4 => m4,
                                                                            5 => m5,
                                                                            6 => m6,
                                                                            7 => m7,
                                                                            8 => m8,
                                                                            9 => m9,
                                                                            10 => m10,
                                                                            11 => m11,
                                                                            12 => m12,
                                                                            13 => m13,
                                                                            14 => m14,
                                                                            _ => -1,
                                                                        };
                                                                    }
                                                                    continue_m14:;
                                                                }
                                                                continue_m13:;
                                                            }
                                                            continue_m12:;
                                                        }
                                                        continue_m11:;
                                                    }
                                                    continue_m10:;
                                                }
                                                continue_m9:;
                                            }
                                            continue_m8:;
                                        }
                                        continue_m7:;
                                    }
                                    continue_m6:;
                                }
                                continue_m5:;
                            }
                            continue_m4:;
                        }
                        continue_m3:;
                    }
                    continue_m2:;
                }
                continue_m1:;
            }
            continue_m0:;
        }
        );

        //504094726375448 too high
        //Console.WriteLine(res);
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
