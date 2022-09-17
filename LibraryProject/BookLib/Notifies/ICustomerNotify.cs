namespace BookLib.Notifies
{
    public interface ICustomerNotify<T>
    {
        void Borrowed(T item);
        void Bought(T item);
        void NotFound(T item);
        void NotFound();
        bool? Ask(T item);
    }
}
