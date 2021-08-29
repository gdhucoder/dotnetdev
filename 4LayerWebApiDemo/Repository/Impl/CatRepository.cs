using NLayerWebApiDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace NLayerWebApiDemo.Repository
{
    internal class CatRepository : RepositoryBase, ICatRepository
    {
        public CatRepository(IDbTransaction transaction)
            :base(transaction)
        {

        }

        public void Add(Cats entity)
        {
            entity.id = Connection.ExecuteScalar<int>(
                "insert into Cats (Name, Birthday) value (@Name, now());SELECT LAST_INSERT_ID();",
                param: new {Name = entity.Name },
                transaction: Transaction);
        }

        public Cats Find(int id)
        {
            var res = Connection.Get<Cats>(id);
            return res;
        }

        public void TestDMDataBase()
        {
            string sql = "select Name from \"TestDB\".\"UserDB\";";
            var name = Connection.QueryFirst<string>(sql);
            Console.WriteLine($"{ name }");
        }
    }
}
