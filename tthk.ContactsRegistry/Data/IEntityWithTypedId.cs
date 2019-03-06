namespace tthk.ContactsRegistry.Data
{
    public interface IEntityWithTypedId<T>
    {
        T Id { get; set; }
    }
}