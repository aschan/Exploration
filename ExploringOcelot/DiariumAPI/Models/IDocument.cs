namespace DiariumAPI.Models
{
    public interface IDocument : IDocumentMetaData
    {
        IDocumentMetaData MetaData { get; set; }

        object Content { get; set; }
    }
}
