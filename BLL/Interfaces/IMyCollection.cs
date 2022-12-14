using System.Collections;
using DAL;

namespace BLL
{
    public interface IMyCollection : IEnumerable
    {
        void Add(Rectangle rectangle);
        void Delete();
        void Delete(int index);
        Rectangle Find(Rectangle rectangle);
    }

    public interface IMyCollection<T> : IEnumerable
    {
        void Add(T rectangle);
        void Delete();
        T Find(T data);
    }
}
