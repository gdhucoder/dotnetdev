using MySql.Data.MySqlClient;
using System;
using System.Data;
using Dapper;
using System.Collections.Generic;
using static HelperLibrary.Tools;

namespace StopWordsDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // GetWriteCount();
            // InsertDataBatch();
            Freq();
            Console.WriteLine("Hello World!");
        }

        private static void GetWriteCount()
        {
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                string sql = @"select * from articles;";

                var items = cnn.Query<Item>(sql);

                foreach (var i in items)
                {
                    Console.WriteLine($"{i.title}:{i.body}");
                }
            }
        }

        private static void InsertData()
        {
            int Records = 1000000;
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                string sql = @"insert into articles (title, body) values (@title, @body);";
                
                for (int i = 0; i < Records; i++)
                {
                    cnn.Execute(sql, new { title="数据库管理", body="在本教程中我将向你展示如何管理数据库" });
                }
               
            }
        }

        private static void InsertDataBatch()
        {
            int Records = 1000000;
            List<Item> list = new List<Item>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(new Item { title = "数据库管理", body = "在本教程中我将向你展示如何管理数据库" });
            }
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                string sql = @"insert into articles (title, body) values (@title, @body);";
                
                for (int i = 0; i < 100; i++)
                {
                    cnn.Execute(sql, list);
                }

            }
        }

        private static void Freq()
        {
            
            using (IDbConnection cnn = new MySqlConnection(GetConnectionString()))
            {
                string sql = @"select title, body from articles limit @start,1000;";
                for (int cnt = 0; cnt < 105; cnt++)
                {
                    var list = cnn.Query<Item>(sql, new { start = cnt*1000});
                    string update = "insert into word_freq (word) values (@word) on duplicate key update count=count+1;";
                    foreach (var it in list)
                    {
                        for (int i = 0; i < it.title.Length - 1; i++)
                        {
                            Console.WriteLine(it.title.Substring(i, 2));
                            cnn.Execute(update, new { word = it.title.Substring(i, 2) });
                        }
                    }
                }


            }
        }

    }

    class Item
    {
        public string title { get; set; }
        public string body { get; set; }
    }
}
