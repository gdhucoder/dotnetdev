using NLayerWebApiDemo.Model;

namespace NLayerWebApiDemo.Repository
{
    public interface ICatRepository
    {
        void Add(Cat entity);
        Cat Find(int id);
    }
}