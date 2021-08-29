using NLayerWebApiDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace NLayerWebApiDemo.Repository
{
    internal class DogRepository : RepositoryBase, IDogRepository
    {
        public DogRepository(IDbTransaction transaction)
            :base(transaction)
        {

        }

        public void Add(Cats entity)
        {
            entity.id = Connection.ExecuteScalar<int>(
                "insert into Dogs (Name, Birthday) value (@Name, now());SELECT LAST_INSERT_ID();",
                param: new {Name = entity.Name },
                transaction: Transaction);
        }

        public Cats Find(int id)
        {
            return Connection.QueryFirstOrDefault<Cats>(
                "select * from Dogs where id=@Id",
                param: new { Id = id},
                transaction:Transaction);
        }
    }
}
