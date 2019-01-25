namespace DiariumAPI.Entities
{
    using System;

    using DiariumAPI.Models;

    public class DocumentMetaData : IDocumentMetaData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string RegistryNumber { get; set; }
        public DateTime Registered { get; set; }
    }
}
