using BookLib.Models;
using BookLib.Notifies;
using BookLib.Permissions;
using GalaSoft.MvvmLight;
using LibraryProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.ViewModel.Worker
{
    public class RemoveViewModel : ViewModelBase
    {
        public IWorker<AbstractItem> Wlib { get; set; }
        public IWorkerNotify<AbstractItem> WNotify { get; set; }
        public string Text { get; set; }
        public string Amount { get; set; }
        public ButtonCommand Remove { get; set; }

        public RemoveViewModel(IWorker<AbstractItem> wl, IWorkerNotify<AbstractItem> wn)
        {
            Wlib = wl;
            WNotify = wn;
            Remove = new ButtonCommand(RemoveItem);
        }

        void RemoveItem()
        {
            //int amount = int.TryParse(Amount, out amount) ? amount : 1;
            bool isAll = !(int.TryParse(Amount, out int amount));
            AbstractItem a = null;

            if (Guid.TryParse(Text, out Guid guid))
            {
                try { a = Wlib[guid]; }
                catch (ArgumentNullException ex) { WNotify.General(ex.Message); }
            }
            else
            {
                try { a = Wlib[Text]; }
                catch (ArgumentNullException ex) { WNotify.General(ex.Message); }
            }
            if (a != null)
            {
                if (isAll) amount = Wlib.AmountOf(a);
                Wlib.Remove(a, amount);
                WNotify.Removed(a, amount);
            }
        }
    }
}
