namespace DiariumAPI.Entities
{
    using System;
    using System.Collections.Generic;

    using DiariumAPI.Models;

    public class Document : DocumentMetaData, IDocument
    {
        public override Guid Id { get; set; }
        public override string Title { get; set; }
        public override string RegistryNumber { get; set; }
        public override DateTime Registered { get; set; }
        public object Content { get; set; }

        public override bool Validate(out IEnumerable<string> propertyNames)
        {
            var properties = new List<string>();
            if (base.Validate(out var baseProperties) && baseProperties != null)
            {
                properties.AddRange(baseProperties);
            }

            if (Content == null)
            {
                properties.Add(nameof(Content));
            }

            propertyNames = properties.Count > 0 ? properties : null;
            return properties.Count == 0;
        }
    }
}
