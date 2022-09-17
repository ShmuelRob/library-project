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
        }

        void FilterAct()
        {
            ObservableCollection<AbstractItem> items;
            if (!(double.TryParse(MinV, out double a) && double.TryParse(MaxV, out double z))) return;
            else
            {
                if (a > z) Swap(ref a, ref z);
                items = WLib.FilterBy(Selected, a, z);
            }
            StringBuilder sb = new StringBuilder();
            foreach (AbstractItem item in items) sb.AppendLine(item.ToString());
            WNotify.General(sb.ToString());
        }
        void Swap<T>(ref T i1, ref T i2) => (i2, i1) = (i1, i2);
    }
}
