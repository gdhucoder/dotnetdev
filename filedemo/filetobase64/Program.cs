using System;
using System.IO;

namespace filetobase64
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                test();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine($"{i} Time spend {elapsedMs}");
            }

        }


        /// <summary>
        /// 1. read file to byte[]
        /// 2. convert byte[] to base64string
        /// 3. sender: send base64string file content
        /// 4. receiver: convert base64string back to original file
        /// (each character in base64 string takes up 8 bits, represents 6 bits in byte array,
        ///  so the size will roughly 1.33 times larger than the original file)
        /// </summary>
        public static void test()
        {
            byte[] oribyts = File.ReadAllBytes("E:/dotnetdev/filedemo/filetobase64/华为技术有限公司.pdf");
            var file2base64string = Convert.ToBase64String(oribyts);
            byte[] bytesfile = Convert.FromBase64String(file2base64string);
            File.WriteAllBytes("E:/dotnetdev/filedemo/filetobase64/华为技术有限公司-convert.pdf", bytesfile);
        }
    }


}
