using StackExchange.Redis;
using System;

namespace RedisPubSub
{
    class Program
    {
        static void Main(string[] args)
        {
            var redis = ConnectionMultiplexer.Connect("localhost:5002");
            var sub = redis.GetSubscriber();
            string channel = "";
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1) Subscribe");
                Console.WriteLine("2) Publish");
                Console.WriteLine("3) Unsubscribe");
                var answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        channel = Console.ReadLine();
                        sub.Subscribe(channel, (chan, msg) =>
                         {
                             Console.WriteLine($"channel {chan}, msg {msg}");
                         });
                        break;
                    case "2":
                        sub.Publish(channel, Console.ReadLine());
                        break;
                    case "3":
                        sub.Unsubscribe(channel);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
