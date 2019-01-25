namespace DiariumAPI.Entities
{
    using System;

    using DiariumAPI.Models;

    public class Document : IDocument
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string RegistryNumber { get; set; }
        public DateTime Registered { get; set; }
        public IDocumentMetaData MetaData { get; set; }
        public object Content { get; set; }
    }
}
