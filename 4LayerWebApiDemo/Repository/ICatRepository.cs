using NLayerWebApiDemo.Model;

namespace NLayerWebApiDemo.Repository
{
    public interface ICatRepository
    {
        void Add(Cats entity);
        Cats Find(int id);
        void TestDMDataBase();
    }
}