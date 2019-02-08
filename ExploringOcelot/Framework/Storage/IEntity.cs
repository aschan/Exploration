namespace Framework.Storage
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}
