using BookLib.Models;
using BookLib.Notifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Services
{
    class WorkerNotify : IWorkerNotify<AbstractItem>
    {
        public void Added(AbstractItem item, int amount) => MessageBox.Show($"{amount} of {item} was added to the library");
        public bool AskWorker()
        {
            switch (MessageBox.Show("Aru you sure you want to do this?", "are you sure", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                default:
                    return false;
            }
        }
        public void General(string message) => MessageBox.Show(message);
        public void Removed(AbstractItem item, int amount) => MessageBox.Show($"{amount} of {item} was removed from the library");
        public void Updated(AbstractItem item) => MessageBox.Show($"{item} details were updated");
    }
}
