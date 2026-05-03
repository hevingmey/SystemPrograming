using System.Diagnostics;

namespace dndGame;


record Hero(string Name, int Health, int Power);

record Quest(string Title, int Level, int Gold, TimeSpan Time);

record Result(
    Hero Hero,
    Quest Quest,
    bool Win,
    bool Cancel,
    int Gold
);

class Program
{
    private static readonly Stopwatch Timer = Stopwatch.StartNew();

    static async Task Main(string[] args)
    {
        Hero bob = new Hero("Bob", 100, 45);
        Hero alice = new Hero("Alicce", 80, 70);

        Quest cave = new Quest(
            "Dragon cave",
            80,
            300,
            TimeSpan.FromSeconds(5)
        );

        Quest tower = new Quest(
            "mage tower",
            60,
            150,
            TimeSpan.FromSeconds(1.2)
        );

        List<Task<Result>> tasks = new List<Task<Result>>
        {
            StartAsync(bob, cave),
            StartAsync(alice, tower)
        };

        List<Task<Result>> active = new List<Task<Result>>(tasks);

        while (active.Count > 0)
        {
            Task<Result> done = await Task.WhenAny(active);

            Result result = await done;

            Show(result);

            active.Remove(done);
        }

        await Task.WhenAll(tasks);
    }

    static async Task<Result> StartAsync(Hero hero, Quest quest)
    {
        using CancellationTokenSource cts = new CancellationTokenSource(
            TimeSpan.FromSeconds(3)
        );

        return await RunAsync(hero, quest, cts.Token);
    }

    static async Task<Result> RunAsync(
        Hero hero,
        Quest quest,
        CancellationToken ct
    )
    {
        Log($"{hero.Name} started \"{quest.Title}\"");

        try
        {
            await Task.Delay(quest.Time, ct);

            int random = Random.Shared.Next(-20, 21);
            int power = hero.Power + random;

            bool win = power >= quest.Level;

            int gold = win ? quest.Gold : 0;

            return new Result(hero, quest, win, false, gold);
        }
        catch (OperationCanceledException)
        {
            return new Result(hero, quest, false, true, 0);
        }
    }

    static void Show(Result result)
    {
        if (result.Cancel)
        {
            Log($"{result.Hero.Name} retreated from \"{result.Quest.Title}\" (timeout)");
            return;
        }

        if (result.Win)
        {
            Log($"{result.Hero.Name} finished \"{result.Quest.Title}\" — won (+{result.Gold} gold)");
        }
        else
        {
            Log($"{result.Hero.Name} finished \"{result.Quest.Title}\" — DEfeat");
        }
    }

    static void Log(string text)
    {
        Console.WriteLine($"[{Timer.Elapsed:mm\\:ss\\.fff}] {text}");
    }
}