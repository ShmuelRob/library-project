using System;

namespace BookLib.Models
{
    public abstract class AbstractItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        Guid isbn;
        public Guid ISBN => isbn;
        public DateTime DatePublished { get; set; }
        public byte Edition { get; set; }

        public AbstractItem(string title, string author, DateTime date, byte edition)
        {
            Title = title;
            Author = author;
            DatePublished = date;
            Edition = edition;
            isbn = Guid.NewGuid();
            //isbn = new Guid();
        }

        public override sealed string ToString() => $"{Title}, by: {Author}";
        public abstract override bool Equals(object obj);
        public override int GetHashCode() => base.GetHashCode();
    }
}
