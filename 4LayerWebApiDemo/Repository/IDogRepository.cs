using NLayerWebApiDemo.Model;

namespace NLayerWebApiDemo.Repository
{
    public interface IDogRepository
    {
        void Add(Cats entity);
        Cats Find(int id);
    }
}