namespace DiariumAPI.Entities
{
    using System;
    using System.Collections.Generic;

    using DiariumAPI.Models;

    public class DocumentMetaData : IDocumentMetaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string RegistryNumber { get; set; }
        public virtual DateTime Registered { get; set; }
        public virtual Uri Url { get; set; }

        public virtual bool Validate(out IEnumerable<string> propertyNames)
        {
            var properties = new List<string>();

            if (Id == Guid.Empty)
            {
                properties.Add(nameof(Id));
            }

            if (string.IsNullOrWhiteSpace(Title))
            {
                properties.Add(nameof(Title));
            }

            if (string.IsNullOrWhiteSpace(RegistryNumber))
            {
                properties.Add(nameof(RegistryNumber));
            }

            if (Registered == DateTime.MinValue)
            {
                properties.Add(nameof(Registered));
            }

            propertyNames = properties.Count > 0 ? properties : null;
            return properties.Count == 0;
        }
    }
}
