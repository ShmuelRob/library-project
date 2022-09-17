using System.Windows;
using GalaSoft.MvvmLight;
using LibraryProject.Services;
using LibraryProject.Views;
using LibraryProject.Views.Customer;
using LibraryProject.Views.Worker;

namespace LibraryProject.ViewModel
{
    public class OpeningViewModel : ViewModelBase
    {
        string access;
        private string userName;
        private string password;

        public string Access { get => access; set { Set(ref access, value); } }
        public string UserName { get => userName is null? "UserName" : userName; set => userName = value; }
        public string Password { get => password is null? "Password" : password; set => password = value; }
        public ButtonCommand Worker { get; set; }
        public ButtonCommand Customer { get; set; }

        public OpeningViewModel()
        {
            Worker = new ButtonCommand(OpenWorker);
            Customer = new ButtonCommand(OpenCustomer);
        }

        void OpenWorker()
        {
            if (UserName == "admin" && Password == "admin")
            {
                WorkerView view = new WorkerView { Title = $"Manager - {UserName}" };
                view.Activate();
                view.Show();
                Application.Current.Windows[0].Close();
            }
            else Access = "Acsess Denied";
        }
        void OpenCustomer()
        {
            CustomerView view = new CustomerView();
            view.Activate();
            view.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
