using BookLib.Models;
using BookLib.Notifies;
using BookLib.Permissions;
using GalaSoft.MvvmLight;
using LibraryProject.Views.Worker.WorkerTabs;
using System;
using System.Collections.ObjectModel;

namespace LibraryProject.ViewModel.Worker
{
    public class OverviewViewModel : ViewModelBase
    {
        public IWorker<AbstractItem> WLib { get; set; }
        IWorkerNotify<AbstractItem> WNotify { get; set; }
        ObservableCollection<AbstractItem> items;
        public ObservableCollection<AbstractItem> Items { get => items; set => Set(ref items, value); }
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
            items = WLib.ShowAll();
        }

        void ItemSelected()
        {
            if (selecteditem == null) return;
            try
            {
                ItemPropsView itemProps = new ItemPropsView(SelectedItem, WLib);
                itemProps.Show();
            }
            catch (Exception ex) { WNotify.General(ex.Message); }
        }
    }
}
