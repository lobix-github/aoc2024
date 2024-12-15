class d15 : baseD
{
	public void Run()
	{
        var boxes = new HashSet<CPoint>();
        var wall = new HashSet<CPoint>();
        CPoint cur = default;
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
                    if (line[j] == 'O') boxes.Add(new CPoint(j, i));
                    if (line[j] == '#') wall.Add(new CPoint(j, i));
                    if (line[j] == '@') cur = new CPoint(j, i);
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
            if (wall.Contains(newCur)) continue;
            if (!boxes.Contains(newCur))
            {
                cur = newCur;
                continue;
            }

            //we have a box
            cur = tryMoveBoxes(newCur, move);
        }

        var sum = boxes.Aggregate(0, (acc, p) => acc += p.X + 100 * p.Y);
        Console.WriteLine(sum);

        CPoint tryMoveBoxes(CPoint p, char move)
        {
            var delta = move switch
            {
                '>' => new CPoint(1, 0),
                '<' => new CPoint(-1, 0),
                '^' => new CPoint(0, -1),
                'v' => new CPoint(0, 1),
            };

            var newP = new CPoint(p.X, p.Y);
            while (true)
            {
                newP = new CPoint(newP.X + delta.X, newP.Y + delta.Y);
                if (wall.Contains(newP)) break;
                if (boxes.Contains(newP)) continue; 
                //free space
                boxes.Remove(p);
                boxes.Add(newP);
                return p;
            }

            return new CPoint(p.X - delta.X, p.Y - delta.Y); ;
        }

        CPoint getNext(char move) => move switch
        {
            '>' => new CPoint(cur.X + 1, cur.Y),
            '<' => new CPoint(cur.X - 1, cur.Y),
            '^' => new CPoint(cur.X, cur.Y - 1),
            'v' => new CPoint(cur.X, cur.Y + 1),
        };
    }
}

