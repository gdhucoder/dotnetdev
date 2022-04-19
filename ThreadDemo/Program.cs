using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var test = "I";
            var tt = new String("I");
            Console.WriteLine(test.Equals("I"));
            Console.WriteLine(tt == "I");
            //var p = new Program();
            //var rnd = new Random();
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //var tasks = new Task[100];
            //for (int i = 0; i < 100; i++)
            //{
            //    tasks[i] = Task.Run(()=> {
            //        Thread.Sleep(100);
            //        Console.WriteLine("   Task #{0}: {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.ThreadState);
            //    });
            //}
            //await Task.WhenAll(tasks);
            //var elpMs = watch.ElapsedMilliseconds;
            //Console.WriteLine($"{elpMs}");
            //Console.ReadLine();

        }


        public async Task run(int time)
        {
            Thread.Sleep(time);
            Console.WriteLine("   Task #{0}: {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.ThreadState);
        }



        

    }
}
