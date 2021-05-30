using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        public static HttpClient Client = new HttpClient();

        public static HttpClient[] Clients = new HttpClient[] { new HttpClient(), new HttpClient() };

        static async Task Main(string[] args)
        {
            var p = new Program();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await p.Test1();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Time spend {elapsedMs}");
            watch.Reset();
            watch.Start();
            await p.Test2();
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Time spend {elapsedMs}");

            watch.Reset();
            watch.Start();
            await p.Test3();
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Time spend {elapsedMs}");


            Console.ReadLine();

        }



        public async Task Test1()
        {
            for (int i = 0; i < 10; i++)
            {
                using (var httpClient = new HttpClient())
                {
                    var res = await httpClient.GetAsync("https://www.szcredit.org.cn");
                    // Console.WriteLine(res.StatusCode);
                }
            }
        }

        public async Task Test2()
        {
            for (int i = 0; i < 10; i++)
            {
                var res = await Client.GetAsync("https://www.szcredit.org.cn");
            }
        }

        public async Task Test3()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    var res = await Clients[0].GetAsync("https://www.szcredit.org.cn");
                }
                else
                {
                    var res = await Clients[1].GetAsync("https://docs.microsoft.com/en-us/dotnet/standard/async-in-depth");
                }

                    
                    // Console.WriteLine(res.StatusCode);
            }
        }

    }
}
