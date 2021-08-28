using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerWebApiDemo.Repository
{
    public interface IUnitOfWork :IDisposable
    {
        ICatRepository CatRepository { get; }

        void Commit();
    }
}
