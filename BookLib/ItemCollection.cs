using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BookLib.Enums;
using BookLib.Models;
using BookLib.Permissions;

namespace BookLib
{
    public class ItemCollection : IEnumerable, IEnumerable<AbstractItem>, ICustomer<AbstractItem>, IWorker<AbstractItem>
    {
        readonly List<Product> list;

        public ItemCollection()
        {
            list = new List<Product>();
            AddItemsToSystem();
        }

        public void Add(AbstractItem item, double price) => Add(item, price, 1);
        public void Add(AbstractItem item, double price, int amount)
        {
            var prod = list.Where((x) => x.Item == item).FirstOrDefault();
            if (prod is null)
            {
                foreach (var tmp in list)
                {
                    if (tmp.Item.Title == item.Title && tmp.Item.Author == item.Author
                        && tmp.Item.DatePublished == item.DatePublished && tmp.Item.Edition == item.Edition)
                        prod = tmp;
                }
            }
            if (prod is null) list.Add(new Product(item, price, amount));
            else prod.Amount += amount;
        }
        public bool Remove(AbstractItem item) => Remove(item, true);
        public bool Remove(AbstractItem item, int amount) => Remove(item, false, amount);
        bool Remove(AbstractItem item, bool isAll, int amount = 0)
        {
            var prod = list.Where((x) => x.Item == item).FirstOrDefault();
            if (prod is null) return false;
            if (isAll) list.Remove(prod);
            else
            {
                prod.Amount -= amount;
                if (prod.Amount <= 0) list.Remove(prod);
            }
            return true;
        }
        public bool Borrow(AbstractItem item)
        {
            var a = list.Where((x) => x.Item == item).FirstOrDefault();
            if (a is null) return false;
            if (a.InStock <= 0) return false;
            a.InStock--;
            return true;
        }
        public bool Buy(AbstractItem item)
        {
            var a = list.Where((x) => x.Item == item).FirstOrDefault();
            if (a is null || a.InStock <= 0) return false;
            a.Amount--;
            a.InStock--;
            return true;
        }
        public int Count
        {
            get
            {
                var count = 0;
                foreach (var item in list)
                    count += item.Amount;
                return count;
            }
        }
        public int AmountOf(AbstractItem item)
        {
            var a = list.Where((x) => x.Item == item).FirstOrDefault();
            return a is null ? throw new KeyNotFoundException() : a.Amount;
        }
        public int AvailableAmountOf(AbstractItem item)
        {
            var a = list.Where((x) => x.Item == item).FirstOrDefault();
            return a is null ? throw new KeyNotFoundException() : a.InStock;
        }
        public double PriceOf(AbstractItem item)
        {
            var a = list.Where((x) => x.Item == item).FirstOrDefault();
            return a is null ? throw new KeyNotFoundException() : a.Price;
        }

        public List<AbstractItem> ShowAll()
        {
            var lst = new List<AbstractItem>();
            foreach (var item in list) if (item.Amount > 0) lst.Add(item.Item);
            return lst;
        }
        public List<AbstractItem> FilterBy(Filter filter, double filterNum)
        {
            List<AbstractItem> lst;
            switch (filter)
            {
                case Filter.Price:
                    lst = list.Where(a => a.Price > filterNum).Select(x => x.Item).ToList();
                    break;
                case Filter.Available:
                    lst = list.Where(a => a.InStock > filterNum).Select(x => x.Item).ToList();
                    break;
                //case Filter.Discount:
                //    lst = list.Where(a => a.Discount > filterNum).Select(x => x.Item).ToList();
                //    break;
                case Filter.Amount:
                default:
                    lst = list.Where(a => a.Amount > filterNum).Select(x => x.Item).ToList();
                    break;
            }
            return lst;
        }
        public List<Book> FilterBooksByCategory(BookCategory category)
        {
            List<Book> lst = list.Where(x => x.Item is Book).Select(x => x.Item as Book).ToList();
            return lst.Where(x => x.Category == category).ToList();
        }
        public List<Journal> FilterJournalsByCategory(JournalCategory category)
        {
            List<Journal> lst = list.Where(x => x.Item is Journal).Select(x => x.Item as Journal).ToList();
            return lst.Where(x => x.Category == category).ToList();
        }
        public IEnumerator<AbstractItem> GetEnumerator()
        {
            foreach (var item in list) yield return item.Item;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // TODO exceptions
        public AbstractItem this[Guid index]
        {
            get
            {
                for (int i = 0; i < list.Count; i++)
                    if (list[i].Item.ISBN == index) return list[i].Item;
                throw new ArgumentNullException();
            }
        }
        public AbstractItem this[string index]
        {
            get
            {
                try
                {
                    var item = list.Select(x => x.Item).First(x => x.Title == index);
                    return item;
                }
                catch { throw new ArgumentNullException(); }
            }
        }

        void AddItemsToSystem()
        {
            Add(new Book("Harry Potter", "J.K Rolling", new DateTime(1999, 1, 11), 1, 700, BookCategory.Fantasy), 40, 50);
            Add(new Book("Percy Jackson", "Rick Riordan", new DateTime(2001, 5, 4), 2), 20,80);
            Add(new Book("Bible", "God", DateTime.MinValue, 1), 80, 10);
            Add(new Journal("Djkstra Algorithm", "Djakstra", new DateTime(1986, 10, 15), 1, "TA University", JournalCategory.Academic, JournalSpan.Monthly), 40);
            Add(new Book("Rich Dad Poor Dad", "Robert Kiyosaki", new DateTime(1998, 2, 6), 3), 80);
        }
    }
}