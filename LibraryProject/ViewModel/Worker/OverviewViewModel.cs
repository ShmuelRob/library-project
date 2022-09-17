using BookLib.Models;
using BookLib.Notifies;
using BookLib.Permissions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.ViewModel.Worker
{
    public class OverviewViewModel : ViewModelBase
    {
        public IWorker<AbstractItem> WLib { get; set; }
        IWorkerNotify<AbstractItem> WNotify { get; set; }
        BindingList<AbstractItem> X;
        public ObservableCollection<AbstractItem> Items => new ObservableCollection<AbstractItem>(WLib.ShowAll());

        AbstractItem selecteditem;
        public AbstractItem SelectedItem
        {
            get => selecteditem;
            set
            {
                selecteditem = value;
                ItemSelected();
            }
        }

        public OverviewViewModel(IWorker<AbstractItem> worker, IWorkerNotify<AbstractItem> notify)
        {
            WLib = worker;
            WNotify = notify;

            //X = new BindingList<AbstractItem>();
            //Items.CollectionChanged += (s, e) => ItemSelected();
            //X.ListChanged
        }

        void ItemSelected()
        {
            //items = new ObservableCollection<AbstractItem>(WLib.ShowAll());
            try
            {
                StringBuilder sb = new StringBuilder($"{selecteditem.Title}, by: ");
                sb.AppendLine(SelectedItem.Author);
                sb.AppendLine($"{selecteditem.ISBN}");
                sb.Append($"amount: {WLib.AmountOf(SelectedItem)}, price {WLib.PriceOf(SelectedItem)}");
                WNotify.General(sb.ToString());
            }
            catch (Exception ex) { WNotify.General(ex.Message); }
        }
    }
}
