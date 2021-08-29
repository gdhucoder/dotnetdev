using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dm;

namespace NLayerWebApiDemo.Repository
{
    public class DamengConnectionFacory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            var cnn = new DmConnection("server=localhost;port=5236;user=SYSDBA;password=SYSDBA");
            return cnn;
        }
    }
}
