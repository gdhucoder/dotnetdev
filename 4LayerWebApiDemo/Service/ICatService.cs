using NLayerWebApiDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerWebApiDemo.Service
{
    public interface ICatService
    {
        void Add(Cats entiry);
        Cats Find(int id);
        void TestDM();
    }
}
