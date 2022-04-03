using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var time_1 = DateTimeOffset.UtcNow;
            var time_2 = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var time_3 = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            Console.WriteLine("time_1 {0}", time_1);
            Console.WriteLine("time_2 {0}", time_2);
            Console.WriteLine("time_3 {0}", time_3);

        }
    }
}
