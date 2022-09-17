using BookLib.Models;
using BookLib.Notifies;
using BookLib.Permissions;
using GalaSoft.MvvmLight;
using LibraryProject.Services;
using LibraryProject.Views.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.ViewModel.Customer
{
    public class CustomerViewModel : ViewModelBase
    {
        public ICustomer<AbstractItem> CLib { get; set; }
        public ICustomerNotify<AbstractItem> CNotify { get; set; }
        public string Text { get; set; }
        public ButtonCommand Search { get; set; }
        public ButtonCommand ShowAll { get; set; }
        public CustomerViewModel(ICustomer<AbstractItem> customer, ICustomerNotify<AbstractItem> notify)
        {
            CLib = customer;
            CNotify = notify;
            Search = new ButtonCommand(SearchItem);
            ShowAll = new ButtonCommand(ShowAllItems);
        }

        void SearchItem()
        {
            AbstractItem item;
            try
            {
                if (Guid.TryParse(Text, out Guid isbn)) item = CLib[isbn];
                else item = CLib[Text];
            }
            catch { CNotify.NotFound(); return; }

            bool? a = CNotify.Ask(item);
            switch (a)
            {
                case null: break;
                case true:
                    CLib.Buy(item);
                    CNotify.Bought(item);
                    break;
                case false:
                    CLib.Borrow(item);
                    CNotify.Borrowed(item);
                    break;
                default: break;
            }
        }
        void ShowAllItems()
        {
            ItemsListView listView = new ItemsListView();
            listView.Activate();
            listView.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
