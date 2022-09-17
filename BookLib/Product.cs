using System.Text;
using BookLib.Models;

namespace BookLib
{
    internal class Product
    {
        private double price;

        internal AbstractItem Item { get; set; }
        internal int Amount { get; set; }
        internal int InStock { get; set; }
        internal double Price { get => price * (1 - Discount); set => price = value; }
        internal double Discount
        {
            get
            {
                double discount = 0;
                if (Item is Book) discount += .1;
                if (Amount > 5) discount += .11;
                if (Amount - InStock < 2) discount -= .07;
                if (discount < 0) return 0;
                if (discount > 0.95) return 0.95;
                return discount;
            }
        }

        public Product(AbstractItem item, double price, int amount)
        {
            Item = item;
            Price = price;
            Amount = amount;
            InStock = amount;
        }
        public override string ToString()
        {
            var isAvailable = InStock != 0;
            var sb = new StringBuilder(Item.ToString());
            sb.Append($", {Price}, Available: {isAvailable}");
            return sb.ToString();
        }
    }
}
