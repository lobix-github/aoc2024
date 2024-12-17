using System.Collections.Concurrent;

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
        for (int i = 0; i < program.Count; i++)
        {
            maybes.Add(new());
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
			}
		}

        long counter = 0;
        ConcurrentBag<long> wins = new();
        int im0 = 0;
        Parallel.ForEach(maybes[0], m0 =>
        //foreach (var m0 in maybes[0])
        {
            Console.WriteLine($"starting 0: {im0}");

			int im1 = 0;
			Parallel.ForEach(maybes[1], m1 =>
            //foreach (var m1 in maybes[1])
            {
                Console.WriteLine($"starting 0/1: {im0}/{im1}");

			    int im2 = 0;
				Parallel.ForEach(maybes[2], m2 =>
                //foreach (var m2 in maybes[2])
                {
                    Console.WriteLine($"starting 0/1/2: {im0}/{im1}/{im2}");

			        int im3 = 0;
                    Parallel.ForEach(maybes[3], m3 =>
                    //foreach (var m3 in maybes[3])
                    {
                        Console.WriteLine($"starting 0/1/2/3: {im0}/{im1}/{im2}/{im3}");

                        foreach (var m4 in maybes[4])
                        {
                            Console.WriteLine($"starting 4: {im0}/{im1}/{im2}/{im3}");
							foreach (var m5 in maybes[5])
                            {
                                Interlocked.Increment(ref counter);
								Console.WriteLine($"starting 5: {im0}/{im1}/{im2}/{im3} ({counter * 100 / Math.Pow(8, 6)}%)");
								foreach (var m6 in maybes[6])
                                {
									foreach (var m7 in maybes[7])
                                    {
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
                                                                                i++;
                                                                                if (i == program.Count)
                                                                                {
                                                                                    wins.Add(a);
                                                                                    Console.WriteLine("łuhu!");
                                                                                }
                                                                                a = a >> 3;
                                                                                continue;
                                                                            }
                                                                            break;
                                                                        }

                                                                        //interloc
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
											}
										}
									}
								}
                                Console.WriteLine($"finished 5: {im0}/{im1}/{im2}/{im3}");
							}
                            Console.WriteLine($"finished 4: {im0}/{im1}/{im2}/{im3}");
						}
						Console.WriteLine($"finished 0/1/2/3: {im0}/{im1}/{im2}/{im3++}");
					}
					);
					Console.WriteLine($"finished 0/1/2: {im0}/{im1}/{im2++}");
				}
				);
                Console.WriteLine($"finished 0/1: {im0}/{im1++}");
			}
			);
            Console.WriteLine($"finished 0: {im0++}");
		}
		);

		int ii = 0;
        //      long b = 0;
        //      long c = 0;
        //      while (i < program.Count)
        //      {
        //          b = (a % 8) ^ 5;
        //          c = a >> (byte)b;
        //          b = b ^ 6;
        //          b = b ^ c;
        //          var r = b % 8;
        //          if (r == program[i])
        //          {
        //              i++;
        //              a = a >> 3;
        //          }
        //      }



        //504094726375448 too high
        //Console.WriteLine(res);

        //Parallel.For(int.MinValue, int.MaxValue, x => 
        //      {
        //	var comp = new compD17(x, B, C);
        //	if (string.Join(',', comp.Run(program)) == programString)
        //          {
        //		Console.WriteLine(x);
        //	};
        //});
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
