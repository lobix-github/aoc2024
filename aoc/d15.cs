using System.Collections.Immutable;
using System.Numerics;

using Map = System.Collections.Immutable.IImmutableDictionary<System.Numerics.Complex, char>;

class d15 : baseD
{
    //static Complex Up = -Complex.ImaginaryOne;
    //static Complex Down = Complex.ImaginaryOne;
    //static Complex Left = -1;
    //static Complex Right = 1;

    //public void Solve(string input)
    //{
    //    var (map, steps) = Parse(input);
    //    var robot = map.Keys.Single(k => map[k] == '@');
    //    foreach (var dir in steps)
    //    {
    //        if (map[robot] == 'X')
    //        {
    //            robot += dir;
    //        }
    //    }
    //}

    //(Map, Complex[]) Parse(string input)
    //{
    //    var blocks = input.Split("\n\n");
    //    var lines = blocks[0].Split("\n");
    //    var map = (
    //        from y in Enumerable.Range(0, lines.Length)
    //        from x in Enumerable.Range(0, lines[0].Length)
    //        select new KeyValuePair<Complex, char>(x + y * Down, lines[y][x])
    //    ).ToImmutableDictionary();

    //    var steps = blocks[1].ReplaceLineEndings("").Select(ch =>
    //        ch switch {
    //            '^' => Up,
    //            '<' => Left,
    //            '>' => Right,
    //            'v' => Down,
    //            _ => throw new Exception()
    //        });

    //    return (map, steps.ToArray());
    //}

    public void Run()
	{
        var boxes = new HashSet<box2>();
        var wall = new HashSet<box2>();
        box2 cur = default;
        var lines = File.ReadAllLines(@"..\..\..\inputs\15.txt");
        var moves = string.Empty;
        var map = new List<char[]>();
        var readMoves = false;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line == "")
            {
                readMoves = true;
                continue;
            }
            if (!readMoves)
            {
                map.Add(line.ToCharArray());
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == 'O') boxes.Add(new box2(j * 2, i) { right = new CPoint(j * 2 + 1, i) });
                    if (line[j] == '#') wall.Add(new box2(j * 2, i) { right = new CPoint(j * 2 + 1, i) });
                    if (line[j] == '@') cur = new box2(j * 2, i) { right = new CPoint(j * 2, i) };
                }
            }
            else
            {
                moves += line;
            }
        }

        for (int i = 0; i < moves.Length; i++)
        {
            var move = moves[i];
            var newCur = getNext(move);
            var maybBox = boxes.Where(b => b.Equals(newCur)).FirstOrDefault();
            if (wall.Any(x => x.Equals(newCur)) || (maybBox != default && wall.Any(x => x.Equals(maybBox)))) continue;
            if (!boxes.Any(x => x.Equals(newCur)))
            {
                cur = newCur;
                continue;
            }

            //we have a box
            cur = tryMoveBoxes(newCur, move) ?? cur;
        }

        var sum = boxes.Aggregate(0, (acc, p) => acc += p.X + 100 * p.Y);
        Console.WriteLine(sum);

        box2 tryMoveBoxes(box2 p, char move)
        {
            var delta = move switch
            {
                '>' => new CPoint(1, 0),
                '<' => new CPoint(-1, 0),
                '^' => new CPoint(0, -1),
                'v' => new CPoint(0, 1),
            };

            if (delta.X != 0) return moveH(p, delta.X);

            var movingBoxes = boxes.Where(x => x.Equals(p)).ToHashSet();
            var boxesToMove = new HashSet<box2>(movingBoxes);
            while (true)
            {
                movingBoxes = movingBoxes.Select(b => new box2(b.X, b.Y + delta.Y) { right = new CPoint(b.right.X, b.Y + delta.Y) }).ToHashSet();
                if (wall.Any(w => movingBoxes.Any(x => x.Equals(w)))) return null; 
                foreach (var b in movingBoxes)
                {
                    boxesToMove.Add(new box2(b.X, b.Y - delta.Y) { right = new CPoint(b.right.X, b.Y - delta.Y) });
                }

                movingBoxes = boxes.Where(b => movingBoxes.Any(x => x.Equals(b))).ToHashSet();
                if (!movingBoxes.Any()) break;
            }

            foreach (var boxToMove in boxesToMove)
            {
                boxes.Remove(boxToMove);
            }
            foreach (var boxToMove in boxesToMove)
            {
                boxes.Add(new box2(boxToMove.X, boxToMove.Y + delta.Y) { right = new CPoint(boxToMove.right.X, boxToMove.right.Y + delta.Y) });
            }
            return p;
        }

        box2 moveH(box2 p, int deltaX)
        {
            var boxesToMove = new HashSet<box2>();
            var newP = new box2(p.X, p.Y);
            while (true)
            {
                newP = new box2(newP.X + deltaX, p.Y);
                if (wall.Any(x => x.Equals(newP))) break;
                if (boxes.Any(x => x.Equals(newP)))
                {
                    boxesToMove.Add(boxes.First(x => x.Equals(newP)));
                    continue;
                };
                //free space
                foreach (var box in boxesToMove)
                {
                    boxes.Remove(box);
                    boxes.Add(new box2(box.X + deltaX, p.Y) { right = new CPoint(box.right.X + deltaX, p.Y) } );
                }
                return p;
            }

            return null;
        }

        box2 getNext(char move) => move switch
        {
            '>' => new box2(cur.X + 1, cur.Y) { right = new CPoint(cur.X + 1, cur.Y) },
            '<' => new box2(cur.X - 1, cur.Y) { right = new CPoint(cur.X - 1, cur.Y) },
            '^' => new box2(cur.X, cur.Y - 1) { right = new CPoint(cur.X, cur.Y - 1) },
            'v' => new box2(cur.X, cur.Y + 1) { right = new CPoint(cur.X, cur.Y + 1) },
        };
    }

    class box2 : CPoint
    {
        public CPoint right;

        public box2(int X, int Y) : base(X, Y) { }

        public CPoint other(int x) => X == x ? right : this;

        public override bool Equals(object obj) 
            => obj is box2 other
                && (other.X == X || other.X == right.X || other.right?.X == X || other.right?.X == right.X)
                && (other.Y == Y || other.Y == right.Y || other.right?.Y == Y || other.right?.Y == right.Y);
    }
}

