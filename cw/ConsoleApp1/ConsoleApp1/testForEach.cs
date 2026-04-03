using System.Diagnostics;

namespace ConsoleApp1;


internal class TestForeach
{
    private static long _sum = 0;
    private static List<int> _list =new List<int>();
    private static Random _random = new Random();
    private static object _locker = new object();
    public static void Run()
    {
        FillList();
        Stopwatch sw = Stopwatch.StartNew();
        CalculateSumOneThread();
        Console.WriteLine($"Sum {_sum} Time: {sw.ElapsedMilliseconds} ms");
        _list.Clear();
    }

    public static void RunParallel()
    {
        FillList();
        Stopwatch sw = Stopwatch.StartNew();
        CalculateSumManyThread();
        Console.WriteLine($"Sum {_sum} Time: {sw.ElapsedMilliseconds} ms");
        _list.Clear();
    }

    private static void CalculateSumOneThread()
    {
        foreach (var item in _list)
        {
            _sum += item;
        }
    }
    private static void CalculateSumManyThread()
    {
        Parallel.ForEach(_list, item =>
        {
            lock(_locker)
            {
                _sum += item;
            }

        });
    }
    // паралель foreach дозволяє робити в окремих потоках
    // потоко безпечні колекції

    private static void FillList()
    {
        for(int i=0;i<100_000;i++)
        {
            _list.Add(_random.Next(1,10));
        }
    }


}
