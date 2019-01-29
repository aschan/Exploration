namespace DiariumAPI.Models
{
    public interface IDocument : IDocumentMetaData
    {
        object Content { get; set; }
    }
}
