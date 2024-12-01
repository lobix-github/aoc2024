class d01
{
    public void Run()
    {
        List<int> list1 = new();
        List<int> list2 = new();

        var lines = File.ReadLines(@"..\..\..\inputs\01.txt").ToList();
        foreach (var line in lines)
        {
            list1.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0].ToInt32());
            list2.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1].ToInt32());
        }

        list1.Sort();
        list2.Sort();

        var result1 = list1.Zip(list2, (a, b) => Math.Abs(a - b)).Aggregate(0, (res, x) => res += x);
        Console.WriteLine(result1);

        var result2 = list1.Zip(list2, (a, b) => a * list2.Count(x => x == a)).Aggregate(0, (res, x) => res += x);
        Console.WriteLine(result2);
    }
}
