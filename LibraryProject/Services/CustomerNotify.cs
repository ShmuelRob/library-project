using BookLib.Models;
using BookLib.Notifies;
using System.Windows;

namespace LibraryProject.Services
{
    public class CustomerNotify : ICustomerNotify<AbstractItem>
    {
        public bool? Ask(AbstractItem item)
        {
            switch (MessageBox.Show("do you want to buy or borrow this item?", "are you sure", MessageBoxButton.YesNoCancel))
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                case MessageBoxResult.Cancel:
                default:
                    return null;
            }
        }
        public void Borrowed(AbstractItem item) => MessageBox.Show($"You successfully borrowed {item}");
        public void Bought(AbstractItem item) => MessageBox.Show($"You successfully bought {item}");
        public void NotFound(AbstractItem item) => MessageBox.Show($"We couldn't find {item}");
        public void NotFound() => MessageBox.Show($"We couldn't find item with this properties");
    }
}
