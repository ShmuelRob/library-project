using BookLib.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookLib.Permissions
{
    public interface ICustomer<T>
    {
        bool Borrow(T item);
        bool Buy(T item);
        int AmountOf(T item);
        double PriceOf(T item);
        ObservableCollection<T> ShowAll();
        T this[string name] { get; }
        T this[Guid isbn] { get; }
    }
}
