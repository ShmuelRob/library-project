using BookLib.Models;
using BookLib.Permissions;
using System.Windows;

namespace LibraryProject.Views.Worker.WorkerTabs
{
    public partial class ItemPropsView : Window
    {
        public ItemPropsView(AbstractItem item, IWorker<AbstractItem> Wl)
        {
            InitializeComponent();
            try
            {
                first.Text = $"{item.Title}, by: {item.Author}";
                link.Text = $"{item.ISBN}";
                last.Text = $"Amount: {Wl.AmountOf(item)}, Price: {Wl.PriceOf(item)}";
            }
            catch { }
        }
    }
}
