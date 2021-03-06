using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace NLayerWebApiDemo.Repository
{
    internal abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }

        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public RepositoryBase (IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
