using System;
using BookLib.Enums;

namespace BookLib.Models
{
    public class Journal : AbstractItem
    {
        public string Organization { get; set; }
        public JournalCategory Category { get; set; }
        public JournalSpan Span { get; set; }

        public Journal(string title, string author, DateTime date, byte edition, string organization, JournalCategory cat, JournalSpan span)
            : base(title, author, date, edition)
        {
            Organization = organization;
            Category = cat;
            Span = span;
        }
        public Journal(string title, string author, DateTime date, byte edition, string organization, JournalCategory cat)
            : this(title, author, date, edition,organization, cat, JournalSpan.Monthly) { }
        public Journal(string title, string author, DateTime date, byte edition, string organization, JournalSpan span)
            : this(title, author, date, edition, organization, JournalCategory.other, span) { }
        public Journal(string title, string author, DateTime date, byte edition, string organization)
            : this(title, author, date, edition, organization, JournalCategory.other, JournalSpan.Monthly) { }

        public override sealed bool Equals(object obj)
        {
            if (!(obj is Journal)) return false;
            Journal other = (Journal)obj;
            if (ISBN == other.ISBN) return true;
            if (Title == other.Title && Author == other.Author) return true;
            return false;
        }
        public override sealed int GetHashCode() => base.GetHashCode();
        public static bool operator ==(Journal journal1, Journal journal2) => journal1.Equals(journal2);
        public static bool operator !=(Journal journal1, Journal journal2) => !journal1.Equals(journal2);
    }
}
