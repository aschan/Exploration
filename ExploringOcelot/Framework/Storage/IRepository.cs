namespace Framework.Storage
{
    using System.Collections.Generic;

    public interface IRepository<T, in TKey> where T : IEntity<TKey>
    {
        IEnumerable<T> Get();

        T Get(TKey key);

        void Delete(T item);

        T Delete(TKey key);

        T Add(T item);

        T Update(T item);
    }
}
