namespace DiariumAPI.Models
{
    using System;
    using System.Collections.Generic;

    public interface IDocumentMetaData : IEntity<Guid>
    {
        new Guid Id { get; set; }

        string Title { get; set; }

        string RegistryNumber { get; set; }

        DateTime Registered { get; set; }

        bool Validate(out IEnumerable<string> propertyNames);
    }
}
