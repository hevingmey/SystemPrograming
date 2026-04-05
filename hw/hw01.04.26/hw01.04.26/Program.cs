using System;
using System.Threading;

class Program
{
    static Semaphore semaphore = new Semaphore(1, 1);

    static void Work(object id)
    {
        bool isEnter = false;

        Console.WriteLine($"Thread {id} чекає...");

        try
        {
            isEnter = semaphore.WaitOne(1000);

            if (isEnter)
            {
                Console.WriteLine($"Thread {id} зайшов");
                Thread.Sleep(2000);
            }
        }
        finally
        {
           

            if (isEnter)
            {
                Console.WriteLine($"Thread {id} виходить");
                semaphore.Release();
            }
        }
    }

    static void Main()
    {
        Thread t1 = new Thread(Work);
        Thread t2 = new Thread(Work);
        Thread t3 = new Thread(Work);

        t1.Start(1);
        t2.Start(2);
        t3.Start(3);

        t1.Join();
        t2.Join();
        t3.Join();
    }
}
//waitOne може повернути фолз. Якщо після цього викликати release виникає Semaphorefullexception бо потік не заходив у семафор