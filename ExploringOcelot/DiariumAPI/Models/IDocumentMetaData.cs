namespace DiariumAPI.Models
{
    using System;
    using System.Collections.Generic;

    using Framework.Storage;

    public interface IDocumentMetaData : IEntity<Guid>
    {
        new Guid Id { get; set; }

        string Title { get; set; }

        string RegistryNumber { get; set; }

        DateTime Registered { get; set; }

        Uri Url { get; set; }

        bool Validate(out IEnumerable<string> propertyNames);
    }
}
