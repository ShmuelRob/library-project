using BookLib.Models;
using System.Text;

namespace BookLib.Reports
{
    public class Report
    {
        public ReportType Type { get; set; }
        public AbstractItem Item { get; set; }
        public int Amount { get; set; }
        public Report(ReportType type, AbstractItem item, int amount)
        {
            Type = type;
            Item = item;
            Amount = amount;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Amount} of ");
            sb.Append($"{Item}");
            switch (Type)
            {
                case ReportType.Add:
                    sb.Append(" added");
                    break;
                case ReportType.Remove:
                    sb.Append(" removed");
                    break;
                case ReportType.borrow:
                    sb.Append(" added");
                    break;
                case ReportType.buy:
                    sb.Append(" added");
                    break;
                default:
                    break;
            }
            sb.Append("to the library");
            return sb.ToString();
        }
    }
}
