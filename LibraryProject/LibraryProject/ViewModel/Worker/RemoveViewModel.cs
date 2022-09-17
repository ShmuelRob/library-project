using BookLib.Models;
using BookLib.Notifies;
using BookLib.Permissions;
using GalaSoft.MvvmLight;
using LibraryProject.Services;
using System;
using System.Collections.Generic;

namespace LibraryProject.ViewModel.Worker
{
    public class RemoveViewModel : ViewModelBase
    {
        public IWorker<AbstractItem> Wlib { get; set; }
        public IWorkerNotify<AbstractItem> WNotify { get; set; }
        public string Text { get; set; }
        public string Amount { get; set; }
        public ButtonCommand Remove { get; set; }
        readonly OverviewViewModel OverviewViewModel;
        readonly GeneralViewModel generalViewModel;

        public RemoveViewModel(OverviewViewModel overview, GeneralViewModel general, IWorker<AbstractItem> wl, IWorkerNotify<AbstractItem> wn)
        {
            Wlib = wl;
            WNotify = wn;
            Remove = new ButtonCommand(RemoveItem);
            OverviewViewModel = overview;
            generalViewModel = general;
        }

        void RemoveItem()
        {
            bool isAll = !(int.TryParse(Amount, out int amount));
            AbstractItem a = null;

            if (Guid.TryParse(Text, out Guid guid))
            {
                try { a = Wlib[guid]; }
                catch (KeyNotFoundException ex) { WNotify.General(ex.Message); }
            }
            else
            {
                try { a = Wlib[Text]; }
                catch (KeyNotFoundException ex) { WNotify.General(ex.Message); }
            }
            if (a != null)
            {
                if (!isAll && amount == Wlib.AmountOf(a)) isAll = true;
                if (isAll)
                {
                    amount = Wlib.AmountOf(a);
                    OverviewViewModel.Items.Remove(a);
                }
                Wlib.Remove(a, amount);
                WNotify.Removed(a, amount);
                generalViewModel.Count -= amount;
            }
        }
    }
}
