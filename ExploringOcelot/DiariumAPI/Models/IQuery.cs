namespace DiariumAPI.Models
{
    public interface IQuery
    {
        IFilter Filter { get; set; }

        ISort Sort { get; set; }

        IRestrict Restrict { get; set; }
    }
}
