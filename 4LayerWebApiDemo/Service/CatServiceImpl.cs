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
        private readonly IUnitOfWork _uow;
        public CatServiceImpl(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(Cats entity)
        {
            _uow.CatRepository.Add(entity);
            _uow.Commit();
        }

        public Cats Find(int id)
        {
            return _uow.CatRepository.Find(id); ;
        }

        public void TestDM()
        {
            _uow.CatRepository.TestDMDataBase();
        }
    }
}
