using BookLib.Models;
using BookLib.Permissions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.ViewModel.Worker
{
    public class GeneralViewModel : ViewModelBase
    {
        public IWorker<AbstractItem> WLib { get; set; }
        public string Count => $"Count of all items in the Library: {WLib.Count}";
        string version = "1.0";
        public string Version { get => $"Version: {version}"; set => version = value; }
        public GeneralViewModel(IWorker<AbstractItem> wl)
        {
            WLib = wl;
            //version = ConfigurationManager.AppSettings["Version"];
        }
    }
}
