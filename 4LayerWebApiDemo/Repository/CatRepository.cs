using NLayerWebApiDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace NLayerWebApiDemo.Repository
{
    internal class CatRepository : RepositoryBase, ICatRepository
    {
        public CatRepository(IDbTransaction transaction)
            :base(transaction)
        {

        }

        public void Add(Cat entity)
        {
            entity.id = Connection.ExecuteScalar<int>(
                "insert into Cats (Name, Birthday) value (@Name, now());SELECT LAST_INSERT_ID();",
                param: new {Name = entity.Name },
                transaction: Transaction);
        }

        public Cat Find(int id)
        {
            return Connection.QueryFirstOrDefault<Cat>(
                "select * from Cats",
                param: new { Id = id},
                transaction:Transaction);
        }
    }
}
