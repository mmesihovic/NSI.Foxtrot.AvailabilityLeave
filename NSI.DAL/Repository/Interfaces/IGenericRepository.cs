using System.Collections.Generic;

namespace NSI.DAL.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        T Create(T tObject);
        T GetById(long id);
        ICollection<T> GetAll();
        T Update(T tObject);
        bool Delete(long id);
    }
}
