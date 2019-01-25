namespace DiariumAPI.Models
{
    using System;

    using DiariumAPI.Entities;

    public interface IDocumentMetaData : IEntity<Guid>
    {
        new Guid Id { get; set; }

        string Title { get; set; }

        string RegistryNumber { get; set; }

        DateTime Registered { get; set; }
    }
}
