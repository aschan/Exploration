namespace ParticipatePlace.Models
{
    using System.Collections.Generic;

    public interface IResult
    {
        IQuery Query { get; set; }

        IEnumerable<IMetaData> Documents { get; set; }
    }
}
