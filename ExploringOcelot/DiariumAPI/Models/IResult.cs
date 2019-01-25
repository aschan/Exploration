namespace DiariumAPI.Models
{
    using System.Collections.Generic;

    public interface IResult
    {
        IQuery Query { get; set; }

        IEnumerable<IDocumentMetaData> Documents { get; set; }
    }
}
