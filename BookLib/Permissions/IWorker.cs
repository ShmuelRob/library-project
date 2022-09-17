using System;
using System.Collections.Generic;

namespace BookLib.Permissions
{
    public interface IWorker<T>
    {
        int Count { get; }

        void Add(T item, double price);
        void Add(T item, double price, int amount);
        bool Remove(T item);
        bool Remove(T item, int amount);
        List<T> ShowAll();
        int AmountOf(T item);
        int AvailableAmountOf(T item);
        double PriceOf(T item);
        T this[string name] { get; }
        T this[Guid isbn] { get; }
    }
}
