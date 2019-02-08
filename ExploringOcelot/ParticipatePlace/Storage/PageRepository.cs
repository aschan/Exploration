namespace ParticipatePlace.Storage
{
    using System;
    using System.Collections.Generic;

    using Framework.Storage;

    using ParticipatePlace.Models;

    public class PageRepository : IRepository<IPage, Guid>
    {
        private static readonly IDictionary<Guid, IPage> _pages = new Dictionary<Guid, IPage>();

        public IEnumerable<IPage> Get()
        {
            return _pages.Values;
        }

        public IPage Get(Guid key)
        {
            return _pages.TryGetValue(key, out var document) ? document : null;
        }

        public void Delete(IPage item)
        {
            _pages.Remove(item.Id);
        }

        public IPage Delete(Guid key)
        {
            if (!_pages.TryGetValue(key, out var document))
            {
                return null;
            }

            _pages.Remove(key);
            return document;

        }

        public IPage Add(IPage item)
        {
            _pages.Add(item.Id, item);
            return _pages[item.Id];
        }

        public IPage Update(IPage item)
        {
            _pages[item.Id] = item;
            return _pages[item.Id];
        }
    }
}
