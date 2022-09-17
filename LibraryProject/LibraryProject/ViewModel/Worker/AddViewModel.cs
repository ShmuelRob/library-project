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
    public class AddViewModel : ViewModelBase
    {
        public IWorker<AbstractItem> WLib { get; set; }
        public IWorkerNotify<AbstractItem> WNotify { get; set; }
        public ButtonCommand AddCommand { get; set; }
        readonly OverviewViewModel overviewViewModel;
        readonly GeneralViewModel generalViewModel;
        #region UI Connection
        public bool BClicked { get; set; }
        public bool JClicked { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DatePublished { get; set; }
        public string Edition { get; set; }
        public string Pages { get; set; }
        public BookCategory BCategory { get; set; }
        public JournalCategory JCategory { get; set; }
        public JournalSpan JSpan { get; set; }
        public string Publisher { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }

        public Array BCValues => Enum.GetValues(typeof(BookCategory));
        public Array JCValues => Enum.GetValues(typeof(JournalCategory));
        public Array JSValues => Enum.GetValues(typeof(JournalSpan));
        #endregion
        public AddViewModel(OverviewViewModel overview, GeneralViewModel general, IWorker<AbstractItem> wl, IWorkerNotify<AbstractItem> wn)
        {
            WLib = wl;
            WNotify = wn;
            AddCommand = new ButtonCommand(AddItem);
            overviewViewModel = overview;
            generalViewModel = general;
        }

        void AddItem()
        {
            if (!(Title != string.Empty && Author != string.Empty && byte.TryParse(Edition, out byte edition)
                && double.TryParse(Price, out double price))) return;
            AbstractItem item;
            if (BClicked && int.TryParse(Pages, out int pages))
                item = new Book(Title, Author, DatePublished, edition, pages, BCategory);
            else if (JClicked && Publisher != String.Empty)
                item = new Journal(Title, Author, DatePublished, edition, Publisher, JCategory, JSpan);
            else return;
            if (!int.TryParse(Amount, out int amount)) amount = 1;
            WLib.Add(item, price, amount);
            overviewViewModel.Items.Add(item);
            WNotify.Added(item, amount);
            generalViewModel.Count += amount;
        }
    }
}
