using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace ConsoleApp1;



class Program
{

   // // static void Counter()
   //  {
   //      for (int i = 0; i < 10; i++)
   //      {
   //          mutex.WaitOne();
   //         try{ number++;}
   //         finally{mutex.ReleaseMutex();}
   //
   //         Console.WriteLine(number);
   //      }
   //                      Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
      static Mutex mutex=new Mutex(false,"GlobalMyLogFileMutex");

    //}
    static void Main(string[] args)
    {   
        string path="logfile.txt";
        for (int i = 0; i < 3; i++)
        {
            mutex.WaitOne();
            try
            {
                File.AppendAllText(path,"pid= "+Environment.ProcessId+"час: "+DateTime.Now+"sentence "+i+"\n");
            }
            finally{
                mutex.ReleaseMutex();}
            Thread.Sleep(1000);
        }

        Console.WriteLine("completed ");
        
        
       
        
        
        // List<int> numbers = new();
        // var tasks = new List<Task>();
        //
        // for (int i = 0; i < 10; i++)
        // {
        //     tasks.Add(Task.Run(() =>
        //     {
        //         for (int j = 0; j < 1000; j++)
        //         {
        //             numbers.Add(j); //  Небезпечно: одночасний доступ до List<T>
        //         }
        //     }));
        // }
        //
        // Task.WaitAll(tasks.ToArray());
        //
        // Console.WriteLine($"Кількість елементів: {numbers.Count}");
        
        
        // TestForeach.Run();
        // TestForeach.RunParallel();
        // string[] URLSimg =
        // {
        //    " https://se-cdn.djiits.com/tpc/uploads/photo/image/43f27177fcd87ce27fca5daa7a06f0cd@large.jpg",              
        //    "https://se-cdn.djiits.com/tpc/uploads/photo/image/43f27177fcd87ce27fca5daa7a06f0cd@large.jpg",
        //    "https://se-cdn.djiits.com/tpc/uploads/photo/image/e28a9335e35068a905ae561b9b0aeaa5@large.jpg",
        //    "https://se-cdn.djiits.com/tpc/uploads/carousel/image/560f1b92810c859a4ab02fdf9195d052@origin.jpg?format=webp",
        //    "https://se-cdn.djiits.com/tpc/uploads/carousel/image/d4a1adddd5c1ebe61cbd4b5b54107345@origin.jpg?format=webp",
        //    "https://se-cdn.djiits.com/tpc/uploads/carousel/image/0c5b58bc67875b48e88ffca5f0299e6f@origin.jpg",
        //    "https://se-cdn.djiits.com/tpc/uploads/carousel/image/7672c4c9935f59cc33fa4e92cb5dc7f2@origin.jpg?format=webp",
        //    "https://se-cdn.djiits.com/tpc/uploads/carousel/image/1c832f9e1bb82fb5df4b0c2281d71412@ultra.webp",
        //    "https://se-cdn.djiits.com/tpc/uploads/carousel/image/6566ef8ca9ef7a0508559a6120d6134f@ultra.webp",
        //    "https://se-cdn.djiits.com/tpc/uploads/carousel/image/a99bc67b150f07c44d535a5493feab6d@ultra.webp"
        //     
        // };
        //
        // using HttpClient client = new HttpClient();
        //
        // int counter = 20;
        // Stopwatch stopwatch = Stopwatch.StartNew();
        //
        // Parallel.ForEach(URLSimg, url =>
        // {
        //     var data = client.GetByteArrayAsync(url).Result;
        //     File.WriteAllBytes($"picture{counter}.jpg", data);
        //
        //     counter++;
        // });
        //
        // stopwatch.Stop();
        //
        // Console.WriteLine($" {stopwatch.ElapsedMilliseconds}mls");
    }
}