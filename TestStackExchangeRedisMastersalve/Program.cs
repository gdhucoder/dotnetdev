using StackExchange.Redis;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6380,localhost:6381,localhost:6382");
            IDatabase db = redis.GetDatabase();
            //for (int i = 0; i < 10; i++)
            //{
            //    // 请求随机分配给从库（如果从库不可用，请求落到主库），适合读请求
            //    string value = db.StringGet("name", CommandFlags.PreferReplica);
            //    Console.WriteLine(value); // writes: "abcdefg"
            //}
            var keys = new RedisKey[] { "name", "B" , "shcool"};
            var cache = db.StringGet(keys, CommandFlags.PreferReplica);
            for(int i = 0;i < cache.Length; i++)
            {
                string val = cache[i];
                if (val is null)
                {
                    val = "null";
                }
                Console.WriteLine(val);
            }


        }
    }
}