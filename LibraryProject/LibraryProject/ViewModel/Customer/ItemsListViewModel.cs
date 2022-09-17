using BookLib;
using BookLib.Models;
using BookLib.Notifies;
using BookLib.Permissions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.ViewModel.Customer
{
    public class ItemsListViewModel : ViewModelBase
    {
        public ICustomer<AbstractItem> Clib { get; set; }
        public ICustomerNotify<AbstractItem> CNotify { get; set; }
        public ObservableCollection<AbstractItem> Items { get => new ObservableCollection<AbstractItem>(Clib.ShowAll()); }
        AbstractItem selected;
        public AbstractItem Selected
        {
            get => selected;
            set
            {
                selected = value;
                SelectedItem();
            }
        }
        public ItemsListViewModel(ItemCollection col, ICustomerNotify<AbstractItem> notify)
        {
            Clib = col;
            CNotify = notify;
        }

        void SelectedItem()
        {
            bool? a = CNotify.Ask(selected);
            switch (a)
            {
                case null:
                    break;
                case true:
                    Clib.Buy(Selected);
                    CNotify.Bought(Selected);
                    break;
                case false:
                    Clib.Borrow(Selected);
                    CNotify.Borrowed(Selected);
                    break;
                default:
                    break;
            }
        }
    }
}
