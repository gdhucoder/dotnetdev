using NLayerWebApiDemo.Model;
using NLayerWebApiDemo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerWebApiDemo.Service
{
    public class CatServiceImpl : ICatService
    {

        public CatServiceImpl()
        {

        }

        public void Add(Cat entity)
        {
            using (var uow = new UnitOfWork("Server=localhost;Port=5004;Uid=root;Password=password;Database=testdb"))
            {
                uow.CatRepository.Add(entity);
                uow.Commit();
            }
        }

        public Cat Find(int id)
        {
            Cat res = null;
            using (var uow = new UnitOfWork(""))
            {
                res = uow.CatRepository.Find(id);
            }
            return res;
        }
    }
}
