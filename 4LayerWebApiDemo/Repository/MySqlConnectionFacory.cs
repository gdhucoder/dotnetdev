using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace NLayerWebApiDemo.Repository
{
    public class MySqlConnectionFacory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection("Server=localhost;Port=5004;Uid=root;Password=password;Database=testdb");
        }
    }
}
