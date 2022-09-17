using BookLib.Enums;
using BookLib.Models;
using BookLib.Notifies;
using BookLib.Permissions;
using GalaSoft.MvvmLight;
using LibraryProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.ViewModel.Worker
{
    public class FilterViewModel : ViewModelBase
    {
        public IWorker<AbstractItem> WLib { get; set; }
        public IWorkerNotify<AbstractItem> WNotify { get; set; }

        ObservableCollection<AbstractItem> items;
        public ObservableCollection<AbstractItem> Items { get => items; set => items = value; }
        public Array FilterOptions => Enum.GetValues(typeof(Filter));
        public Filter Selected { get; set; }
        public string MinV { get; set; }
        public string MaxV { get; set; }
        public ButtonCommand FilterItems { get; set; }
        public FilterViewModel(IWorker<AbstractItem> wl, IWorkerNotify<AbstractItem> wn)
        {
            WLib = wl;
            WNotify = wn;
            FilterItems = new ButtonCommand(FilterAct);
            items = new ObservableCollection<AbstractItem>(WLib.ShowAll());
            //Items.CollectionChanged += (s, e) => FilterAct();
            //items.CollectionChanged += (s, e) => FilterAct();
        }

        void FilterAct()
        {
            ObservableCollection<AbstractItem> items;
            if (!(double.TryParse(MinV, out double a) && double.TryParse(MaxV, out double z))) return;

            else
            {
                if (a > z) Swap(ref a, ref z);
                switch (Selected)
                {
                    case Filter.Available:
                        items = new ObservableCollection<AbstractItem>(WLib.ShowAll()
                            .Where(x => WLib.AvailableAmountOf(x) >= a && WLib.AvailableAmountOf(x) <= z));
                        break;
                    case Filter.Amount:
                        items = new ObservableCollection<AbstractItem>(WLib.ShowAll()
                            .Where(x => WLib.AmountOf(x) >= a && WLib.AmountOf(x) <= z));
                        break;
                    case Filter.Price:
                    default:
                        items = new ObservableCollection<AbstractItem>(WLib.ShowAll()
                            .Where(x => WLib.PriceOf(x) >= a && WLib.PriceOf(x) <= z));
                        break;
                }

            }
            //Items = new ObservableCollection<AbstractItem>(items);
            //else items = new ObservableCollection<AbstractItem>();
            StringBuilder sb = new StringBuilder();
            foreach (AbstractItem item in items) sb.AppendLine(item.ToString());
            WNotify.General(sb.ToString());
        }
        void Swap<T>(ref T i1, ref T i2) => (i2, i1) = (i1, i2);
    }
}
