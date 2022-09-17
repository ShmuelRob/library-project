namespace BookLib.Notifies
{
    public interface IWorkerNotify<T>
    {
        void Added(T item, int amount);
        void Updated(T item);
        void Removed(T item, int amount);
        bool AskWorker();
        void General(string message);
    }
}
