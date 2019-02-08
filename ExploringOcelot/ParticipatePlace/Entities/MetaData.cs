namespace ParticipatePlace.Entities
{
    using System;
    using System.Collections.Generic;

    using ParticipatePlace.Models;

    public class MetaData : IMetaData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public bool Validate(out IEnumerable<string> propertyNames)
        {
            throw new NotImplementedException();
        }
    }
}
