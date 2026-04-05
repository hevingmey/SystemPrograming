using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;

namespace hw30._03._26;

class User
{
    public string Name { get; set; }
    public string Reson { get; set; }

}

class Program
{
    
    static ConcurrentQueue<User> users = new ConcurrentQueue<User>();
    static bool Iswork = true;


    static void AddUser()
    { 
        for (int i = 1; i < 7; i++)
        {
            User user  = new User();    
            user.Name = $"User{i}";
            user.Reson = $"Reson{i}";
            users.Enqueue(user);
            Console.WriteLine($"user: {user.Name}");
            Thread.Sleep(1500);
        }
    }

    static void StUser()
    {
        while (Iswork||users.Count>0)
        {
            User user;
            if (users.TryDequeue(out user))
            {
                Console.WriteLine($"shopping: {user.Name}-{user.Reson}");
                Thread.Sleep(1000);
            }
        }

        Console.WriteLine("completed");
    }
    
    static void Main(string[] args)
    {
        Thread userThread = new Thread(AddUser);
        Thread work = new Thread(StUser);
        
        work.Start();
        userThread.Start();

        userThread.Join();
        
        Iswork=false;
        work.Join();
        



    }
}