namespace DiariumAPI.Storage
{
    using System;
    using System.Collections.Generic;
    
    using DiariumAPI.Models;

    using Framework.Storage;

    public class DocumentRepository : IRepository<IDocument, Guid>
    {
        private static readonly IDictionary<Guid, IDocument> _documents = new Dictionary<Guid, IDocument>();

        public IEnumerable<IDocument> Get()
        {
            return _documents.Values;
        }

        public IDocument Get(Guid key)
        {
            return _documents.TryGetValue(key, out var document) ? document : null;
        }

        public void Delete(IDocument item)
        {
            _documents.Remove(item.Id);
        }

        public IDocument Delete(Guid key)
        {
            if (!_documents.TryGetValue(key, out var document))
            {
                return null;
            }

            _documents.Remove(key);
            return document;

        }

        public IDocument Add(IDocument item)
        {
            _documents.Add(item.Id, item);
            return _documents[item.Id];
        }

        public IDocument Update(IDocument item)
        {
            _documents[item.Id] = item;
            return _documents[item.Id];
        }
    }
}
