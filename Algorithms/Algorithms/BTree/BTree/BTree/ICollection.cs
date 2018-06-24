using System;

namespace BTree
{
    interface ICollection<T> where T : IComparable<T>
    {
        bool Contains(T value);
        T Search(T value);
        void Add(T value);
        void Delete(T value);
    }
}
