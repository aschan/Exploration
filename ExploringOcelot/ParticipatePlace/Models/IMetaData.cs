namespace ParticipatePlace.Models
{
    using System;
    using System.Collections.Generic;

    using Framework.Storage;

    public interface IMetaData : IEntity<Guid>
    {
        new Guid Id { get; set; }

        string Title { get; set; }

        Uri Url { get; set; }

        DateTime Created { get; set; }

        DateTime Modified { get; set; }

        bool Validate(out IEnumerable<string> propertyNames);
    }
}
