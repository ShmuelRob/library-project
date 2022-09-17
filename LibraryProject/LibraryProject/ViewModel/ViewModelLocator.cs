using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using BookLib;
using BookLib.Permissions;
using BookLib.Models;
using BookLib.Notifies;
using LibraryProject.Services;
using LibraryProject.ViewModel.Customer;
using LibraryProject.ViewModel.Worker;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace LibraryProject.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            #region General
            //SimpleIoc.Default.Register<ObservableCollection<AbstractItem>>();
            //SimpleIoc.Default.Register<List<AbstractItem>>();
            SimpleIoc.Default.Register<ItemCollection>();
            SimpleIoc.Default.Register<OpeningViewModel>();
            #endregion
            #region Worker
            SimpleIoc.Default.Register<IWorkerNotify<AbstractItem>, WorkerNotify>();
            SimpleIoc.Default.Register<IWorker<AbstractItem>>(() => SimpleIoc.Default.GetInstance<ItemCollection>());
            //SimpleIoc.Default.Register<ItemPropsViewModel>();
            SimpleIoc.Default.Register<OverviewViewModel>();
            SimpleIoc.Default.Register<AddViewModel>();
            SimpleIoc.Default.Register<RemoveViewModel>();
            SimpleIoc.Default.Register<FilterViewModel>();
            SimpleIoc.Default.Register<GeneralViewModel>();
            #endregion
            #region Customer
            SimpleIoc.Default.Register<ICustomerNotify<AbstractItem>, CustomerNotify>(); //(() => SimpleIoc.Default.GetInstance<CustomerNotify>());
            SimpleIoc.Default.Register<ICustomer<AbstractItem>>(() => SimpleIoc.Default.GetInstance<ItemCollection>());
            SimpleIoc.Default.Register<CustomerViewModel>();
            SimpleIoc.Default.Register<ItemsListViewModel>();
            #endregion
        }
        #region Instances
        #region General
        //public ObservableCollection<AbstractItem> Items => ServiceLocator.Current.GetInstance<ObservableCollection<AbstractItem>>();
        //public List<AbstractItem> ItemsList => ServiceLocator.Current.GetInstance<List<AbstractItem>>(); 
        public ItemCollection ItemCol => ServiceLocator.Current.GetInstance<ItemCollection>();
        public OpeningViewModel Opening => ServiceLocator.Current.GetInstance<OpeningViewModel>();
        #endregion
        #region Worker
        public IWorkerNotify<AbstractItem> WNotify => ServiceLocator.Current.GetInstance<IWorkerNotify<AbstractItem>>();
        public IWorker<AbstractItem> WLib => ServiceLocator.Current.GetInstance<IWorker<AbstractItem>>(); 
        //public ItemPropsViewModel ItemProps => ServiceLocator.Current.GetInstance<ItemPropsViewModel>();
        public OverviewViewModel Overview => ServiceLocator.Current.GetInstance<OverviewViewModel>();
        public AddViewModel Add => ServiceLocator.Current.GetInstance<AddViewModel>();
        public RemoveViewModel Remove => ServiceLocator.Current.GetInstance<RemoveViewModel>();
        public FilterViewModel Filter => ServiceLocator.Current.GetInstance<FilterViewModel>();
        public GeneralViewModel General => ServiceLocator.Current.GetInstance<GeneralViewModel>();
        #endregion
        #region Customer
        public ICustomerNotify<AbstractItem> CNotify => ServiceLocator.Current.GetInstance<ICustomerNotify<AbstractItem>>();
        public ICustomer<AbstractItem> CLib => ServiceLocator.Current.GetInstance<ICustomer<AbstractItem>>(); 
        public CustomerViewModel CView => ServiceLocator.Current.GetInstance<CustomerViewModel>();
        public ItemsListViewModel ItemsListViewModel => ServiceLocator.Current.GetInstance<ItemsListViewModel>();
        #endregion
        #endregion

        public static void Cleanup() { }
    }
}