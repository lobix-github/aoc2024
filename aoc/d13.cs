class d13 : baseD
{
    const int WIDTH = 101;
    const int HEIGHT = 103;

	public void Run()
	{
        var robots = new List<Robot>();
        var lines = File.ReadAllLines(@"..\..\..\inputs\13.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var parts = line.Split(' ');
            var coords = parts[0].Split('=')[1];
            var coordX = coords.Split(',')[0].ToInt32();
            var coordY = coords.Split(',')[1].ToInt32();
            var v = parts[1].Split('=')[1];
            var vX = v.Split(',')[0].ToInt32();
            var vY = v.Split(',')[1].ToInt32();

            var robot = new Robot(coordX, coordY, vX, vY, i);
            robots.Add(robot);
        }

        var result = new List<Robot>();
        foreach (var robot in robots)
        {
            result.Add(robot.Move(100));
        }
        var q1 = result.Where(r => r.X < WIDTH / 2 && r.Y < HEIGHT / 2).Count();
        var q2 = result.Where(r => r.X > WIDTH / 2 && r.Y < HEIGHT / 2).Count();
        var q3 = result.Where(r => r.X < WIDTH / 2 && r.Y > HEIGHT / 2).Count();
        var q4 = result.Where(r => r.X > WIDTH / 2 && r.Y > HEIGHT / 2).Count();

        var sum = q1 * q2 * q3 * q4;

        Console.WriteLine(sum);
    }

    class Robot : CPoint
    {
        private readonly int moveX;
        private readonly int moveY;
        public int _id;

        public Robot(int X, int Y, int moveX, int moveY, int id) : base(X, Y)
        {
            this.moveX = moveX;
            this.moveY = moveY;
            _id = id;
        }

        public override string id => $"{base.id}, id: {_id}";

        public Robot Move(int seconds)
        {
            var cur = new CPoint(X, Y);
            loopCycle(seconds, i =>
            {
                var newX = (X + moveX * i + WIDTH * i) % WIDTH;
                var newY = (Y + moveY * i + HEIGHT * i) % HEIGHT;
                cur = new CPoint(newX, newY);
                return cur;
            });

            return new Robot(cur.X, cur.Y, moveX, moveY, _id);
        }
    };
}

