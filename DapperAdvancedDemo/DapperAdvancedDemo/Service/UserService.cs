using DapperAdvancedDemo.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dm;

namespace DapperAdvancedDemo.Service
{
    public class UserService
    {
        public UserService()
        {
        }
        public PagedResults<User> FindAllUsers()
        {
            var result = new PagedResults<User>();
            using (IDbConnection cnn = new MySqlConnection("Server=127.0.0.1;Port=5004;Uid=root;Password=password;Database=testdb"))
            {
                string sql = "select * from user where id = '1'; select count(*) from user;";
                
                using (var mult = cnn.QueryMultiple(sql))
                {
                    // user list
                    result.Items = mult.Read<User>().ToList();
                    // total records
                    result.Total = mult.ReadFirst<int>();
                }

                foreach (var user in result.Items)
                {
                    Console.WriteLine($"{ user.UserName }");
                }
            }
            return result;
        }

        public PagedResults<User> FindCountsDamenDB()
        {
            var result = new PagedResults<User>();

            try
            {
                IDbConnection cnn = new DmConnection("server=localhost;port=5236;user=SYSDBA;password=SYSDBA");
                string sql = "select Name from \"TestDB\".\"UserDB\";";
                var name = cnn.QueryFirst<string>(sql);
                Console.WriteLine($"{ name }");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message }");
                throw;
            }

            return result;
        }
    }
}
