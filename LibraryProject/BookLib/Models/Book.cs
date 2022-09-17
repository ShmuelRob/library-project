using System;
using BookLib.Enums;

namespace BookLib.Models
{
    public class Book : AbstractItem
    {
        public int Pages { get; set; }
        public BookCategory Category { get; set; }

        public Book(string title, string author, DateTime date, byte edition, int pages, BookCategory cat)
            : base(title, author, date, edition)
        {
            Pages = pages;
            Category = cat;
        }
        public Book(string title, string author, DateTime date, byte edition, int pages)
            : this(title, author, date, edition, pages, BookCategory.Other) { }
        public Book(string title, string author, DateTime date, byte edition, BookCategory cat)
            : this(title, author, date, edition, 0, cat) { }
        public Book(string title, string author, DateTime date, byte edition)
            : this(title, author, date, edition, 0, BookCategory.Other) { }

        public override sealed bool Equals(object obj)
        {
            if (!(obj is Book)) return false;
            Book other = obj as Book;
            if (ISBN == other.ISBN) return true;
            if (Title == other.Title && Author == other.Author) return true;
            return false;
        }
        public override sealed int GetHashCode() => base.GetHashCode();
        public static bool operator ==(Book book1, Book book2) => book1.Equals(book2);
        public static bool operator !=(Book book1, Book book2) => !(book1 == book2);
    }
}
