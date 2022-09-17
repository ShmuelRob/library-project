using BookLib.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookLib.Permissions
{
    public interface IWorker<T>
    {
        int Count { get; }

        void Add(T item, double price);
        void Add(T item, double price, int amount);
        bool Remove(T item);
        bool Remove(T item, int amount);
        ObservableCollection<T> ShowAll();
        int AmountOf(T item);
        int AvailableAmountOf(T item);
        double PriceOf(T item);
        ObservableCollection<T> FilterBy(Filter filter, double min, double max);
        T this[string name] { get; }
        T this[Guid isbn] { get; }
    }
}
