namespace DiariumAPI.Models
{
    using System;

    public interface IDocument
    {
        Guid Id { get; set; }

        IDocumentMetaData MetaData { get; set; }

        IDocumentContent Content { get; set; }
    }
}
